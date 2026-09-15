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
            List<Transicion> dibujadas =
                new List<Transicion>();

            foreach (Transicion transicion
                in automata.Transiciones)
            {
                if (dibujadas.Contains(transicion))
                    continue;

                DibujarTransicion(
                    g,
                    transicion,
                    automata,
                    fuente,
                    dibujadas
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
                using (Pen lapiz =
                    new Pen(
                        EstiloInterfaz.ColorLinea,
                        2))
                {
                    lapiz.CustomEndCap =
                        new System.Drawing.Drawing2D.AdjustableArrowCap(
                            5,
                            5
                        );

                    g.DrawLine(
                        lapiz,
                        x - 55,
                        y,
                        x - RADIO_ESTADO,
                        y
                    );
                }
            }

            using (Brush pincel =
                new SolidBrush(
                    EstiloInterfaz.ColorEstado))
            {
                g.FillEllipse(
                    pincel,
                    rectangulo
                );
            }

            using (Pen lapiz =
                new Pen(
                    EstiloInterfaz.CianActivo,
                    2))
            {
                g.DrawEllipse(
                    lapiz,
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
                        lapiz,
                        interior
                    );
                }
            }

            using (Brush pincel =
                new SolidBrush(
                    EstiloInterfaz.TextoPrincipal))
            {
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
                        pincel,
                        rectangulo,
                        formato
                    );
                }
            }
        }

        private void DibujarTransicion(
    Graphics g,
    Transicion transicion,
    AutomataEditor automata,
    Font fuente,
    List<Transicion> dibujadas)
        {
            Estado origen =
                transicion.Origen;

            Estado destino =
                transicion.Destino;

            if (origen == destino)
            {
                List<Transicion> bucles =
                    ObtenerGrupo(
                        origen,
                        destino,
                        automata
                    );

                DibujarBucle(
                    g,
                    origen,
                    bucles,
                    fuente
                );

                foreach (Transicion t
                    in bucles)
                {
                    dibujadas.Add(t);
                }

                return;
            }

            List<Transicion> grupo =
                ObtenerGrupo(
                    origen,
                    destino,
                    automata
                );

            foreach (Transicion t
                in grupo)
            {
                dibujadas.Add(t);
            }

            // Solo se dibuja una vez el grupo
            Transicion primera =
                grupo[0];

            if (transicion != primera)
                return;

            string simbolos =
                ObtenerSimbolos(
                    grupo
                );

            float x1 = origen.X;
            float y1 = origen.Y;

            float x2 = destino.X;
            float y2 = destino.Y;

            float dx = x2 - x1;
            float dy = y2 - y1;

            float distancia =
                (float)Math.Sqrt(
                    dx * dx +
                    dy * dy
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

            int inversas =
                ContarTransiciones(
                    automata,
                    destino,
                    origen
                );

            bool curva =
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

                float textoX =
                    (inicioX + finalX) / 2;

                float textoY =
                    (inicioY + finalY) / 2 - 15;

                DibujarTexto(
                    g,
                    simbolos,
                    textoX,
                    textoY,
                    fuente
                );

                return;
            }

            float px = -uy;
            float py = ux;

            float curvatura = 50;

            float medioX =
                (inicioX + finalX) / 2;

            float medioY =
                (inicioY + finalY) / 2;

            float controlX =
                medioX + px * curvatura;

            float controlY =
                medioY + py * curvatura;

            using (Pen pen =
                new Pen(
                    EstiloInterfaz.ColorLinea,
                    2))
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
                simbolos,
                medioX +
                px * curvatura * 0.65f,
                medioY +
                py * curvatura * 0.65f,
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
                new Pen(
                    EstiloInterfaz.ColorLinea,
                    2))
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
    Estado estado,
    List<Transicion> bucles,
    Font fuente)
        {
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
                new Pen(
                    EstiloInterfaz.ColorLinea,
                    2))
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

            string simbolos =
                ObtenerSimbolos(
                    bucles
                );

            DibujarTexto(
                g,
                simbolos,
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

            using (Brush pincel =
                new SolidBrush(
                    EstiloInterfaz.ColorSimbolo))
            {
                g.DrawString(
                    texto,
                    fuente,
                    pincel,
                    x - tamaño.Width / 2,
                    y - tamaño.Height / 2
                );
            }
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

        private List<Transicion> ObtenerGrupo(
            Estado origen,
            Estado destino,
            AutomataEditor automata)
        {
            List<Transicion> grupo =
                new List<Transicion>();

            foreach (Transicion transicion
                in automata.Transiciones)
            {
                if (transicion.Origen == origen &&
                    transicion.Destino == destino)
                {
                    grupo.Add(transicion);
                }
            }

            return grupo;
        }

        private string ObtenerSimbolos(
            List<Transicion> grupo)
        {
            List<string> simbolos =
                new List<string>();

            foreach (Transicion transicion
                in grupo)
            {
                if (!simbolos.Contains(
                    transicion.Simbolo))
                {
                    simbolos.Add(
                        transicion.Simbolo
                    );
                }
            }

            return string.Join(
                ",",
                simbolos
            );
        }
    }
}
