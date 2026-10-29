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

        private Label lblNivel;
        private ComboBox cmbNivel;

        private Label lblModulo;
        private ComboBox cmbModulo;

        private Label lblRegex;
        private TextBox txtRegex;

        private Button btnBuscarRegex;
        private Button btnLimpiarFiltros;

        private DataGridView dgvResultadosRegex;

        private Label lblCoincidencias;
        private RichTextBox txtResumenRegex;

        // ==========================================
        // PANELES
        // ==========================================

        private Panel panelAnalisis;
        private Panel panelRegex;

        private string rutaArchivo;

        // ==========================================
        // CONSTRUCTOR
        // ==========================================

        public FrmAnalizadorLogs(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;

            InicializarFormulario();
            CargarAnalisis();
        }

        // ==========================================
        // INICIALIZAR FORMULARIO
        // ==========================================

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
                "Ejemplos: .* INFO .* | .* WARNING .* | .* STATE .* | .* CRITICAL .*";

            lblEjemploRegex.ForeColor =
                EstiloInterfaz.TextoSecundario;

            panelRegex.Controls.Add(
                lblEjemploRegex);

            // ==========================================
            // FILTRO POR NIVEL
            // ==========================================

            lblNivel = CrearLabel(20, 90);
            lblNivel.Text =
                "Nivel:";

            panelRegex.Controls.Add(lblNivel);

            cmbNivel = new ComboBox();
            cmbNivel.Location =
                new Point(20, 115);

            cmbNivel.Size =
                new Size(145, 30);

            cmbNivel.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbNivel.Items.Add("Todos");
            cmbNivel.Items.Add("INFO");
            cmbNivel.Items.Add("WARNING");
            cmbNivel.Items.Add("ERROR");
            cmbNivel.Items.Add("CRITICAL");

            cmbNivel.SelectedIndex = 0;

            cmbNivel.SelectedIndexChanged +=
                cmbNivel_SelectedIndexChanged;

            panelRegex.Controls.Add(cmbNivel);

            // ==========================================
            // FILTRO POR MÓDULO
            // ==========================================

            lblModulo = CrearLabel(185, 90);
            lblModulo.Text =
                "Módulo:";

            panelRegex.Controls.Add(lblModulo);

            cmbModulo = new ComboBox();
            cmbModulo.Location =
                new Point(185, 115);

            cmbModulo.Size =
                new Size(145, 30);

            cmbModulo.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbModulo.Items.Add("Todos");
            cmbModulo.SelectedIndex = 0;

            cmbModulo.SelectedIndexChanged +=
                cmbModulo_SelectedIndexChanged;

            panelRegex.Controls.Add(cmbModulo);

            // ==========================================
            // EXPRESIÓN REGULAR
            // ==========================================

            lblRegex = CrearLabel(350, 90);
            lblRegex.Text =
                "Expresión:";

            panelRegex.Controls.Add(lblRegex);

            txtRegex = new TextBox();
            txtRegex.Location =
                new Point(350, 115);

            txtRegex.Size =
                new Size(195, 30);

            EstiloInterfaz.AplicarEstiloTextBox(
                txtRegex);

            panelRegex.Controls.Add(txtRegex);

            // ==========================================
            // BOTÓN BUSCAR
            // ==========================================

            btnBuscarRegex = new Button();
            btnBuscarRegex.Text =
                "Buscar";

            btnBuscarRegex.Location =
                new Point(555, 113);

            btnBuscarRegex.Size =
                new Size(80, 35);

            btnBuscarRegex.Click +=
                btnBuscarRegex_Click;

            EstiloInterfaz.AplicarEstiloBoton(
                btnBuscarRegex);

            panelRegex.Controls.Add(
                btnBuscarRegex);

            // ==========================================
            // BOTÓN LIMPIAR
            // ==========================================

            btnLimpiarFiltros = new Button();
            btnLimpiarFiltros.Text =
                "Limpiar";

            btnLimpiarFiltros.Location =
                new Point(640, 113);

            btnLimpiarFiltros.Size =
                new Size(75, 35);

            btnLimpiarFiltros.Click +=
                btnLimpiarFiltros_Click;

            EstiloInterfaz.AplicarEstiloBoton(
                btnLimpiarFiltros);

            panelRegex.Controls.Add(
                btnLimpiarFiltros);

            // ==========================================
            // COINCIDENCIAS
            // ==========================================

            lblCoincidencias =
                CrearLabel(20, 160);

            lblCoincidencias.Text =
                "Coincidencias: 0";

            panelRegex.Controls.Add(
                lblCoincidencias);

            // ==========================================
            // RESUMEN DE RESULTADOS
            // ==========================================

            txtResumenRegex =
                new RichTextBox();

            txtResumenRegex.Location =
                new Point(20, 190);

            txtResumenRegex.Size =
                new Size(695, 130);

            txtResumenRegex.ReadOnly = true;

            EstiloInterfaz.AplicarEstiloRichTextBox(
                txtResumenRegex);

            panelRegex.Controls.Add(
                txtResumenRegex);

            // ==========================================
            // DATAGRIDVIEW
            // ==========================================

            dgvResultadosRegex =
                new DataGridView();

            dgvResultadosRegex.Location =
                new Point(20, 335);

            dgvResultadosRegex.Size =
                new Size(695, 320);

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

            dgvResultadosRegex.RowTemplate.Height =
                28;

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
            // AGREGAR PANELES
            // ==========================================

            Controls.Add(panelAnalisis);
            Controls.Add(panelRegex);
        }

        // ==========================================
        // CREAR LABEL
        // ==========================================

        private Label CrearLabel(int x, int y)
        {
            Label etiqueta =
                new Label();

            etiqueta.AutoSize = true;

            etiqueta.Location =
                new Point(x, y);

            EstiloInterfaz.AplicarEstiloLabel(
                etiqueta);

            return etiqueta;
        }

        // ==========================================
        // CAMBIAR NIVEL
        // ==========================================

        private void cmbNivel_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            ActualizarExpresionDesdeFiltros();
        }

        // ==========================================
        // CAMBIAR MÓDULO
        // ==========================================

        private void cmbModulo_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            ActualizarExpresionDesdeFiltros();
        }

        // ==========================================
        // ACTUALIZAR EXPRESIÓN
        // ==========================================

        private void ActualizarExpresionDesdeFiltros()
        {
            if (cmbNivel == null ||
                cmbModulo == null ||
                txtRegex == null)
            {
                return;
            }

            string nivel =
                cmbNivel.SelectedItem == null
                    ? "Todos"
                    : cmbNivel.SelectedItem.ToString();

            string modulo =
                cmbModulo.SelectedItem == null
                    ? "Todos"
                    : cmbModulo.SelectedItem.ToString();

            string expresion = ".*";

            // ==========================================
            // FILTRO POR NIVEL
            // ==========================================

            if (nivel != "Todos")
            {
                expresion =
                    ".* " + nivel + " .*";
            }

            // ==========================================
            // FILTRO POR MÓDULO
            // ==========================================

            if (modulo != "Todos")
            {
                expresion =
                    ".* " +
                    nivelTextoParaRegex(nivel) +
                    " " +
                    Regex.Escape(modulo) +
                    " .*";
            }

            txtRegex.Text = expresion;
        }

        // ==========================================
        // TEXTO DE NIVEL
        // ==========================================

        private string nivelTextoParaRegex(
            string nivel)
        {
            if (nivel == "Todos")
            {
                return "(?:INFO|WARNING|ERROR|CRITICAL)";
            }

            return Regex.Escape(nivel);
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

                cmbModulo.Items.Clear();
                cmbModulo.Items.Add("Todos");
                cmbModulo.SelectedIndex = 0;

                dgvResultadosRegex.Rows.Clear();
                txtResumenRegex.Clear();

                return;
            }

            LogAnalysisResult resultado =
                LogAnalyzer.Analyze(
                    rutaArchivo);

            // ==========================================
            // INFORMACIÓN GENERAL
            // ==========================================

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
            // RESUMEN GENERAL
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

            // ==========================================
            // PORCENTAJES
            // ==========================================

            if (resultado.Total > 0)
            {
                double porcentajeValidos =
                    (resultado.Validos * 100.0) /
                    resultado.Total;

                double porcentajeInvalidos =
                    (resultado.Invalidos * 100.0) /
                    resultado.Total;

                txtResumen.AppendText(
                    "\r\nPORCENTAJES\r\n");

                txtResumen.AppendText(
                    "Válidos: " +
                    porcentajeValidos.ToString("0.00") +
                    "%\r\n");

                txtResumen.AppendText(
                    "Inválidos: " +
                    porcentajeInvalidos.ToString("0.00") +
                    "%\r\n");
            }

            // ==========================================
            // REGISTROS INVÁLIDOS
            // ==========================================

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
            // CARGAR MÓDULOS
            // ==========================================

            CargarModulos(resultado);

            // ==========================================
            // LIMPIAR RESULTADOS
            // ==========================================

            dgvResultadosRegex.Rows.Clear();

            lblCoincidencias.Text =
                "Coincidencias: 0";

            txtResumenRegex.Clear();

            // ==========================================
            // RESTABLECER FILTROS
            // ==========================================

            cmbNivel.SelectedIndex = 0;
            cmbModulo.SelectedIndex = 0;

            txtRegex.Text = ".*";
        }

        // ==========================================
        // CARGAR MÓDULOS
        // ==========================================

        private void CargarModulos(
            LogAnalysisResult resultado)
        {
            cmbModulo.Items.Clear();

            cmbModulo.Items.Add("Todos");

            HashSet<string> modulos =
                new HashSet<string>();

            foreach (LogEvent evento
                     in resultado.Eventos)
            {
                if (!string.IsNullOrWhiteSpace(
                    evento.Modulo))
                {
                    modulos.Add(evento.Modulo);
                }
            }

            List<string> listaModulos =
                new List<string>(modulos);

            listaModulos.Sort();

            foreach (string modulo
                     in listaModulos)
            {
                cmbModulo.Items.Add(modulo);
            }

            cmbModulo.SelectedIndex = 0;
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
            // VALIDAR REGEX
            // ==========================================

            Regex regex;

            try
            {
                regex =
                    new Regex(
                        expresion,
                        RegexOptions.Compiled);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    "La expresión regular no es válida.\r\n\r\n" +
                    "Detalle:\r\n" +
                    ex.Message,
                    "Error en expresión regular",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show(
                    "El archivo de log no existe.",
                    "Búsqueda Regex",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ==========================================
            // OBTENER FILTROS
            // ==========================================

            string nivel =
                cmbNivel.SelectedItem == null
                    ? "Todos"
                    : cmbNivel.SelectedItem.ToString();

            string modulo =
                cmbModulo.SelectedItem == null
                    ? "Todos"
                    : cmbModulo.SelectedItem.ToString();

            // ==========================================
            // BUSCAR
            // ==========================================

            List<string> resultados =
                LogRegexAnalyzer.Buscar(
                    rutaArchivo,
                    expresion,
                    nivel,
                    modulo);

            dgvResultadosRegex.Rows.Clear();

            // ==========================================
            // CONTADORES
            // ==========================================

            int info = 0;
            int warning = 0;
            int error = 0;
            int critical = 0;

            Dictionary<string, int> modulos =
                new Dictionary<string, int>();

            int numeroResultado = 1;

            // ==========================================
            // PROCESAR RESULTADOS
            // ==========================================

            foreach (string registro
                     in resultados)
            {
                LogEvent evento;
                string detalle;

                if (!LogParser.TryParse(
                    registro,
                    out evento,
                    out detalle))
                {
                    continue;
                }

                dgvResultadosRegex.Rows.Add(
                    numeroResultado,
                    evento.Nivel,
                    evento.Modulo,
                    evento.Mensaje,
                    registro);

                numeroResultado++;

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

            // ==========================================
            // COINCIDENCIAS
            // ==========================================

            int coincidencias =
                dgvResultadosRegex.Rows.Count;

            lblCoincidencias.Text =
                "Coincidencias: " +
                coincidencias;

            // ==========================================
            // RESUMEN
            // ==========================================

            txtResumenRegex.Clear();

            txtResumenRegex.AppendText(
                "ANÁLISIS DE RESULTADOS\r\n\r\n");

            txtResumenRegex.AppendText(
                "Expresión: " +
                expresion +
                "\r\n");

            txtResumenRegex.AppendText(
                "Nivel: " +
                nivel +
                "\r\n");

            txtResumenRegex.AppendText(
                "Módulo: " +
                modulo +
                "\r\n");

            txtResumenRegex.AppendText(
                "Coincidencias: " +
                coincidencias +
                "\r\n");

            // ==========================================
            // PORCENTAJE
            // ==========================================

            LogAnalysisResult analisisGeneral =
                LogAnalyzer.Analyze(
                    rutaArchivo);

            if (analisisGeneral.Validos > 0)
            {
                double porcentaje =
                    (coincidencias * 100.0) /
                    analisisGeneral.Validos;

                txtResumenRegex.AppendText(
                    "Porcentaje sobre registros válidos: " +
                    porcentaje.ToString("0.00") +
                    "%\r\n");
            }

            txtResumenRegex.AppendText(
                "\r\nNIVELES\r\n");

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

            // ==========================================
            // MÓDULOS
            // ==========================================

            txtResumenRegex.AppendText(
                "MÓDULOS ENCONTRADOS\r\n");

            if (modulos.Count == 0)
            {
                txtResumenRegex.AppendText(
                    "No se encontraron módulos.");
            }
            else
            {
                foreach (
                    KeyValuePair<string, int> moduloResultado
                    in modulos)
                {
                    txtResumenRegex.AppendText(
                        moduloResultado.Key +
                        ": " +
                        moduloResultado.Value +
                        "    ");
                }
            }
        }

        // ==========================================
        // LIMPIAR FILTROS
        // ==========================================

        private void btnLimpiarFiltros_Click(
            object sender,
            EventArgs e)
        {
            cmbNivel.SelectedIndex = 0;
            cmbModulo.SelectedIndex = 0;

            txtRegex.Text = ".*";

            dgvResultadosRegex.Rows.Clear();

            lblCoincidencias.Text =
                "Coincidencias: 0";

            txtResumenRegex.Clear();
        }

        // ==========================================
        // ABRIR ARCHIVO
        // ==========================================

        private void btnAbrirArchivo_Click(
            object sender,
            EventArgs e)
        {
            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show(
                    "El archivo no existe.",
                    "Archivo de log",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

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