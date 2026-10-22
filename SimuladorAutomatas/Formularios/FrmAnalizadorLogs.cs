using SimuladorAutomatas.Logica;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SimuladorAutomatas.Formularios
{
    public class FrmAnalizadorLogs : Form
    {
        private Label lblTitulo;
        private Label lblArchivo;
        private Label lblTotal;
        private Label lblValidos;
        private Label lblInvalidos;
        private Label lblInfo;
        private Label lblWarning;
        private Label lblError;
        private RichTextBox txtResumen;
        private Button btnAbrirArchivo;
        private Button btnCerrar;
        private Button btnSeleccionarArchivo;

        private string rutaArchivo;

        public FrmAnalizadorLogs(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;

            InicializarFormulario();
            CargarAnalisis();
        }

        private void InicializarFormulario()
        {
            Text = "Análisis de Logs";
            Width = 700;
            Height = 550;
            StartPosition = FormStartPosition.CenterParent;

            EstiloInterfaz.AplicarEstiloFormulario(this);

            lblTitulo = new Label();
            lblTitulo.Text = "ANÁLISIS DE LOGS";
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(25, 20);
            EstiloInterfaz.AplicarEstiloTitulo(lblTitulo);

            lblArchivo = CrearLabel(25, 60);
            lblTotal = CrearLabel(25, 95);
            lblValidos = CrearLabel(25, 130);
            lblInvalidos = CrearLabel(25, 165);
            lblInfo = CrearLabel(25, 200);
            lblWarning = CrearLabel(25, 235);
            lblError = CrearLabel(25, 270);

            txtResumen = new RichTextBox();
            txtResumen.Location = new Point(25, 310);
            txtResumen.Size = new Size(630, 130);
            txtResumen.ReadOnly = true;
            EstiloInterfaz.AplicarEstiloRichTextBox(txtResumen);

            btnSeleccionarArchivo = new Button();
            btnSeleccionarArchivo.Text = "Seleccionar log";
            btnSeleccionarArchivo.Location = new Point(220, 460);
            btnSeleccionarArchivo.Size = new Size(180, 35);
            btnSeleccionarArchivo.Click += btnSeleccionarArchivo_Click;
            EstiloInterfaz.AplicarEstiloBoton(btnSeleccionarArchivo);
            btnAbrirArchivo = new Button();
            btnAbrirArchivo.Text = "Abrir archivo original";
            btnAbrirArchivo.Location = new Point(25, 460);
            btnAbrirArchivo.Size = new Size(180, 35);
            btnAbrirArchivo.Click += btnAbrirArchivo_Click;
            EstiloInterfaz.AplicarEstiloBoton(btnAbrirArchivo);

            btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Location = new Point(520, 460);
            btnCerrar.Size = new Size(135, 35);
            btnCerrar.Click += delegate
            {
                Close();
            };
            EstiloInterfaz.AplicarEstiloBoton(btnCerrar);

            Controls.Add(lblTitulo);
            Controls.Add(lblArchivo);
            Controls.Add(lblTotal);
            Controls.Add(lblValidos);
            Controls.Add(lblInvalidos);
            Controls.Add(lblInfo);
            Controls.Add(lblWarning);
            Controls.Add(lblError);
            Controls.Add(txtResumen);
            Controls.Add(btnAbrirArchivo);
            Controls.Add(btnSeleccionarArchivo);
            Controls.Add(btnCerrar);
        }

        private Label CrearLabel(int x, int y)
        {
            Label etiqueta = new Label();
            etiqueta.AutoSize = true;
            etiqueta.Location = new Point(x, y);
            EstiloInterfaz.AplicarEstiloLabel(etiqueta);
            return etiqueta;
        }

        private void CargarAnalisis()
        {
            if (!File.Exists(rutaArchivo))
            {
                lblArchivo.Text = "Archivo: No encontrado";
                return;
            }

            LogAnalysisResult resultado =
                LogAnalyzer.Analyze(rutaArchivo);

            lblArchivo.Text =
                "Archivo: " + rutaArchivo;

            lblTotal.Text =
                "Registros analizados: " + resultado.Total;

            lblValidos.Text =
                "Registros válidos: " + resultado.Validos;

            lblInvalidos.Text =
                "Registros inválidos: " + resultado.Invalidos;

            lblInfo.Text =
                "INFO: " + resultado.Info;

            lblWarning.Text =
                "WARNING: " + resultado.Warning;

            lblError.Text =
                "ERROR: " + resultado.Error;

            txtResumen.Clear();

            txtResumen.AppendText(
                "RESUMEN\r\n\r\n");

            txtResumen.AppendText(
                "El archivo contiene " +
                resultado.Total +
                " registros.\r\n");

            txtResumen.AppendText(
                "Registros válidos: " +
                resultado.Validos +
                "\r\n");

            txtResumen.AppendText(
                "Registros inválidos: " +
                resultado.Invalidos +
                "\r\n");

            if (resultado.Invalidos > 0)
            {
                txtResumen.AppendText(
                    "\r\nSe encontraron registros con formato inválido.\r\n");

                txtResumen.AppendText(
                    "\r\nDETALLE DE REGISTROS INVÁLIDOS\r\n");

                for (int i = 0; i < resultado.RegistrosInvalidos.Count; i++)
                {
                    txtResumen.AppendText(
                        "\r\n" +
                        resultado.RegistrosInvalidos[i] +
                        "\r\n");
                }
            }
        }

        private void btnAbrirArchivo_Click(
            object sender,
            EventArgs e)
        {
            if (!File.Exists(rutaArchivo))
                return;

            System.Diagnostics.Process.Start(
                "notepad.exe",
                "\"" + rutaArchivo + "\"");
        }

        private void btnSeleccionarArchivo_Click(
            object sender,
            EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();

            dialogo.Filter = "Archivos de log (*.log)|*.log|Todos los archivos (*.*)|*.*";
            dialogo.Title = "Seleccionar archivo de log";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = dialogo.FileName;
                CargarAnalisis();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmAnalizadorLogs
            // 
            this.ClientSize = new System.Drawing.Size(452, 397);
            this.Name = "FrmAnalizadorLogs";
            this.ResumeLayout(false);

        }
    }
}