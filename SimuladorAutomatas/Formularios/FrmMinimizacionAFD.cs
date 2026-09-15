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

namespace SimuladorAutomatas.Formularios
{
    public partial class FrmMinimizacionAFD : Form
    {
        private AutomataEditor afd;
        private Dictionary<string, Label> celdasTabla;


        public FrmMinimizacionAFD(AutomataEditor afd)
        {
            InitializeComponent();

            this.afd = afd;

            celdasTabla =
                new Dictionary<string, Label>();

            CrearTabla();
        }

        private void CrearTabla()
        {
            panelTabla.Controls.Clear();

            txtProcedimientoMin.Dock =
                DockStyle.Bottom;

            txtProcedimientoMin.Height = 250;

            panelTabla.Controls.Add(
                txtProcedimientoMin
            );

            int inicioX = 70;
            int inicioY = 20;

            int anchoCelda = 70;
            int altoCelda = 45;

            // Encabezado superior
            for (int i = 0;
                i < afd.Estados.Count - 1;
                i++)
            {
                Label encabezado =
                    CrearEtiqueta(
                        afd.Estados[i].Nombre,
                        inicioX +
                        i * anchoCelda,
                        inicioY,
                        anchoCelda,
                        altoCelda
                    );

                panelTabla.Controls.Add(
                    encabezado
                );
            }

            // Encabezado lateral
            for (int i = 1;
                i < afd.Estados.Count;
                i++)
            {
                Label encabezado =
                    CrearEtiqueta(
                        afd.Estados[i].Nombre,
                        inicioX - anchoCelda,
                        inicioY +
                        i * altoCelda,
                        anchoCelda,
                        altoCelda
                    );

                panelTabla.Controls.Add(
                    encabezado
                );
            }

            // Crear celdas triangulares
            for (int fila = 1;
                fila < afd.Estados.Count;
                fila++)
            {
                for (int columna = 0;
                    columna < fila;
                    columna++)
                {
                    Estado estado1 =
                        afd.Estados[columna];

                    Estado estado2 =
                        afd.Estados[fila];

                    string clave =
                        ObtenerClave(
                            estado1,
                            estado2
                        );

                    Label celda =
                        CrearEtiqueta(
                            "",
                            inicioX +
                            columna * anchoCelda,
                            inicioY +
                            fila * altoCelda,
                            anchoCelda,
                            altoCelda
                        );

                    celdasTabla.Add(
                        clave,
                        celda
                    );

                    panelTabla.Controls.Add(
                        celda
                    );
                }
            }
        }

        private Label CrearEtiqueta(
            string texto,
            int x,
            int y,
            int ancho,
            int alto)
        {
            Label etiqueta =
                new Label();

            etiqueta.Text = texto;

            etiqueta.Location =
                new Point(x, y);

            etiqueta.Size =
                new Size(
                    ancho,
                    alto
                );

            etiqueta.BorderStyle =
                BorderStyle.FixedSingle;

            etiqueta.TextAlign =
                ContentAlignment.MiddleCenter;

            etiqueta.Font =
                new Font(
                    "Arial",
                    11,
                    FontStyle.Bold
                );

            return etiqueta;
        }

        private string ObtenerClave(
            Estado estado1,
            Estado estado2)
        {
            return estado1.Nombre +
                "|" +
                estado2.Nombre;
        }

        private void FrmMinimizacionAFD_Load(object sender, EventArgs e)
        {

        }
    }
}
