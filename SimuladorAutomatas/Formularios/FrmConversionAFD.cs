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

        public FrmConversionAFD(string procedimiento,AutomataEditor afd)
        {
            InitializeComponent();

            this.afd = afd;
            dibujador = new DibujadorAutomata();
            txtProcedimiento.Text = procedimiento;

            panelAFD.Paint += panelAFD_Paint;
            AplicarEstilo();
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
            SimuladorAFD simulador = new SimuladorAFD(afd);

            bool aceptada =
                simulador.Simular(
                    txtCadenaAFD.Text
                );

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

    }
}
