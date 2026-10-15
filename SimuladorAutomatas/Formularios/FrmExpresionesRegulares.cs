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
            if (automataGenerado == null)
            {
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
            if (afdGenerado == null)
            {
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

                // Ignorar las líneas vacías.
                if (string.IsNullOrWhiteSpace(linea))
                {
                    continue;
                }

                string resultado;
                string detalle;

                // Comprobar la estructura del log.
                Match coincidencia = Regex.Match(
                    linea,
                    @"^(INFO|WARNING|ERROR)\s+(\d{4}-\d{2}-\d{2})\s+(.+)$"
                );

                if (!coincidencia.Success)
                {
                    resultado = "INVÁLIDO";
                    detalle = "Formato incorrecto. Use NIVEL AAAA-MM-DD Mensaje.";
                    invalidos++;
                }
                else
                {
                    string fechaTexto = coincidencia.Groups[2].Value;

                    // Comprobar que la fecha exista realmente.
                    DateTime fecha;

                    bool fechaValida = DateTime.TryParseExact(
                        fechaTexto,
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out fecha
                    );

                    if (!fechaValida)
                    {
                        resultado = "INVÁLIDO";
                        detalle = "La fecha no es válida.";
                        invalidos++;
                    }
                    else
                    {
                        resultado = "VÁLIDO";
                        detalle = "El log cumple con el formato.";
                        validos++;
                    }
                }

                // Mostrar el resultado en la tabla.
                dgvResultadosLogs.Rows.Add(
                    i + 1,
                    lineaOriginal,
                    resultado,
                    detalle
                );
            }

            if (validos + invalidos == 0)
            {
                MessageBox.Show(
                    "Ingresa al menos un log para validar.",
                    "Validación de logs",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            lblResultadoRegex.Text =
                "Validación terminada. Válidos: " + validos
                + " | Inválidos: " + invalidos;

            lblResultadoRegex.ForeColor = invalidos == 0
                ? System.Drawing.Color.Green
                : System.Drawing.Color.Red;
        }
    }
}
