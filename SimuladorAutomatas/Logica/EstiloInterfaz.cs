using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorAutomatas.Logica
{
    public static class EstiloInterfaz
    {
        // Colores principales
        public static Color FondoPrincipal =
            Color.FromArgb(15, 23, 42);

        public static Color FondoPanel =
            Color.FromArgb(30, 41, 59);

        public static Color AzulTecnologico =
            Color.FromArgb(37, 99, 235);

        public static Color CianActivo =
            Color.FromArgb(6, 182, 212);

        public static Color TextoPrincipal =
            Color.White;

        public static Color TextoSecundario =
            Color.FromArgb(203, 213, 225);

        public static Color VerdeResultado =
            Color.FromArgb(34, 197, 94);

        public static Color RojoResultado =
            Color.FromArgb(239, 68, 68);

        // Colores del editor
        public static Color FondoEditor =
            Color.FromArgb(15, 23, 42);

        public static Color ColorEstado =
            Color.FromArgb(30, 41, 59);

        public static Color ColorLinea =
            Color.White;

        public static Color ColorSimbolo =
            Color.FromArgb(34, 211, 238);

        // Fuentes
        public static Font FuenteBoton =
            new Font(
                "Segoe UI",
                9,
                FontStyle.Bold
            );

        public static Font FuenteTitulo =
            new Font(
                "Segoe UI",
                11,
                FontStyle.Bold
            );

        // Formulario
        public static void AplicarEstiloFormulario(
            Form formulario)
        {
            formulario.BackColor =
                FondoPrincipal;

            formulario.ForeColor =
                TextoPrincipal;

            formulario.Font =
                new Font(
                    "Segoe UI",
                    9
                );
        }

        // Botón
        public static void AplicarEstiloBoton(
            Button boton)
        {
            boton.BackColor =
                AzulTecnologico;

            boton.ForeColor =
                TextoPrincipal;

            boton.FlatStyle =
                FlatStyle.Flat;

            boton.FlatAppearance.BorderSize =
                0;

            boton.Font =
                FuenteBoton;

            boton.Cursor =
                Cursors.Hand;

            boton.FlatAppearance.MouseOverBackColor =
                CianActivo;

            boton.FlatAppearance.MouseDownBackColor =
                CianActivo;
        }

        // Panel
        public static void AplicarEstiloPanel(
            Panel panel)
        {
            panel.BackColor =
                FondoPanel;
        }

        // TextBox
        public static void AplicarEstiloTextBox(
            TextBox texto)
        {
            texto.BackColor =
                Color.White;

            texto.ForeColor =
                Color.Black;

            texto.BorderStyle =
                BorderStyle.FixedSingle;
        }

        // RichTextBox
        public static void AplicarEstiloRichTextBox(
            RichTextBox texto)
        {
            texto.BackColor =
                FondoPanel;

            texto.ForeColor =
                TextoSecundario;

            texto.BorderStyle =
                BorderStyle.None;

            texto.Font =
                new Font(
                    "Consolas",
                    10
                );
        }

        // Label
        public static void AplicarEstiloLabel(
            Label etiqueta)
        {
            etiqueta.ForeColor =
                TextoPrincipal;
        }

        // Título
        public static void AplicarEstiloTitulo(
            Label etiqueta)
        {
            etiqueta.ForeColor =
                CianActivo;

            etiqueta.Font =
                FuenteTitulo;
        }
    }
}
