using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimuladorAutomatas.Formularios
{
    public class FrmAnalizadorLogs : Form
    {
        private TextBox txtRuta;
        private Button btnAbrir;
        private Button btnAnalizar;
        private Button btnOriginal;
        private Label lblTotal;
        private Label lblValidos;
        private Label lblInvalidos;
        private Label lblInfo;
        private Label lblWarning;
        private Label lblError;
        private TextBox txtResumen;
        private ListBox lstInvalidos;

        public FrmAnalizadorLogs()
        {
            ConstruirInterfaz();
            CargarRutaPredeterminada();
        }

        private void ConstruirInterfaz()
        {
            Text = "Análisis de Logs - Simulador de Autómatas";
            StartPosition = FormStartPosition.CenterParent;
            Width = 900;
            Height = 650;
            MinimizeBox = false;

            SimuladorAutomatas.Logica.EstiloInterfaz.AplicarEstiloFormulario(this);

            Label titulo = new Label { Left = 20, Top = 15, Width = 500, Text = "ANÁLISIS DE LOGS" };
            SimuladorAutomatas.Logica.EstiloInterfaz.AplicarEstiloTitulo(titulo);
            Controls.Add(titulo);

            Label ruta = new Label { Left = 20, Top = 55, Width = 90, Text = "Archivo:" };
            SimuladorAutomatas.Logica.EstiloInterfaz.AplicarEstiloLabel(ruta);
            Controls.Add(ruta);

            txtRuta = new TextBox { Left = 105, Top = 52, Width = 560, ReadOnly = true };
            SimuladorAutomatas.Logica.EstiloInterfaz.AplicarEstiloTextBox(txtRuta);
            Controls.Add(txtRuta);

            btnAbrir = CrearBoton("Abrir archivo", 680, 48, 90, 32);
            btnAbrir.Click += btnAbrir_Click;
            Controls.Add(btnAbrir);

            btnAnalizar = CrearBoton("Analizar", 775, 48, 90, 32);
            btnAnalizar.Click += btnAnalizar_Click;
            Controls.Add(btnAnalizar);

            lblTotal = CrearIndicador("Registros analizados: 0", 20, 100);
            lblValidos = CrearIndicador("Registros válidos: 0", 220, 100);
            lblInvalidos = CrearIndicador("Registros inválidos: 0", 420, 100);
            lblInfo = CrearIndicador("INFO: 0", 620, 100);
            lblWarning = CrearIndicador("WARNING: 0", 620, 135);
            lblError = CrearIndicador("ERROR: 0", 620, 170);

            Label resumen = new Label { Left = 20, Top = 145, Width = 100, Text = "RESUMEN" };
            SimuladorAutomatas.Logica.EstiloInterfaz.AplicarEstiloTitulo(resumen);
            Controls.Add(resumen);

            txtResumen = new TextBox { Left = 20, Top = 175, Width = 570, Height = 150, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
            SimuladorAutomatas.Logica.EstiloInterfaz.AplicarEstiloTextBox(txtResumen);
            Controls.Add(txtResumen);

            Label errores = new Label { Left = 20, Top = 345, Width = 300, Text = "REGISTROS INVÁLIDOS" };
            SimuladorAutomatas.Logica.EstiloInterfaz.AplicarEstiloTitulo(errores);
            Controls.Add(errores);

            lstInvalidos = new ListBox { Left = 20, Top = 375, Width = 845, Height = 170 };
            Controls.Add(lstInvalidos);

            btnOriginal = CrearBoton("Abrir archivo original", 20, 560, 180, 35);
            btnOriginal.Click += btnOriginal_Click;
            Controls.Add(btnOriginal);
        }

        private Button CrearBoton(string texto, int x, int y, int ancho, int alto)
        {
            Button boton = new Button { Text = texto, Left = x, Top = y, Width = ancho, Height = alto };
            SimuladorAutomatas.Logica.EstiloInterfaz.AplicarEstiloBoton(boton);
            return boton;
        }

        private Label CrearIndicador(string texto, int x, int y)
        {
            Label etiqueta = new Label { Text = texto, Left = x, Top = y, Width = 190, Height = 25 };
            SimuladorAutomatas.Logica.EstiloInterfaz.AplicarEstiloLabel(etiqueta);
            Controls.Add(etiqueta);
            return etiqueta;
        }

        private void CargarRutaPredeterminada()
        {
            txtRuta.Text = SimuladorAutomatas.Logica.LogService.GetLogFilePath();
            Analizar();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Archivos de log (*.log;*.txt)|*.log;*.txt|Todos los archivos (*.*)|*.*";
                dialogo.Title = "Abrir archivo de log";

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                txtRuta.Text = dialogo.FileName;
                Analizar();
            }
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            Analizar();
        }

        private void Analizar()
        {
            string ruta = txtRuta.Text;
            if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
            {
                txtResumen.Text = "No existe un archivo de log para analizar.";
                return;
            }

            SimuladorAutomatas.Logica.LogAnalysisResult resultado = SimuladorAutomatas.Logica.LogAnalyzer.Analyze(ruta);

            lblTotal.Text = "Registros analizados: " + resultado.Total;
            lblValidos.Text = "Registros válidos: " + resultado.Validos;
            lblInvalidos.Text = "Registros inválidos: " + resultado.Invalidos;
            lblInfo.Text = "INFO: " + resultado.Info;
            lblWarning.Text = "WARNING: " + resultado.Warning;
            lblError.Text = "ERROR: " + resultado.Error;

            StringBuilder resumen = new StringBuilder();
            resumen.AppendLine("Archivo: " + ruta);
            resumen.AppendLine();
            resumen.AppendLine("La mayoría de los registros analizados cumplen el formato establecido.");
            if (resultado.Invalidos == 0)
                resumen.AppendLine("✓ Todos los registros son válidos.");
            else
                resumen.AppendLine("⚠ Se encontraron " + resultado.Invalidos + " registros que requieren revisión.");

            if (resultado.Error > 0)
                resumen.AppendLine("✖ Existen eventos ERROR que conviene revisar.");
            if (resultado.Warning > 0)
                resumen.AppendLine("⚠ Existen eventos WARNING en el historial.");

            txtResumen.Text = resumen.ToString();

            lstInvalidos.Items.Clear();
            foreach (string registro in resultado.RegistrosInvalidos)
                lstInvalidos.Items.Add(registro);
        }

        private void btnOriginal_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtRuta.Text))
            {
                MessageBox.Show("El archivo original no existe.", "Logs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process.Start(txtRuta.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el archivo original.\n\n" + ex.Message, "Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
