using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using SimuladorAutomatas.Modelos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorAutomatas
{
    public partial class Form1 : Form
    {

        private AutomataEditor automata;
        private const int RADIO_ESTADO = 25;

        private Estado estadoSeleccionado;
        private bool moviendoEstado;

        private bool estadoFueMovido;
        private bool modoEstadoInicial;
        private bool modoEstadoFinal;

        //Transiciones
        private bool modoTransicion;
        private Estado estadoOrigenTransicion;

        public Form1()
        {
            InitializeComponent();
            automata = new AutomataEditor();
        }

        private void panelEditor_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Primero dibujar las transiciones
            foreach (Transicion transicion in automata.Transiciones)
            {
                DibujarTransicion(g, transicion);
            }

            // Después dibujar los estados
            foreach (Estado estado in automata.Estados)
            {
                DibujarEstado(g, estado);
            }
        }

        private void DibujarEstado(Graphics g, Estado estado)
        {
            int diametro = RADIO_ESTADO * 2;

            int x = estado.X - RADIO_ESTADO;
            int y = estado.Y - RADIO_ESTADO;

            Rectangle circulo = new Rectangle(
                x,
                y,
                diametro,
                diametro
            );

            // Dibujar círculo del estado
            using (Pen lapiz = new Pen(Color.Black, 2))
            {
                g.DrawEllipse(lapiz, circulo);

                // Si es estado final, dibujar segundo círculo
                if (estado.EsFinal)
                {
                    int margen = 5;

                    Rectangle segundoCirculo = new Rectangle(
                        x + margen,
                        y + margen,
                        diametro - margen * 2,
                        diametro - margen * 2
                    );

                    g.DrawEllipse(lapiz, segundoCirculo);
                }
            }

            // Dibujar nombre del estado
            using (Brush pincel = new SolidBrush(Color.Black))
            {
                StringFormat formato = new StringFormat();

                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;

                g.DrawString(
                    estado.Nombre,
                    this.Font,
                    pincel,
                    circulo,
                    formato
                );
            }

            // Dibujar flecha si es el estado inicial
            if (estado.EsInicial)
            {
                using (Pen lapizFlecha = new Pen(Color.Black, 2))
                {
                    lapizFlecha.CustomEndCap =
                        new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

                    g.DrawLine(
                        lapizFlecha,
                        estado.X - RADIO_ESTADO - 35,
                        estado.Y,
                        estado.X - RADIO_ESTADO,
                        estado.Y
                    );
                }
            }
        }

        private void DibujarTransicion(Graphics g, Transicion transicion)
        {
            Estado origen = transicion.Origen;
            Estado destino = transicion.Destino;

            // Diferencia entre las posiciones
            float dx = destino.X - origen.X;
            float dy = destino.Y - origen.Y;

            double distancia = Math.Sqrt(dx * dx + dy * dy);

            // Evitar división entre cero
            if (distancia == 0)
                return;

            // Vector normalizado
            float nx = dx / (float)distancia;
            float ny = dy / (float)distancia;

            // Punto donde empieza la línea
            float inicioX = origen.X + nx * RADIO_ESTADO;
            float inicioY = origen.Y + ny * RADIO_ESTADO;

            // Punto donde termina la línea
            float finalX = destino.X - nx * RADIO_ESTADO;
            float finalY = destino.Y - ny * RADIO_ESTADO;

            using (Pen lapiz = new Pen(Color.Black, 2))
            {
                lapiz.CustomEndCap =
                    new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

                g.DrawLine(
                    lapiz,
                    inicioX,
                    inicioY,
                    finalX,
                    finalY
                );
            }

            // Dibujar símbolo
            float medioX = (inicioX + finalX) / 2;
            float medioY = (inicioY + finalY) / 2;

            using (Brush pincel = new SolidBrush(Color.Black))
            {
                StringFormat formato = new StringFormat();

                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;

                RectangleF areaTexto = new RectangleF(
                    medioX - 20,
                    medioY - 15,
                    40,
                    30
                );

                g.DrawString(
                    transicion.Simbolo,
                    this.Font,
                    pincel,
                    areaTexto,
                    formato
                );
            }
        }

        private void panelEditor_MouseClick(object sender, MouseEventArgs e)
        {
            // Clic derecho = eliminar estado
            if (e.Button == MouseButtons.Right)
            {
                foreach (Estado estado in automata.Estados)
                {
                    if (EstaDentroDelEstado(estado, e.X, e.Y))
                    {
                        // Si el estado eliminado era el inicial
                        if (automata.EstadoInicial == estado)
                        {
                            automata.EstadoInicial = null;
                        }

                        // Si era un estado final, quitarlo de la lista
                        automata.EstadosFinales.Remove(estado);

                        // Eliminar el estado
                        automata.Estados.Remove(estado);

                        panelEditor.Invalidate();
                        return;
                    }
                }

                return;
            }

            // Si no es clic izquierdo, no hacemos nada
            if (e.Button != MouseButtons.Left)
                return;

            // -----------------------------------------
            // MODO ESTADO INICIAL
            // -----------------------------------------

            if (modoEstadoInicial)
            {
                foreach (Estado estado in automata.Estados)
                {
                    if (EstaDentroDelEstado(estado, e.X, e.Y))
                    {
                        // Quitar estado inicial anterior
                        if (automata.EstadoInicial != null)
                        {
                            automata.EstadoInicial.EsInicial = false;
                        }

                        // Establecer nuevo estado inicial
                        automata.EstadoInicial = estado;
                        estado.EsInicial = true;

                        modoEstadoInicial = false;

                        panelEditor.Invalidate();
                        return;
                    }
                }

                return;
            }

            // -----------------------------------------
            // MODO ESTADO FINAL
            // -----------------------------------------

            if (modoEstadoFinal)
            {
                foreach (Estado estado in automata.Estados)
                {
                    if (EstaDentroDelEstado(estado, e.X, e.Y))
                    {
                        // Si no es final, agregarlo
                        if (!estado.EsFinal)
                        {
                            estado.EsFinal = true;
                            automata.EstadosFinales.Add(estado);
                        }
                        else
                        {
                            // Si ya era final, quitarlo
                            estado.EsFinal = false;
                            automata.EstadosFinales.Remove(estado);
                        }

                        panelEditor.Invalidate();
                        return;
                    }
                }

                return;
            }

            // -----------------------------------------
            // MODO TRANSICIÓN
            // -----------------------------------------

            if (modoTransicion)
            {
                foreach (Estado estado in automata.Estados)
                {
                    if (EstaDentroDelEstado(estado, e.X, e.Y))
                    {
                        // Primer clic: seleccionar origen
                        if (estadoOrigenTransicion == null)
                        {
                            estadoOrigenTransicion = estado;
                            return;
                        }

                        // Segundo clic: seleccionar destino
                        Estado destino = estado;

                        // Pedir símbolo
                        string simbolo = Microsoft.VisualBasic.Interaction.InputBox(
                            "Ingrese el símbolo de la transición:",
                            "Nueva transición",
                            ""
                        );

                        // Si el usuario cancela o deja vacío
                        if (string.IsNullOrWhiteSpace(simbolo))
                        {
                            estadoOrigenTransicion = null;
                            modoTransicion = false;
                            return;
                        }

                        // Crear transición
                        Transicion nuevaTransicion = new Transicion(
                            estadoOrigenTransicion,
                            simbolo,
                            destino
                        );

                        automata.AgregarTransicion(nuevaTransicion);

                        // Agregar símbolo al alfabeto
                        automata.AgregarSimbolo(simbolo);

                        // Reiniciar modo
                        estadoOrigenTransicion = null;
                        modoTransicion = false;

                        panelEditor.Invalidate();
                        return;
                    }
                }

                return;
            }

            // -----------------------------------------
            // SI ACABAMOS DE MOVER UN ESTADO
            // -----------------------------------------

            if (estadoFueMovido)
            {
                estadoFueMovido = false;
                return;
            }

            // -----------------------------------------
            // CLIC SOBRE UN ESTADO EXISTENTE
            // -----------------------------------------

            foreach (Estado estado in automata.Estados)
            {
                if (EstaDentroDelEstado(estado, e.X, e.Y))
                    return;
            }

            // -----------------------------------------
            // CREAR NUEVO ESTADO
            // -----------------------------------------

            string nombreEstado = ObtenerNombreNuevoEstado();

            Estado nuevoEstado = new Estado(
                nombreEstado,
                e.X,
                e.Y
            );

            automata.AgregarEstado(nuevoEstado);

            panelEditor.Invalidate();
        }

        private void panelEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            estadoFueMovido = false;

            foreach (Estado estado in automata.Estados)
            {
                if (EstaDentroDelEstado(estado, e.X, e.Y))
                {
                    estadoSeleccionado = estado;
                    moviendoEstado = true;
                    break;
                }
            }
        }

        private bool EstaDentroDelEstado(Estado estado, int x, int y)
        {
            int distanciaX = x - estado.X;
            int distanciaY = y - estado.Y;

            double distancia = Math.Sqrt(
                distanciaX * distanciaX +
                distanciaY * distanciaY
            );

            return distancia <= RADIO_ESTADO;
        }

        private string ObtenerNombreNuevoEstado()
        {
            int numero = 0;

            while (true)
            {
                string nombre = "q" + numero;

                bool existe = false;

                foreach (Estado estado in automata.Estados)
                {
                    if (estado.Nombre == nombre)
                    {
                        existe = true;
                        break;
                    }
                }

                if (!existe)
                    return nombre;

                numero++;
            }
        }


        private void panelEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!moviendoEstado || estadoSeleccionado == null)
                return;

            estadoSeleccionado.X = e.X;
            estadoSeleccionado.Y = e.Y;

            estadoFueMovido = true;

            panelEditor.Invalidate();
        }

        private void panelEditor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            moviendoEstado = false;
            estadoSeleccionado = null;
        }

        private void btnEstadoInicial_Click(object sender, EventArgs e)
        {
            modoEstadoInicial = true;
        }

        private void btnEstadoFinal_Click(object sender, EventArgs e)
        {
            modoEstadoFinal = true;
        }

        private void btnTransicion_Click(object sender, EventArgs e)
        {
            modoTransicion = true;
            estadoOrigenTransicion = null;
        }
    }
}
