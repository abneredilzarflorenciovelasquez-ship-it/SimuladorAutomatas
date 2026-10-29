using SimuladorAutomatas.Logica;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
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

        private Label lblRegex;
        private Label lblTituloRegex;
        private Label lblEjemploRegex;
        private TextBox txtRegex;
        private Button btnBuscarRegex;
        private DataGridView dgvResultadosRegex;
        private Label lblCoincidencias;

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
            Width = 850;
            Height = 900;
            StartPosition = FormStartPosition.CenterParent;
            AutoScroll = true;

            EstiloInterfaz.AplicarEstiloFormulario(this);

            // =========================
            // TÍTULO
            // =========================

            lblTitulo = new Label();
            lblTitulo.Text = "ANÁLISIS DE LOGS";
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(25, 20);
            EstiloInterfaz.AplicarEstiloTitulo(lblTitulo);

            // =========================
            // INFORMACIÓN DEL ARCHIVO
            // =========================

            lblArchivo = CrearLabel(25, 60);
            lblTotal = CrearLabel(25, 95);
            lblValidos = CrearLabel(25, 130);
            lblInvalidos = CrearLabel(25, 165);
            lblInfo = CrearLabel(25, 200);
            lblWarning = CrearLabel(25, 235);
            lblError = CrearLabel(25, 270);

            // =========================
            // RESUMEN
            // =========================

            txtResumen = new RichTextBox();
            txtResumen.Location = new Point(25, 310);
            txtResumen.Size = new Size(790, 140);
            txtResumen.ReadOnly = true;
            EstiloInterfaz.AplicarEstiloRichTextBox(txtResumen);

            // =========================
            // BÚSQUEDA CON REGEX
            // =========================

            lblTituloRegex = CrearLabel(25, 465);
            lblTituloRegex.Text = "BÚSQUEDA MEDIANTE EXPRESIONES REGULARES";
            lblTituloRegex.ForeColor = EstiloInterfaz.CianActivo;
            lblTituloRegex.Font = EstiloInterfaz.FuenteTitulo;

            lblEjemploRegex = CrearLabel(25, 495);
            lblEjemploRegex.Text =
                "Ejemplo: .* INFO .*   |   .* WARNING .*   |   .* STATE .*";
            lblEjemploRegex.ForeColor = EstiloInterfaz.TextoSecundario;

            lblRegex = CrearLabel(25, 525);
            lblRegex.Text = "Expresión regular:";

            txtRegex = new TextBox();
            txtRegex.Location = new Point(25, 550);
            txtRegex.Size = new Size(500, 30);
            EstiloInterfaz.AplicarEstiloTextBox(txtRegex);

            btnBuscarRegex = new Button();
            btnBuscarRegex.Text = "Buscar con Regex";
            btnBuscarRegex.Location = new Point(540, 553);
            btnBuscarRegex.Size = new Size(200, 35);
            btnBuscarRegex.Click += btnBuscarRegex_Click;
            EstiloInterfaz.AplicarEstiloBoton(btnBuscarRegex);

            // =========================
            // RESULTADOS
            // =========================

            lblCoincidencias = CrearLabel(25, 600);
            lblCoincidencias.Text = "Coincidencias: 0";

            dgvResultadosRegex = new DataGridView();
            dgvResultadosRegex.Location = new Point(25, 630);
            dgvResultadosRegex.Size = new Size(790, 150);

            dgvResultadosRegex.AllowUserToAddRows = false;
            dgvResultadosRegex.AllowUserToDeleteRows = false;
            dgvResultadosRegex.ReadOnly = true;
            dgvResultadosRegex.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvResultadosRegex.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvResultadosRegex.Columns.Add(
                "Numero",
                "N°");

            dgvResultadosRegex.Columns.Add(
                "Nivel",
                "Nivel");

            dgvResultadosRegex.Columns.Add(
                "Modulo",
                "Módulo");

            dgvResultadosRegex.Columns.Add(
                "Mensaje",
                "Mensaje");

            dgvResultadosRegex.Columns.Add(
                "Registro",
                "Registro completo");

            dgvResultadosRegex.Columns["Numero"].FillWeight = 8;
            dgvResultadosRegex.Columns["Nivel"].FillWeight = 12;
            dgvResultadosRegex.Columns["Modulo"].FillWeight = 15;
            dgvResultadosRegex.Columns["Mensaje"].FillWeight = 30;
            dgvResultadosRegex.Columns["Registro"].FillWeight = 35;

            EstiloInterfaz.AplicarEstiloDataGridView(
                dgvResultadosRegex);

            // =========================
            // BOTONES
            // =========================

            btnAbrirArchivo = new Button();
            btnAbrirArchivo.Text = "Abrir archivo original";
            btnAbrirArchivo.Location = new Point(25, 805);
            btnAbrirArchivo.Size = new Size(180, 35);
            btnAbrirArchivo.Click += btnAbrirArchivo_Click;
            EstiloInterfaz.AplicarEstiloBoton(btnAbrirArchivo);

            btnSeleccionarArchivo = new Button();
            btnSeleccionarArchivo.Text = "Seleccionar log";
            btnSeleccionarArchivo.Location = new Point(220, 805);
            btnSeleccionarArchivo.Size = new Size(180, 35);
            btnSeleccionarArchivo.Click += btnSeleccionarArchivo_Click;
            EstiloInterfaz.AplicarEstiloBoton(btnSeleccionarArchivo);

            btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Location = new Point(680, 805);
            btnCerrar.Size = new Size(135, 35);
            btnCerrar.Click += delegate
            {
                Close();
            };
            EstiloInterfaz.AplicarEstiloBoton(btnCerrar);

            // =========================
            // AGREGAR CONTROLES
            // =========================

            Controls.Add(lblTitulo);

            Controls.Add(lblArchivo);
            Controls.Add(lblTotal);
            Controls.Add(lblValidos);
            Controls.Add(lblInvalidos);
            Controls.Add(lblInfo);
            Controls.Add(lblWarning);
            Controls.Add(lblError);

            Controls.Add(txtResumen);

            Controls.Add(lblTituloRegex);
            Controls.Add(lblEjemploRegex);
            Controls.Add(lblRegex);
            Controls.Add(txtRegex);
            Controls.Add(btnBuscarRegex);
            Controls.Add(lblCoincidencias);
            Controls.Add(dgvResultadosRegex);

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
                "Registros analizados: " +
                resultado.Total;

            lblValidos.Text =
                "Registros válidos: " +
                resultado.Validos;

            lblInvalidos.Text =
                "Registros inválidos: " +
                resultado.Invalidos;

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
                    "\r\nSe encontraron registros " +
                    "con formato inválido.\r\n");

                txtResumen.AppendText(
                    "\r\nDETALLE DE REGISTROS INVÁLIDOS\r\n");

                for (int i = 0;
                     i < resultado.RegistrosInvalidos.Count;
                     i++)
                {
                    txtResumen.AppendText(
                        "\r\n" +
                        resultado.RegistrosInvalidos[i] +
                        "\r\n");
                }
            }

            // Limpiar resultados anteriores
            dgvResultadosRegex.Rows.Clear();

            lblCoincidencias.Text =
                "Coincidencias: 0";
        }

        private void btnBuscarRegex_Click(
            object sender,
            EventArgs e)
        {
            string expresion =
                txtRegex.Text.Trim();

            if (string.IsNullOrWhiteSpace(expresion))
            {
                MessageBox.Show(
                    "Ingrese una expresión regular.",
                    "Búsqueda Regex",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Validar que la expresión sea correcta
            try
            {
                new Regex(expresion);
            }
            catch (ArgumentException)
            {
                MessageBox.Show(
                    "La expresión regular no es válida.",
                    "Búsqueda Regex",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            List<string> resultados =
                LogRegexAnalyzer.Buscar(
                    rutaArchivo,
                    expresion);

            dgvResultadosRegex.Rows.Clear();

            for (int i = 0;
                 i < resultados.Count;
                 i++)
            {
                LogEvent evento;
                string detalle;

                if (LogParser.TryParse(
                    resultados[i],
                    out evento,
                    out detalle))
                {
                    dgvResultadosRegex.Rows.Add(
                        i + 1,
                        evento.Nivel,
                        evento.Modulo,
                        evento.Mensaje,
                        resultados[i]);
                }
            }

            lblCoincidencias.Text =
                "Coincidencias: " +
                resultados.Count;
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
            OpenFileDialog dialogo =
                new OpenFileDialog();

            dialogo.Filter =
                "Archivos de log (*.log)|*.log|" +
                "Todos los archivos (*.*)|*.*";

            dialogo.Title =
                "Seleccionar archivo de log";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo =
                    dialogo.FileName;

                CargarAnalisis();
            }
        }
    }
}