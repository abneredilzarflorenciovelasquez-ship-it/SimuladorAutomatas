using SimuladorAutomatas.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorAutomatas.Formularios
{
    public partial class FrmVisualizarAFNRegex : Form
    {
        private AutomataEditor automata;

        private const int RADIO = 25;
        private const int ESPACIO_X = 115;
        private const int ESPACIO_Y = 100;
        private const int MARGEN_X = 80;
        private const int MARGEN_Y = 100;

        public FrmVisualizarAFNRegex(AutomataEditor automata)
        {
            InitializeComponent();

            this.automata = automata;

            Text = "Visualización del AFN generado";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(800, 450);
            Size = new Size(814, 580);

            // Estilo del panel
            panelVisualizacion.Dock = DockStyle.Fill;
            panelVisualizacion.BackColor =
                Color.FromArgb(15, 25, 45);
            panelVisualizacion.AutoScroll = true;

            panelVisualizacion.Paint +=
                panelVisualizacion_Paint;

            panelVisualizacion.Resize +=
                (sender, e) => panelVisualizacion.Invalidate();

            PrepararPosiciones();
        }

        private void PrepararPosiciones()
        {
            if (automata == null || automata.Estados.Count == 0)
                return;

            Estado inicial = automata.EstadoInicial
                ?? automata.Estados.FirstOrDefault(e => e.EsInicial);

            Estado final = automata.EstadosFinales.FirstOrDefault()
                ?? automata.Estados.FirstOrDefault(e => e.EsFinal);

            // Separamos los estados inicial, final e intermedios.
            var intermedios = automata.Estados
                .Where(e => e != inicial && e != final)
                .ToList();

            int xInicial = 100;
            int xIntermedios = 250;
            int separacion = 150;
            int yCentro = 200;

            // El estado inicial queda a la izquierda.
            if (inicial != null)
            {
                inicial.X = xInicial;
                inicial.Y = yCentro;
            }

            // Distribuimos los estados intermedios en dos alturas.
            for (int i = 0; i < intermedios.Count; i++)
            {
                intermedios[i].X =
                    xIntermedios + i * separacion;

                intermedios[i].Y =
                    (i % 2 == 0) ? 140 : 260;
            }

            // El estado final queda a la derecha.
            int xFinal =
                xIntermedios + intermedios.Count * separacion;

            if (final != null)
            {
                final.X = xFinal;
                final.Y = yCentro;
            }

            // Ampliamos el área para permitir desplazamiento.
            int ancho = xFinal + 150;
            int alto = 400;

            panelVisualizacion.AutoScrollMinSize =
                new Size(ancho, alto);
        }

        private void panelVisualizacion_Paint(
            object sender,
            PaintEventArgs e)
        {
            if (automata == null)
                return;

            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            // Fondo oscuro igual al simulador principal.
            g.Clear(Color.FromArgb(15, 25, 45));

            // El dibujo utiliza coordenadas relativas al desplazamiento.
            GraphicsState estadoGrafico = g.Save();

            g.TranslateTransform(
                panelVisualizacion.AutoScrollPosition.X,
                panelVisualizacion.AutoScrollPosition.Y);

            // Título.
            using (Font titulo = new Font(
                "Segoe UI",
                14,
                FontStyle.Bold))
            using (Brush pincelTitulo =
                new SolidBrush(
                    Color.FromArgb(0, 205, 230)))
            {
                g.DrawString(
                    "Autómata Finito No Determinista generado",
                    titulo,
                    pincelTitulo,
                    MARGEN_X,
                    20);
            }

            // Primero se dibujan las transiciones.
            foreach (Transicion transicion
                in automata.Transiciones)
            {
                DibujarTransicion(
                    g,
                    transicion);
            }

            // Después se dibujan los estados encima de las líneas.
            foreach (Estado estado
                in automata.Estados)
            {
                DibujarEstado(
                    g,
                    estado);
            }

            g.Restore(estadoGrafico);
        }

        private void DibujarTransicion(
            Graphics g,
            Transicion transicion)
        {
            Estado origen = transicion.Origen;
            Estado destino = transicion.Destino;

            if (origen == null || destino == null)
                return;

            // Transiciones blancas y símbolos cyan.
            using (Pen lapiz = new Pen(
                Color.White,
                1.8f))
            using (Font fuente = new Font(
                "Segoe UI",
                9,
                FontStyle.Bold))
            using (Brush pincelTexto =
                new SolidBrush(
                    Color.FromArgb(0, 205, 230)))
            {
                lapiz.CustomEndCap =
                    new AdjustableArrowCap(5, 6);

                // Si es un bucle.
                if (origen == destino)
                {
                    DibujarBucle(
                        g,
                        origen,
                        transicion.Simbolo,
                        lapiz,
                        fuente,
                        pincelTexto);

                    return;
                }

                float dx =
                    destino.X - origen.X;

                float dy =
                    destino.Y - origen.Y;

                float distancia =
                    (float)Math.Sqrt(
                        dx * dx +
                        dy * dy);

                if (distancia < 1)
                    return;

                float ux =
                    dx / distancia;

                float uy =
                    dy / distancia;

                float x1 =
                    origen.X + ux * RADIO;

                float y1 =
                    origen.Y + uy * RADIO;

                float x2 =
                    destino.X - ux * RADIO;

                float y2 =
                    destino.Y - uy * RADIO;

                bool inversa =
                    automata.Transiciones.Any(t =>
                        t.Origen == destino &&
                        t.Destino == origen);

                float medioX =
                    (x1 + x2) / 2;

                float medioY =
                    (y1 + y2) / 2;

                if (inversa)
                {
                    float curva = 22;

                    float controlX =
                        medioX - uy * curva;

                    float controlY =
                        medioY + ux * curva;

                    g.DrawBezier(
                        lapiz,
                        x1,
                        y1,
                        controlX - uy * curva,
                        controlY + ux * curva,
                        controlX,
                        controlY,
                        x2,
                        y2);

                    medioX =
                        (x1
                        + 2 * (controlX - uy * curva)
                        + 2 * (controlX - uy * curva)
                        + x2) / 6;

                    medioY =
                        (y1
                        + 2 * (controlY + ux * curva)
                        + 2 * (controlY + ux * curva)
                        + y2) / 6;
                }
                else
                {
                    g.DrawLine(
                        lapiz,
                        x1,
                        y1,
                        x2,
                        y2);
                }

                string etiqueta =
                    transicion.Simbolo;

                SizeF medida =
                    g.MeasureString(
                        etiqueta,
                        fuente);

                RectangleF fondo =
                    new RectangleF(
                        medioX - medida.Width / 2 - 3,
                        medioY - medida.Height / 2 - 2,
                        medida.Width + 6,
                        medida.Height + 4);

                // Fondo del mismo color que el panel.
                using (Brush fondoSimbolo =
                    new SolidBrush(
                        Color.FromArgb(15, 25, 45)))
                {
                    g.FillRectangle(
                        fondoSimbolo,
                        fondo);
                }

                g.DrawString(
                    etiqueta,
                    fuente,
                    pincelTexto,
                    medioX - medida.Width / 2,
                    medioY - medida.Height / 2);
            }
        }

        private void DibujarBucle(
            Graphics g,
            Estado estado,
            string simbolo,
            Pen lapiz,
            Font fuente,
            Brush pincelTexto)
        {
            Rectangle bucle =
                new Rectangle(
                    estado.X - 16,
                    estado.Y - RADIO - 27,
                    32,
                    30);

            g.DrawArc(
                lapiz,
                bucle,
                0,
                300);

            g.DrawLine(
                lapiz,
                estado.X + 13,
                estado.Y - RADIO - 9,
                estado.X + 9,
                estado.Y - RADIO - 17);

            g.DrawString(
                simbolo,
                fuente,
                pincelTexto,
                estado.X - 12,
                estado.Y - RADIO - 45);
        }

        private void DibujarEstado(
            Graphics g,
            Estado estado)
        {
            Rectangle circulo =
                new Rectangle(
                    estado.X - RADIO,
                    estado.Y - RADIO,
                    RADIO * 2,
                    RADIO * 2);

            // Fondo oscuro y borde cyan.
            using (Brush fondo =
                new SolidBrush(
                    Color.FromArgb(15, 25, 45)))
            using (Pen borde =
                new Pen(
                    Color.FromArgb(0, 205, 230),
                    2))
            using (Font fuente =
                new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Bold))
            using (Brush pincelTexto =
                new SolidBrush(
                    Color.White))
            {
                g.FillEllipse(
                    fondo,
                    circulo);

                g.DrawEllipse(
                    borde,
                    circulo);

                // Estado final: doble círculo.
                if (estado.EsFinal)
                {
                    Rectangle interior =
                        new Rectangle(
                            circulo.X + 5,
                            circulo.Y + 5,
                            circulo.Width - 10,
                            circulo.Height - 10);

                    g.DrawEllipse(
                        borde,
                        interior);
                }

                SizeF medida =
                    g.MeasureString(
                        estado.Nombre,
                        fuente);

                g.DrawString(
                    estado.Nombre,
                    fuente,
                    pincelTexto,
                    estado.X - medida.Width / 2,
                    estado.Y - medida.Height / 2);
            }

            // Flecha del estado inicial.
            if (estado.EsInicial)
            {
                using (Pen lapiz =
                    new Pen(
                        Color.White,
                        2))
                {
                    lapiz.CustomEndCap =
                        new AdjustableArrowCap(
                            5,
                            6);

                    g.DrawLine(
                        lapiz,
                        estado.X - RADIO - 35,
                        estado.Y,
                        estado.X - RADIO - 3,
                        estado.Y);
                }
            }
        }
    }
}