using SimuladorAutomatas.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimuladorAutomatas.Logica;

namespace SimuladorAutomatas.Formularios
{
    public partial class FrmConversionAFD : Form
    {
        private AutomataEditor afd;
        private DibujadorAutomata dibujador;
        private Estado estadoMoviendo = null;
        private Point desplazamiento;

        public FrmConversionAFD(string procedimiento,AutomataEditor afd)
        {
            InitializeComponent();

            this.afd = afd;
            dibujador = new DibujadorAutomata();
            txtProcedimiento.Text = procedimiento;

            panelAFD.Paint += panelAFD_Paint;
            panelAFD.MouseDown += panelAFD_MouseDown;
            panelAFD.MouseMove += panelAFD_MouseMove;
            panelAFD.MouseUp += panelAFD_MouseUp;

            AcomodarEstados();

            AplicarEstilo();
        }


        private void AcomodarEstados()
        {
            int centroX = panelAFD.Width / 2;
            int centroY = panelAFD.Height / 2;

            int cantidad = afd.Estados.Count;

            if (cantidad == 0)
                return;

            if (cantidad == 1)
            {
                afd.Estados[0].X = centroX;
                afd.Estados[0].Y = centroY;
            }
            else if (cantidad == 2)
            {
                afd.Estados[0].X = centroX - 150;
                afd.Estados[0].Y = centroY;

                afd.Estados[1].X = centroX + 150;
                afd.Estados[1].Y = centroY;
            }
            else if (cantidad == 3)
            {
                afd.Estados[0].X = centroX;
                afd.Estados[0].Y = centroY - 130;

                afd.Estados[1].X = centroX - 170;
                afd.Estados[1].Y = centroY + 100;

                afd.Estados[2].X = centroX + 170;
                afd.Estados[2].Y = centroY + 100;
            }
            else
            {
                double radio = 180;

                for (int i = 0; i < cantidad; i++)
                {
                    double angulo =
                        (2 * Math.PI * i) / cantidad;

                    afd.Estados[i].X =
                        centroX +
                        (int)(radio * Math.Cos(angulo));

                    afd.Estados[i].Y =
                        centroY +
                        (int)(radio * Math.Sin(angulo));
                }
            }

            panelAFD.Invalidate();
        }

        private void panelAFD_MouseDown(
    object sender,
    MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            foreach (Estado estado in afd.Estados)
            {
                Rectangle area =
                    new Rectangle(
                        estado.X - 25,
                        estado.Y - 25,
                        50,
                        50
                    );

                if (area.Contains(e.Location))
                {
                    estadoMoviendo = estado;

                    desplazamiento =
                        new Point(
                            e.X - estado.X,
                            e.Y - estado.Y
                        );

                    break;
                }
            }
        }

        private void panelAFD_MouseMove(
    object sender,
    MouseEventArgs e)
        {
            if (estadoMoviendo == null)
                return;

            estadoMoviendo.X =
                e.X - desplazamiento.X;

            estadoMoviendo.Y =
                e.Y - desplazamiento.Y;

            panelAFD.Invalidate();
        }

        private void panelAFD_MouseUp(
    object sender,
    MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                estadoMoviendo = null;
            }
        }

        private void panelAFD_Paint(
    object sender,
    PaintEventArgs e)
        {
            dibujador.Dibujar(
                e.Graphics,
                afd,
                this.Font
            );
        }


        private void AplicarEstilo()
        {
            EstiloInterfaz.AplicarEstiloBoton(btnMinimizarAFD);

            EstiloInterfaz.AplicarEstiloFormulario(
                this
            );

            EstiloInterfaz.AplicarEstiloPanel(
                panelProcedimiento
            );

            EstiloInterfaz.AplicarEstiloPanel(
                panelAFD
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnValidarAFD
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnSimularAFD
            );

            EstiloInterfaz.AplicarEstiloTextBox(
                txtCadenaAFD
            );

            EstiloInterfaz.AplicarEstiloLabel(
                lblResultadoAFD
            );

            EstiloInterfaz.AplicarEstiloRichTextBox(
                txtProcedimiento
            );
        }


        private void FrmConversionAFD_Load(object sender, EventArgs e)
        {

        }

        private void txtProcedimiento_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelProcedimiento_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnValidarAFD_Click(object sender, EventArgs e)
        {
            SimuladorAFD simulador =
    new SimuladorAFD(afd);

            if (simulador.Validar())
            {
                MessageBox.Show(
                    "El AFD convertido es válido.",
                    "Validación"
                );
            }
            else
            {
                MessageBox.Show(
                    "El AFD convertido no es válido.",
                    "Validación"
                );
            }
        }

        private void btnSimularAFD_Click(object sender, EventArgs e)
        {
            SimuladorAFD simulador =
    new SimuladorAFD(afd);

            // Verificar que exista un alfabeto
            if (afd.Alfabeto.Count == 0)
            {
                lblResultadoAFD.Text =
                    "Resultado: Debe establecer el alfabeto antes de simular";

                return;
            }

            // Validar AFD
            if (!simulador.Validar())
            {
                lblResultadoAFD.Text =
                    "Resultado: El AFD convertido no es válido";

                return;
            }

            string cadena =
                txtCadenaAFD.Text.Trim();

            // Validar símbolos de la cadena
            List<string> simbolosInvalidos =
                new List<string>();

            foreach (char caracter in cadena)
            {
                string simbolo =
                    caracter.ToString();

                if (!afd.Alfabeto.Contains(simbolo) &&
                    !simbolosInvalidos.Contains(simbolo))
                {
                    simbolosInvalidos.Add(simbolo);
                }
            }

            if (simbolosInvalidos.Count > 0)
            {
                lblResultadoAFD.Text =
                    "Resultado: Símbolos inválidos: " +
                    string.Join(", ", simbolosInvalidos);

                return;
            }

            // Simular cadena
            bool aceptada =
                simulador.Simular(cadena);

            if (aceptada)
            {
                lblResultadoAFD.Text =
                    "Resultado: Cadena aceptada";
            }
            else
            {
                lblResultadoAFD.Text =
                    "Resultado: Cadena rechazada";
            }
        }

        private void btnMinimizarAFD_Click(object sender, EventArgs e)
        {
            // Verificar que exista un alfabeto
            if (afd.Alfabeto.Count == 0)
            {
                MessageBox.Show(
                    "El AFD convertido debe tener un alfabeto.",
                    "Minimización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Validar que el autómata convertido sea un AFD válido
            SimuladorAFD simulador = new SimuladorAFD(afd);

            if (!simulador.Validar())
            {
                MessageBox.Show(
                    "El AFD convertido no es válido y no puede minimizarse.",
                    "Minimización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Abrir la ventana de minimización usando el AFD convertido
            FrmMinimizacionAFD ventana = new FrmMinimizacionAFD(afd);

            ventana.ShowDialog();
        }
    }
}
