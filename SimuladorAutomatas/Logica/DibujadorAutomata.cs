using SimuladorAutomatas.Modelos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Logica
{
    public class DibujadorAutomata
    {
        private const int RADIO_ESTADO = 25;

        public void Dibujar(
            Graphics g,
            AutomataEditor automata,
            Font fuente)
        {
            foreach (Transicion transicion in automata.Transiciones)
            {
                DibujarTransicion(
                    g,
                    transicion,
                    automata,
                    fuente
                );
            }

            foreach (Estado estado in automata.Estados)
            {
                DibujarEstado(
                    g,
                    estado,
                    fuente
                );
            }
        }

        private void DibujarEstado(
            Graphics g,
            Estado estado,
            Font fuente)
        {
            int x = estado.X;
            int y = estado.Y;

            Rectangle rectangulo =
                new Rectangle(
                    x - RADIO_ESTADO,
                    y - RADIO_ESTADO,
                    RADIO_ESTADO * 2,
                    RADIO_ESTADO * 2
                );

            if (estado.EsInicial)
            {
                g.DrawLine(
                    Pens.Black,
                    x - 55,
                    y,
                    x - RADIO_ESTADO,
                    y
                );

                Point[] flecha =
                {
                    new Point(
                        x - RADIO_ESTADO,
                        y
                    ),
                    new Point(
                        x - RADIO_ESTADO - 8,
                        y - 5
                    ),
                    new Point(
                        x - RADIO_ESTADO - 8,
                        y + 5
                    )
                };

                g.FillPolygon(
                    Brushes.Black,
                    flecha
                );
            }

            g.DrawEllipse(
                Pens.Black,
                rectangulo
            );

            if (estado.EsFinal)
            {
                Rectangle interior =
                    new Rectangle(
                        x - RADIO_ESTADO + 5,
                        y - RADIO_ESTADO + 5,
                        (RADIO_ESTADO - 5) * 2,
                        (RADIO_ESTADO - 5) * 2
                    );

                g.DrawEllipse(
                    Pens.Black,
                    interior
                );
            }

            using (StringFormat formato =
                new StringFormat())
            {
                formato.Alignment =
                    StringAlignment.Center;

                formato.LineAlignment =
                    StringAlignment.Center;

                g.DrawString(
                    estado.Nombre,
                    fuente,
                    Brushes.Black,
                    rectangulo,
                    formato
                );
            }
        }

        private void DibujarTransicion(
            Graphics g,
            Transicion transicion,
            AutomataEditor automata,
            Font fuente)
        {
            Estado origen = transicion.Origen;
            Estado destino = transicion.Destino;

            if (origen == destino)
            {
                DibujarBucle(
                    g,
                    transicion,
                    fuente
                );

                return;
            }

            float x1 = origen.X;
            float y1 = origen.Y;

            float x2 = destino.X;
            float y2 = destino.Y;

            float dx = x2 - x1;
            float dy = y2 - y1;

            float distancia =
                (float)System.Math.Sqrt(
                    dx * dx + dy * dy
                );

            if (distancia == 0)
                return;

            float ux = dx / distancia;
            float uy = dy / distancia;

            float inicioX =
                x1 + ux * RADIO_ESTADO;

            float inicioY =
                y1 + uy * RADIO_ESTADO;

            float finalX =
                x2 - ux * RADIO_ESTADO;

            float finalY =
                y2 - uy * RADIO_ESTADO;

            int cantidad =
                ContarTransiciones(
                    automata,
                    origen,
                    destino
                );

            int inversas =
                ContarTransiciones(
                    automata,
                    destino,
                    origen
                );

            bool curva =
                cantidad > 1 ||
                inversas > 0;

            if (!curva)
            {
                DibujarLinea(
                    g,
                    inicioX,
                    inicioY,
                    finalX,
                    finalY
                );
            }
            else
            {
                float px = -uy;
                float py = ux;

                float curvatura = 50;

                if (inversas > 0 &&
                    cantidad == 1)
                {
                    curvatura = 50;
                }

                float medioX =
                    (inicioX + finalX) / 2;

                float medioY =
                    (inicioY + finalY) / 2;

                float controlX =
                    medioX + px * curvatura;

                float controlY =
                    medioY + py * curvatura;

                using (Pen pen =
                    new Pen(Color.Black, 1))
                {
                    pen.CustomEndCap =
                        new System.Drawing.Drawing2D.AdjustableArrowCap(
                            5,
                            5
                        );

                    g.DrawBezier(
                        pen,
                        inicioX,
                        inicioY,
                        controlX,
                        controlY,
                        controlX,
                        controlY,
                        finalX,
                        finalY
                    );
                }

                DibujarTextoCurva(
                    g,
                    transicion.Simbolo,
                    medioX + px * curvatura * 0.65f,
                    medioY + py * curvatura * 0.65f,
                    fuente
                );

                return;
            }

            using (Pen pen =
                new Pen(Color.Black, 1))
            {
                pen.CustomEndCap =
                    new System.Drawing.Drawing2D.AdjustableArrowCap(
                        5,
                        5
                    );

                g.DrawLine(
                    pen,
                    inicioX,
                    inicioY,
                    finalX,
                    finalY
                );
            }

            float textoX =
                (inicioX + finalX) / 2;

            float textoY =
                (inicioY + finalY) / 2;

            DibujarTexto(
                g,
                transicion.Simbolo,
                textoX,
                textoY,
                fuente
            );
        }

        private void DibujarLinea(
            Graphics g,
            float x1,
            float y1,
            float x2,
            float y2)
        {
            using (Pen pen =
                new Pen(Color.Black, 1))
            {
                pen.CustomEndCap =
                    new System.Drawing.Drawing2D.AdjustableArrowCap(
                        5,
                        5
                    );

                g.DrawLine(
                    pen,
                    x1,
                    y1,
                    x2,
                    y2
                );
            }
        }

        private void DibujarBucle(
            Graphics g,
            Transicion transicion,
            Font fuente)
        {
            Estado estado = transicion.Origen;

            int x = estado.X;
            int y = estado.Y;

            Rectangle bucle =
                new Rectangle(
                    x - 20,
                    y - 55,
                    40,
                    40
                );

            using (Pen pen =
                new Pen(Color.Black, 1))
            {
                pen.CustomEndCap =
                    new System.Drawing.Drawing2D.AdjustableArrowCap(
                        5,
                        5
                    );

                g.DrawArc(
                    pen,
                    bucle,
                    180,
                    270
                );
            }

            DibujarTexto(
                g,
                transicion.Simbolo,
                x,
                y - 65,
                fuente
            );
        }

        private void DibujarTexto(
            Graphics g,
            string texto,
            float x,
            float y,
            Font fuente)
        {
            SizeF tamaño =
                g.MeasureString(
                    texto,
                    fuente
                );

            g.DrawString(
                texto,
                fuente,
                Brushes.Black,
                x - tamaño.Width / 2,
                y - tamaño.Height / 2
            );
        }

        private void DibujarTextoCurva(
            Graphics g,
            string texto,
            float x,
            float y,
            Font fuente)
        {
            DibujarTexto(
                g,
                texto,
                x,
                y,
                fuente
            );
        }

        private int ContarTransiciones(
            AutomataEditor automata,
            Estado origen,
            Estado destino)
        {
            int cantidad = 0;

            foreach (Transicion transicion
                in automata.Transiciones)
            {
                if (transicion.Origen == origen &&
                    transicion.Destino == destino)
                {
                    cantidad++;
                }
            }

            return cantidad;
        }
    }
}
