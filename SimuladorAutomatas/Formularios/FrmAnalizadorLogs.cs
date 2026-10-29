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
        // ==========================================
        // CONTROLES DEL ANÁLISIS
        // ==========================================

        private Label lblTitulo;
        private Label lblArchivo;
        private Label lblTotal;
        private Label lblValidos;
        private Label lblInvalidos;
        private Label lblInfo;
        private Label lblWarning;
        private Label lblError;
        private Label lblCritical;

        private RichTextBox txtResumen;

        // ==========================================
        // CONTROLES DE ARCHIVO
        // ==========================================

        private Button btnAbrirArchivo;
        private Button btnCerrar;
        private Button btnSeleccionarArchivo;

        // ==========================================
        // CONTROLES DE REGEX
        // ==========================================

        private Label lblTituloRegex;
        private Label lblEjemploRegex;
        private Label lblRegex;
        private TextBox txtRegex;
        private Button btnBuscarRegex;
        private DataGridView dgvResultadosRegex;
        private Label lblCoincidencias;
        private RichTextBox txtResumenRegex;

        // ==========================================
        // PANELES
        // ==========================================

        private Panel panelAnalisis;
        private Panel panelRegex;

        private string rutaArchivo;

        public FrmAnalizadorLogs(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;

            InicializarFormulario();
            CargarAnalisis();
        }

        private void InicializarFormulario()
        {
            // ==========================================
            // CONFIGURACIÓN DEL FORMULARIO
            // ==========================================

            Text = "Análisis de Logs";
            Width = 1200;
            Height = 800;

            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(1100, 700);

            EstiloInterfaz.AplicarEstiloFormulario(this);

            // ==========================================
            // PANEL IZQUIERDO
            // ==========================================

            panelAnalisis = new Panel();
            panelAnalisis.Location = new Point(15, 15);
            panelAnalisis.Size = new Size(390, 700);

            EstiloInterfaz.AplicarEstiloPanel(panelAnalisis);

            // ==========================================
            // TÍTULO
            // ==========================================

            lblTitulo = new Label();
            lblTitulo.Text = "ANÁLISIS DE LOGS";
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            EstiloInterfaz.AplicarEstiloTitulo(lblTitulo);

            panelAnalisis.Controls.Add(lblTitulo);

            // ==========================================
            // INFORMACIÓN DEL ARCHIVO
            // ==========================================

            lblArchivo = CrearLabel(20, 65);
            lblArchivo.MaximumSize = new Size(350, 0);

            lblTotal = CrearLabel(20, 125);
            lblValidos = CrearLabel(20, 160);
            lblInvalidos = CrearLabel(20, 195);
            lblInfo = CrearLabel(20, 230);
            lblWarning = CrearLabel(20, 265);
            lblError = CrearLabel(20, 300);
            lblCritical = CrearLabel(20, 335);

            panelAnalisis.Controls.Add(lblArchivo);
            panelAnalisis.Controls.Add(lblTotal);
            panelAnalisis.Controls.Add(lblValidos);
            panelAnalisis.Controls.Add(lblInvalidos);
            panelAnalisis.Controls.Add(lblInfo);
            panelAnalisis.Controls.Add(lblWarning);
            panelAnalisis.Controls.Add(lblError);
            panelAnalisis.Controls.Add(lblCritical);

            // ==========================================
            // RESUMEN DEL ARCHIVO
            // ==========================================

            Label lblResumen = CrearLabel(20, 375);
            lblResumen.Text = "RESUMEN";
            lblResumen.ForeColor = EstiloInterfaz.CianActivo;
            lblResumen.Font = EstiloInterfaz.FuenteTitulo;

            panelAnalisis.Controls.Add(lblResumen);

            txtResumen = new RichTextBox();
            txtResumen.Location = new Point(20, 410);
            txtResumen.Size = new Size(350, 170);

            txtResumen.ReadOnly = true;

            EstiloInterfaz.AplicarEstiloRichTextBox(
                txtResumen);

            panelAnalisis.Controls.Add(txtResumen);

            // ==========================================
            // BOTONES DEL ARCHIVO
            // ==========================================

            btnAbrirArchivo = new Button();
            btnAbrirArchivo.Text = "Abrir archivo";
            btnAbrirArchivo.Location = new Point(20, 600);
            btnAbrirArchivo.Size = new Size(105, 35);

            btnAbrirArchivo.Click +=
                btnAbrirArchivo_Click;

            EstiloInterfaz.AplicarEstiloBoton(
                btnAbrirArchivo);

            panelAnalisis.Controls.Add(
                btnAbrirArchivo);

            btnSeleccionarArchivo = new Button();
            btnSeleccionarArchivo.Text = "Seleccionar log";
            btnSeleccionarArchivo.Location =
                new Point(135, 600);
            btnSeleccionarArchivo.Size =
                new Size(115, 35);

            btnSeleccionarArchivo.Click +=
                btnSeleccionarArchivo_Click;

            EstiloInterfaz.AplicarEstiloBoton(
                btnSeleccionarArchivo);

            panelAnalisis.Controls.Add(
                btnSeleccionarArchivo);

            btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Location =
                new Point(260, 600);
            btnCerrar.Size =
                new Size(110, 35);

            btnCerrar.Click += delegate
            {
                Close();
            };

            EstiloInterfaz.AplicarEstiloBoton(
                btnCerrar);

            panelAnalisis.Controls.Add(btnCerrar);

            // ==========================================
            // PANEL DERECHO - REGEX
            // ==========================================

            panelRegex = new Panel();
            panelRegex.Location = new Point(420, 15);
            panelRegex.Size = new Size(745, 700);

            EstiloInterfaz.AplicarEstiloPanel(
                panelRegex);

            // ==========================================
            // TÍTULO REGEX
            // ==========================================

            lblTituloRegex = CrearLabel(20, 20);
            lblTituloRegex.Text =
                "BÚSQUEDA MEDIANTE EXPRESIONES REGULARES";

            lblTituloRegex.ForeColor =
                EstiloInterfaz.CianActivo;

            lblTituloRegex.Font =
                EstiloInterfaz.FuenteTitulo;

            panelRegex.Controls.Add(
                lblTituloRegex);

            // ==========================================
            // EJEMPLOS
            // ==========================================

            lblEjemploRegex = CrearLabel(20, 55);

            lblEjemploRegex.Text =
                "Ejemplos: .* INFO .*   |   .* WARNING .*   |   .* STATE .*   |   .* CRITICAL .*";

            lblEjemploRegex.ForeColor =
                EstiloInterfaz.TextoSecundario;

            panelRegex.Controls.Add(
                lblEjemploRegex);

            // ==========================================
            // EXPRESIÓN
            // ==========================================

            lblRegex = CrearLabel(20, 90);
            lblRegex.Text =
                "Expresión regular:";

            panelRegex.Controls.Add(lblRegex);

            txtRegex = new TextBox();
            txtRegex.Location =
                new Point(20, 120);

            txtRegex.Size =
                new Size(480, 30);

            EstiloInterfaz.AplicarEstiloTextBox(
                txtRegex);

            panelRegex.Controls.Add(txtRegex);

            // ==========================================
            // BOTÓN BUSCAR
            // ==========================================

            btnBuscarRegex = new Button();
            btnBuscarRegex.Text =
                "Buscar con Regex";

            btnBuscarRegex.Location =
                new Point(515, 118);

            btnBuscarRegex.Size =
                new Size(200, 35);

            btnBuscarRegex.Click +=
                btnBuscarRegex_Click;

            EstiloInterfaz.AplicarEstiloBoton(
                btnBuscarRegex);

            panelRegex.Controls.Add(
                btnBuscarRegex);

            // ==========================================
            // COINCIDENCIAS
            // ==========================================

            lblCoincidencias = CrearLabel(20, 165);

            lblCoincidencias.Text =
                "Coincidencias: 0";

            panelRegex.Controls.Add(
                lblCoincidencias);

            // ==========================================
            // RESUMEN DE RESULTADOS REGEX
            // ==========================================

            txtResumenRegex = new RichTextBox();

            txtResumenRegex.Location =
                new Point(20, 195);

            txtResumenRegex.Size =
                new Size(695, 120);

            txtResumenRegex.ReadOnly = true;

            EstiloInterfaz.AplicarEstiloRichTextBox(
                txtResumenRegex);

            panelRegex.Controls.Add(
                txtResumenRegex);

            // ==========================================
            // DATAGRIDVIEW
            // ==========================================

            dgvResultadosRegex = new DataGridView();

            dgvResultadosRegex.Location =
                new Point(20, 330);

            dgvResultadosRegex.Size =
                new Size(695, 325);

            dgvResultadosRegex.AllowUserToAddRows =
                false;

            dgvResultadosRegex.AllowUserToDeleteRows =
                false;

            dgvResultadosRegex.ReadOnly =
                true;

            dgvResultadosRegex.MultiSelect =
                false;

            dgvResultadosRegex.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvResultadosRegex.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvResultadosRegex.RowTemplate.Height = 28;

            // ==========================================
            // COLUMNAS
            // ==========================================

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

            dgvResultadosRegex.Columns[
                "Numero"].FillWeight = 8;

            dgvResultadosRegex.Columns[
                "Nivel"].FillWeight = 12;

            dgvResultadosRegex.Columns[
                "Modulo"].FillWeight = 15;

            dgvResultadosRegex.Columns[
                "Mensaje"].FillWeight = 30;

            dgvResultadosRegex.Columns[
                "Registro"].FillWeight = 35;

            EstiloInterfaz.AplicarEstiloDataGridView(
                dgvResultadosRegex);

            panelRegex.Controls.Add(
                dgvResultadosRegex);

            // ==========================================
            // AGREGAR PANELES AL FORMULARIO
            // ==========================================

            Controls.Add(panelAnalisis);
            Controls.Add(panelRegex);
        }

        // ==========================================
        // CREAR LABEL
        // ==========================================

        private Label CrearLabel(int x, int y)
        {
            Label etiqueta = new Label();

            etiqueta.AutoSize = true;
            etiqueta.Location =
                new Point(x, y);

            EstiloInterfaz.AplicarEstiloLabel(
                etiqueta);

            return etiqueta;
        }

        // ==========================================
        // CARGAR ANÁLISIS
        // ==========================================

        private void CargarAnalisis()
        {
            if (!File.Exists(rutaArchivo))
            {
                lblArchivo.Text =
                    "Archivo: No encontrado";

                return;
            }

            LogAnalysisResult resultado =
                LogAnalyzer.Analyze(
                    rutaArchivo);

            lblArchivo.Text =
                "Archivo:\r\n" +
                rutaArchivo;

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
                "INFO: " +
                resultado.Info;

            lblWarning.Text =
                "WARNING: " +
                resultado.Warning;

            lblError.Text =
                "ERROR: " +
                resultado.Error;

            lblCritical.Text =
                "CRITICAL: " +
                resultado.Critical;

            // ==========================================
            // RESUMEN DEL ARCHIVO
            // ==========================================

            txtResumen.Clear();

            txtResumen.AppendText(
                "RESUMEN\r\n\r\n");

            txtResumen.AppendText(
                "Total de registros: " +
                resultado.Total +
                "\r\n");

            txtResumen.AppendText(
                "Registros válidos: " +
                resultado.Validos +
                "\r\n");

            txtResumen.AppendText(
                "Registros inválidos: " +
                resultado.Invalidos +
                "\r\n\r\n");

            txtResumen.AppendText(
                "NIVELES\r\n");

            txtResumen.AppendText(
                "INFO: " +
                resultado.Info +
                "\r\n");

            txtResumen.AppendText(
                "WARNING: " +
                resultado.Warning +
                "\r\n");

            txtResumen.AppendText(
                "ERROR: " +
                resultado.Error +
                "\r\n");

            txtResumen.AppendText(
                "CRITICAL: " +
                resultado.Critical +
                "\r\n");

            if (resultado.Invalidos > 0)
            {
                txtResumen.AppendText(
                    "\r\nREGISTROS INVÁLIDOS\r\n");

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

            // ==========================================
            // LIMPIAR RESULTADOS REGEX
            // ==========================================

            dgvResultadosRegex.Rows.Clear();

            lblCoincidencias.Text =
                "Coincidencias: 0";

            txtResumenRegex.Clear();
        }

        // ==========================================
        // BUSCAR MEDIANTE REGEX
        // ==========================================

        private void btnBuscarRegex_Click(
            object sender,
            EventArgs e)
        {
            string expresion =
                txtRegex.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                expresion))
            {
                MessageBox.Show(
                    "Ingrese una expresión regular.",
                    "Búsqueda Regex",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ==========================================
            // VALIDAR EXPRESIÓN
            // ==========================================

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

            // ==========================================
            // BUSCAR EN EL ARCHIVO
            // ==========================================

            List<string> resultados =
                LogRegexAnalyzer.Buscar(
                    rutaArchivo,
                    expresion);

            dgvResultadosRegex.Rows.Clear();

            // ==========================================
            // CONTADORES DEL ANÁLISIS
            // ==========================================

            int info = 0;
            int warning = 0;
            int error = 0;
            int critical = 0;

            Dictionary<string, int> modulos =
                new Dictionary<string, int>();

            // ==========================================
            // PROCESAR RESULTADOS
            // ==========================================

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
                    // Mostrar resultado en la tabla
                    dgvResultadosRegex.Rows.Add(
                        i + 1,
                        evento.Nivel,
                        evento.Modulo,
                        evento.Mensaje,
                        resultados[i]);

                    // ==========================================
                    // CONTAR NIVEL
                    // ==========================================

                    if (evento.Nivel == "INFO")
                    {
                        info++;
                    }
                    else if (evento.Nivel == "WARNING")
                    {
                        warning++;
                    }
                    else if (evento.Nivel == "ERROR")
                    {
                        error++;
                    }
                    else if (evento.Nivel == "CRITICAL")
                    {
                        critical++;
                    }

                    // ==========================================
                    // CONTAR MÓDULO
                    // ==========================================

                    if (modulos.ContainsKey(
                        evento.Modulo))
                    {
                        modulos[evento.Modulo]++;
                    }
                    else
                    {
                        modulos.Add(
                            evento.Modulo,
                            1);
                    }
                }
            }

            // ==========================================
            // MOSTRAR CANTIDAD DE COINCIDENCIAS
            // ==========================================

            lblCoincidencias.Text =
                "Coincidencias: " +
                resultados.Count;

            // ==========================================
            // MOSTRAR RESUMEN DE RESULTADOS
            // ==========================================

            txtResumenRegex.Clear();

            txtResumenRegex.AppendText(
                "ANÁLISIS DE RESULTADOS\r\n\r\n");

            txtResumenRegex.AppendText(
                "Expresión: " +
                expresion +
                "\r\n");

            txtResumenRegex.AppendText(
                "Coincidencias: " +
                resultados.Count +
                "\r\n\r\n");

            txtResumenRegex.AppendText(
                "NIVELES\r\n");

            txtResumenRegex.AppendText(
                "INFO: " +
                info +
                "    WARNING: " +
                warning +
                "    ERROR: " +
                error +
                "    CRITICAL: " +
                critical +
                "\r\n\r\n");

            txtResumenRegex.AppendText(
                "MÓDULOS\r\n");

            if (modulos.Count == 0)
            {
                txtResumenRegex.AppendText(
                    "No se encontraron módulos.");
            }
            else
            {
                foreach (
                    KeyValuePair<string, int> modulo
                    in modulos)
                {
                    txtResumenRegex.AppendText(
                        modulo.Key +
                        ": " +
                        modulo.Value +
                        "    ");
                }
            }
        }

        // ==========================================
        // ABRIR ARCHIVO
        // ==========================================

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

        // ==========================================
        // SELECCIONAR LOG
        // ==========================================

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

            if (dialogo.ShowDialog() ==
                DialogResult.OK)
            {
                rutaArchivo =
                    dialogo.FileName;

                CargarAnalisis();
            }
        }
    }
}