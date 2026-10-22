using SimuladorAutomatas.Logica;
using SimuladorAutomatas.Modelos;
using SimuladorAutomatas.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SimuladorAutomatas.Formularios
{
    public partial class FrmExpresionesRegulares : Form
    {

        private AutomataEditor automataGenerado;
        private SimuladorAFN simuladorAFN;
        private AutomataEditor afdGenerado;

        public FrmExpresionesRegulares()
        {
            InitializeComponent();

            dgvRegistro.Columns.Clear();
            dgvRegistro.Columns.Add("Hora", "Hora");
            dgvRegistro.Columns.Add("Cadena", "Cadena ingresada");
            dgvRegistro.Columns.Add("Resultado", "Resultado");

            dgvRegistro.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvRegistro.AllowUserToAddRows = false;
            dgvRegistro.ReadOnly = true;


            dgvResultadosLogs.Columns.Clear();
            dgvResultadosLogs.Columns.Add("Linea", "Línea");
            dgvResultadosLogs.Columns.Add("Log", "Log ingresado");
            dgvResultadosLogs.Columns.Add("Resultado", "Resultado");
            dgvResultadosLogs.Columns.Add("Detalle", "Detalle de validación");

            dgvResultadosLogs.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvResultadosLogs.AllowUserToAddRows = false;
            dgvResultadosLogs.ReadOnly = true;
            dgvResultadosLogs.RowHeadersVisible = false;
            dgvResultadosLogs.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvResultadosLogs.MultiSelect = false;

            AplicarEstilo();

        }

        private void AplicarEstilo()
        {
            // Formulario
            EstiloInterfaz.AplicarEstiloFormulario(this);

            // Botones
            EstiloInterfaz.AplicarEstiloBoton(btnValidarExpresion);
            EstiloInterfaz.AplicarEstiloBoton(btnGenerarAutomata);
            EstiloInterfaz.AplicarEstiloBoton(btnConvertirAFDRegex);
            EstiloInterfaz.AplicarEstiloBoton(btnMinimizarAFDRegex);
            EstiloInterfaz.AplicarEstiloBoton(btnValidarCadena);
            EstiloInterfaz.AplicarEstiloBoton(btnValidarLogs);

            // Labels
            EstiloInterfaz.AplicarEstiloLabel(label1);
            EstiloInterfaz.AplicarEstiloLabel(label2);
            EstiloInterfaz.AplicarEstiloLabel(label3);
            EstiloInterfaz.AplicarEstiloLabel(label4);
            EstiloInterfaz.AplicarEstiloLabel(lblResultadoRegex);
            EstiloInterfaz.AplicarEstiloLabel(label5);
            EstiloInterfaz.AplicarEstiloLabel(lblLogs);

            // Títulos principales
            EstiloInterfaz.AplicarEstiloTitulo(label1);
            EstiloInterfaz.AplicarEstiloTitulo(label5);

            // TextBox
            EstiloInterfaz.AplicarEstiloTextBox(txtExpresion);
            EstiloInterfaz.AplicarEstiloTextBox(txtCadenaRegex);
            EstiloInterfaz.AplicarEstiloTextBox(txtLogs);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnValidarExpresion_Click(object sender, EventArgs e)
        {
            string expresion = txtExpresion.Text;

            ValidadorExpresionRegular validador =
                new ValidadorExpresionRegular();

            string mensaje;
            bool esValida = validador.Validar(expresion, out mensaje);

            LogService.Info("REGEX", esValida ? "Expresion regular valida" : "Expresion regular invalida");

            if (esValida)
            {
                lblResultadoRegex.Text = mensaje;
                lblResultadoRegex.ForeColor =
                    System.Drawing.Color.Green;
            }
            else
            {
                lblResultadoRegex.Text = mensaje;
                lblResultadoRegex.ForeColor =
                    System.Drawing.Color.Red;
            }

        }

        private void btnGenerarAutomata_Click(object sender, EventArgs e)
        {
            string expresion = txtExpresion.Text;

            try
            {
                ConvertidorRegexAFN convertidor =
                    new ConvertidorRegexAFN();

                // Guardamos el autómata en la variable del formulario.
                automataGenerado = convertidor.Convertir(expresion);
                LogService.Info("REGEX", "Expresion regular convertida a AFN");

                // Creamos el simulador para validar cadenas.
                simuladorAFN = new SimuladorAFN(automataGenerado);

                // Abrimos el visualizador del AFN generado.
                FrmVisualizarAFNRegex visualizador =
                    new FrmVisualizarAFNRegex(automataGenerado);

                visualizador.Show();

                lblResultadoRegex.Text =
                    "AFN generado correctamente. Estados: "
                    + automataGenerado.Estados.Count
                    + ", transiciones: "
                    + automataGenerado.Transiciones.Count + ".";

                lblResultadoRegex.ForeColor =
                    System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                LogService.Error(
    "REGEX",
    "Error al convertir expresion regular a AFN",
    ex);
                // Evitamos validar con un autómata anterior si ocurre un error.
                automataGenerado = null;
                simuladorAFN = null;

                lblResultadoRegex.Text =
                    "Error al generar el autómata: " + ex.Message;

                lblResultadoRegex.ForeColor =
                    System.Drawing.Color.Red;
            }
        }

        private void btnValidarCadena_Click(object sender, EventArgs e)
        {
            // Comprobar que ya se generó un autómata.
            if (automataGenerado == null || simuladorAFN == null)
            {
                MessageBox.Show(
                    "Primero debes generar el autómata.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Obtener la cadena ingresada.
            string cadena = txtCadenaRegex.Text;

            // Simular la cadena con el AFN.
            bool aceptada = simuladorAFN.Simular(cadena);
            LogService.Info("REGEX", "Simulacion de cadena en AFN: " + (aceptada ? "aceptada" : "rechazada"));

            string resultado = aceptada ? "ACEPTADA" : "RECHAZADA";

            // Mostrar el resultado en pantalla.
            lblResultadoRegex.Text = "Cadena " + resultado;

            lblResultadoRegex.ForeColor = aceptada
                ? System.Drawing.Color.Green
                : System.Drawing.Color.Red;

            // Mostrar un texto identificable si la cadena está vacía.
            string cadenaMostrar = string.IsNullOrEmpty(cadena)
                ? "(cadena vacía)"
                : cadena;

            // Registrar la prueba en la tabla.
            dgvRegistro.Rows.Add(
                DateTime.Now.ToString("HH:mm:ss"),
                cadenaMostrar,
                resultado);
        }

        private void btnConvertirAFDRegex_Click(object sender, EventArgs e)
        {
            LogService.Info(
    "REGEX",
    "Inicio de conversion de AFN generado desde regex a AFD");
            if (automataGenerado == null)
            {
                LogService.Warning(
                    "REGEX",
                    "Conversion a AFD cancelada: no se ha generado el automata AFN");

                MessageBox.Show(
                    "Primero genera el autómata desde una expresión regular.",
                    "Conversión a AFD",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                SimuladorAFN simulador =
                    new SimuladorAFN(automataGenerado);

                if (!simulador.Validar())
                {
                    MessageBox.Show(
                        "El AFN generado no es válido y no puede convertirse.",
                        "Conversión a AFD",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                ConvertidorAFNaAFD convertidor =
                    new ConvertidorAFNaAFD(automataGenerado);

                ResultadoConversion resultado =
                    convertidor.Convertir();

                afdGenerado = resultado.AFD;
                LogService.Info("REGEX", "AFN generado desde regex convertido a AFD");

                FrmConversionAFD ventana =
                    new FrmConversionAFD(
                        resultado.Procedimiento,
                        resultado.AFD);

                ventana.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al convertir el AFN a AFD: " + ex.Message,
                    "Conversión a AFD",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMinimizarAFDRegex_Click(object sender, EventArgs e)
        {
            LogService.Info(
    "REGEX",
    "Inicio de minimizacion de AFD generado desde regex");

            if (afdGenerado == null)
            {
                LogService.Warning(
    "REGEX",
    "Minimizacion cancelada: no se ha generado un AFD");

                MessageBox.Show(
                    "Primero debes generar el AFN y convertirlo a AFD.",
                    "Minimización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            SimuladorAFD simulador = new SimuladorAFD(afdGenerado);

            if (!simulador.Validar())
            {
                LogService.Warning(
                    "REGEX",
                    "Minimizacion cancelada: el AFD generado no es valido");

                MessageBox.Show(
                    "El AFD generado no es válido.",
                    "Minimización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            FrmMinimizacionAFD ventana =
                new FrmMinimizacionAFD(afdGenerado);

            ventana.ShowDialog();

            LogService.Info(
                "REGEX",
                "AFD generado desde regex minimizado");
        }

        private void btnValidarLogs_Click(object sender, EventArgs e)
        {
            dgvResultadosLogs.Rows.Clear();

            int validos = 0;
            int invalidos = 0;

            string[] lineas = txtLogs.Lines;

            for (int i = 0; i < lineas.Length; i++)
            {
                string lineaOriginal = lineas[i];
                string linea = lineaOriginal.Trim();

                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                LogEvent evento;
                string detalle;
                bool esValido = LogParser.TryParse(linea, out evento, out detalle);

                string resultado = esValido ? "VÁLIDO" : "INVÁLIDO";
                if (esValido) validos++; else invalidos++;

                int indiceFila = dgvResultadosLogs.Rows.Add(
                    i + 1,
                    lineaOriginal,
                    resultado,
                    detalle
                );

                DataGridViewRow fila = dgvResultadosLogs.Rows[indiceFila];
                if (esValido)
                {
                    fila.DefaultCellStyle.BackColor = System.Drawing.Color.Honeydew;
                    fila.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                }
                else
                {
                    fila.DefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
                    fila.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
                }
            }

            if (validos + invalidos == 0)
            {
                LogService.Warning(
                    "LOG",
                    "Validacion manual cancelada: no se ingresaron logs");
                MessageBox.Show(
                    "Ingresa al menos un log para validar.",
                    "Validación de logs",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            LogService.Info("LOG", "Validacion manual de logs: " + validos + " validos, " + invalidos + " invalidos");

            lblResultadoRegex.Text =
                "Validación terminada. Válidos: " + validos
                + " | Inválidos: " + invalidos;

            lblResultadoRegex.ForeColor = invalidos == 0
                ? System.Drawing.Color.Green
                : System.Drawing.Color.Red;
        }
    }
}
