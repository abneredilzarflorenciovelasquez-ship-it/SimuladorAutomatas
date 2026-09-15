using SimuladorAutomatas.Formularios;
using SimuladorAutomatas.Logica;
using SimuladorAutomatas.Logica;
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

        public Form1(AutomataEditor automataConvertido)
        {
            InitializeComponent();

            automata = automataConvertido;
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

            // Si es un bucle
            if (origen == destino)
            {
                DibujarBucle(g, transicion);
                return;
            }

            // Obtener transiciones del mismo sentido
            List<Transicion> mismasTransiciones = new List<Transicion>();

            foreach (Transicion t in automata.Transiciones)
            {
                if (t.Origen == origen && t.Destino == destino)
                {
                    mismasTransiciones.Add(t);
                }
            }

            int indice = mismasTransiciones.IndexOf(transicion);
            int cantidad = mismasTransiciones.Count;

            // Buscar transición en sentido contrario
            bool existeSentidoContrario = false;

            foreach (Transicion t in automata.Transiciones)
            {
                if (t.Origen == destino && t.Destino == origen)
                {
                    existeSentidoContrario = true;
                    break;
                }
            }

            // Diferencia entre las posiciones
            float dx = destino.X - origen.X;
            float dy = destino.Y - origen.Y;

            double distancia = Math.Sqrt(dx * dx + dy * dy);

            // Si la línea tiene longitud cero
            if (distancia == 0)
                return;

            // Vector normalizado
            float nx = dx / (float)distancia;
            float ny = dy / (float)distancia;

            // Vector perpendicular
            float px = -ny;
            float py = nx;

            // Desplazamiento de la curva
            float desplazamiento = 0;

            if (cantidad > 1)
            {
                float separacion = 35;

                desplazamiento =
                    (indice - (cantidad - 1) / 2.0f) * separacion;
            }
            else if (existeSentidoContrario)
            {
                desplazamiento = 35;
            }

            // Puntos de inicio y final
            float inicioX = origen.X + nx * RADIO_ESTADO;
            float inicioY = origen.Y + ny * RADIO_ESTADO;

            float finalX = destino.X - nx * RADIO_ESTADO;
            float finalY = destino.Y - ny * RADIO_ESTADO;

            // Punto de control
            float medioX = (inicioX + finalX) / 2;
            float medioY = (inicioY + finalY) / 2;

            float controlX = medioX + px * desplazamiento;
            float controlY = medioY + py * desplazamiento;

            using (Pen lapiz = new Pen(Color.Black, 2))
            {
                lapiz.CustomEndCap =
                    new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

                if (cantidad == 1 && !existeSentidoContrario)
                {
                    g.DrawLine(
                        lapiz,
                        inicioX,
                        inicioY,
                        finalX,
                        finalY
                    );
                }
                else
                {
                    using (System.Drawing.Drawing2D.GraphicsPath ruta =
                           new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        ruta.AddBezier(
                            inicioX,
                            inicioY,
                            controlX,
                            controlY,
                            controlX,
                            controlY,
                            finalX,
                            finalY
                        );

                        g.DrawPath(lapiz, ruta);
                    }
                }
            }

            // Posición del símbolo
            float textoX;
            float textoY;

            if (cantidad == 1 && !existeSentidoContrario)
            {
                textoX = (inicioX + finalX) / 2;
                textoY = (inicioY + finalY) / 2;
            }
            else
            {
                textoX = controlX;
                textoY = controlY;
            }

            using (Brush pincel = new SolidBrush(Color.Black))
            {
                StringFormat formato = new StringFormat();

                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;

                RectangleF areaTexto = new RectangleF(
                    textoX - 20,
                    textoY - 15,
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

        private void DibujarBucle(Graphics g, Transicion transicion)
        {
            Estado estado = transicion.Origen;

            // Tamaño del bucle
            int ancho = 35;
            int alto = 35;

            // Posición del bucle
            // Lo colocamos arriba del estado
            float x = estado.X - ancho / 2;
            float y = estado.Y - RADIO_ESTADO - alto + 5;

            RectangleF rectanguloBucle = new RectangleF(
                x,
                y,
                ancho,
                alto
            );

            // -----------------------------------------
            // DIBUJAR BUCLE
            // -----------------------------------------

            using (Pen lapiz = new Pen(Color.Black, 2))
            {
                lapiz.CustomEndCap =
                    new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

                // Dibujar arco
                g.DrawArc(
                    lapiz,
                    rectanguloBucle,
                    200,
                    290
                );
            }

            // -----------------------------------------
            // DIBUJAR SÍMBOLO
            // -----------------------------------------

            float textoX = estado.X;
            float textoY = y - 8;

            using (Brush pincel = new SolidBrush(Color.Black))
            {
                StringFormat formato = new StringFormat();

                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;

                RectangleF areaTexto = new RectangleF(
                    textoX - 20,
                    textoY - 15,
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
            // -----------------------------------------
            // CLIC DERECHO
            // -----------------------------------------

            if (e.Button == MouseButtons.Right)
            {
                // Primero revisar si se hizo clic sobre un estado
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

                        // Eliminar las transiciones relacionadas con el estado
                        automata.Transiciones.RemoveAll(
                            t => t.Origen == estado || t.Destino == estado
                        );

                        // Eliminar el estado
                        automata.Estados.Remove(estado);

                        panelEditor.Invalidate();
                        return;
                    }
                }

                // Si no se hizo clic sobre un estado,
                // revisar si se hizo clic sobre una transición
                for (int i = automata.Transiciones.Count - 1; i >= 0; i--)
                {
                    Transicion transicion = automata.Transiciones[i];

                    if (EstaSobreTransicion(transicion, e.X, e.Y))
                    {
                        automata.Transiciones.RemoveAt(i);

                        panelEditor.Invalidate();
                        return;
                    }
                }

                return;
            }

            // -----------------------------------------
            // SI NO ES CLIC IZQUIERDO, NO HACEMOS NADA
            // -----------------------------------------

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

        private bool EstaSobreTransicion(
    Transicion transicion,
    int x,
    int y)
        {
            Estado origen = transicion.Origen;
            Estado destino = transicion.Destino;

            // Si es un bucle
            if (origen == destino)
            {
                int ancho = 35;
                int alto = 35;

                float bucleX = origen.X - ancho / 2;
                float bucleY = origen.Y - RADIO_ESTADO - alto + 5;

                RectangleF areaClic = new RectangleF(
                    bucleX - 8,
                    bucleY - 8,
                    ancho + 16,
                    alto + 16
                );

                return areaClic.Contains(x, y);
            }

            // Obtener transiciones del mismo sentido
            List<Transicion> mismasTransiciones = new List<Transicion>();

            foreach (Transicion t in automata.Transiciones)
            {
                if (t.Origen == origen && t.Destino == destino)
                {
                    mismasTransiciones.Add(t);
                }
            }

            int indice = mismasTransiciones.IndexOf(transicion);
            int cantidad = mismasTransiciones.Count;

            // Buscar transición en sentido contrario
            bool existeSentidoContrario = false;

            foreach (Transicion t in automata.Transiciones)
            {
                if (t.Origen == destino && t.Destino == origen)
                {
                    existeSentidoContrario = true;
                    break;
                }
            }

            // Diferencia entre las posiciones
            float dx = destino.X - origen.X;
            float dy = destino.Y - origen.Y;

            double distancia = Math.Sqrt(dx * dx + dy * dy);

            // Si la línea tiene longitud cero
            if (distancia == 0)
                return false;

            // Vector normalizado
            float nx = dx / (float)distancia;
            float ny = dy / (float)distancia;

            // Vector perpendicular
            float px = -ny;
            float py = nx;

            // Desplazamiento de la curva
            float desplazamiento = 0;

            if (cantidad > 1)
            {
                float separacion = 35;

                desplazamiento =
                    (indice - (cantidad - 1) / 2.0f) * separacion;
            }
            else if (existeSentidoContrario)
            {
                desplazamiento = 35;
            }

            // Puntos de inicio y final
            float inicioX = origen.X + nx * RADIO_ESTADO;
            float inicioY = origen.Y + ny * RADIO_ESTADO;

            float finalX = destino.X - nx * RADIO_ESTADO;
            float finalY = destino.Y - ny * RADIO_ESTADO;

            // Punto de control
            float medioX = (inicioX + finalX) / 2;
            float medioY = (inicioY + finalY) / 2;

            float controlX = medioX + px * desplazamiento;
            float controlY = medioY + py * desplazamiento;

            // Si es una línea recta
            if (cantidad == 1 && !existeSentidoContrario)
            {
                double distanciaLinea = DistanciaPuntoLinea(
                    x,
                    y,
                    inicioX,
                    inicioY,
                    finalX,
                    finalY
                );

                return distanciaLinea <= 8;
            }

            // Si es una curva
            for (int i = 0; i <= 100; i++)
            {
                float t = i / 100.0f;

                float puntoX =
                    (1 - t) * (1 - t) * (1 - t) * inicioX +
                    3 * (1 - t) * (1 - t) * t * controlX +
                    3 * (1 - t) * t * t * controlX +
                    t * t * t * finalX;

                float puntoY =
                    (1 - t) * (1 - t) * (1 - t) * inicioY +
                    3 * (1 - t) * (1 - t) * t * controlY +
                    3 * (1 - t) * t * t * controlY +
                    t * t * t * finalY;

                double diferenciaX = x - puntoX;
                double diferenciaY = y - puntoY;

                double distanciaPunto = Math.Sqrt(
                    diferenciaX * diferenciaX +
                    diferenciaY * diferenciaY
                );

                if (distanciaPunto <= 10)
                    return true;
            }

            return false;
        }

        private double DistanciaPuntoLinea(
    double px,
    double py,
    double x1,
    double y1,
    double x2,
    double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;

            // Si la línea tiene longitud cero
            if (dx == 0 && dy == 0)
            {
                double diferenciaX = px - x1;
                double diferenciaY = py - y1;

                return Math.Sqrt(
                    diferenciaX * diferenciaX +
                    diferenciaY * diferenciaY
                );
            }

            // Posición del punto sobre la línea
            double t =
                ((px - x1) * dx + (py - y1) * dy) /
                (dx * dx + dy * dy);

            // Mantener el punto dentro del segmento
            if (t < 0)
                t = 0;

            if (t > 1)
                t = 1;

            double puntoCercanoX = x1 + t * dx;
            double puntoCercanoY = y1 + t * dy;

            double diferenciaFinalX = px - puntoCercanoX;
            double diferenciaFinalY = py - puntoCercanoY;

            return Math.Sqrt(
                diferenciaFinalX * diferenciaFinalX +
                diferenciaFinalY * diferenciaFinalY
            );
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
            modoEstadoFinal = false;
            modoTransicion = false;
            estadoOrigenTransicion = null;
        }

        private void btnEstadoFinal_Click(object sender, EventArgs e)
        {
            modoEstadoFinal = true;
            modoEstadoInicial = false;
            modoTransicion = false;
            estadoOrigenTransicion = null;
        }

        private void btnTransicion_Click(object sender, EventArgs e)
        {
            modoTransicion = true;
            modoEstadoInicial = false;
            modoEstadoFinal = false;
            estadoOrigenTransicion = null;
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            SimuladorAFD simulador = new SimuladorAFD(automata);

            // Validar AFD
            if (!simulador.Validar())
            {
                lblResultado.Text = "Resultado: El autómata no es un AFD válido";
                return;
            }

            // Simular cadena
            bool aceptada = simulador.Simular(txtCadena.Text);

            if (aceptada)
            {
                lblResultado.Text = "Resultado: Cadena aceptada";
            }
            else
            {
                lblResultado.Text = "Resultado: Cadena rechazada";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnValidarAFD_Click(object sender, EventArgs e)
        {
            SimuladorAFD simulador = new SimuladorAFD(automata);

            if (simulador.Validar())
            {
                MessageBox.Show(
                    "El autómata es un AFD válido.",
                    "Validación"
                );
            }
            else
            {
                MessageBox.Show(
                    "El autómata no es un AFD válido.",
                    "Validación"
                );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SimuladorAFN simulador = new SimuladorAFN(automata);

            bool aceptada = simulador.Simular(txtCadena.Text);

            if (aceptada)
            {
                lblResultado.Text = "Resultado: Cadena aceptada";
            }
            else
            {
                lblResultado.Text = "Resultado: Cadena rechazada";
            }
        }

        private void btnValidarAFN_Click(object sender, EventArgs e)
        {
            SimuladorAFN simulador = new SimuladorAFN(automata);

            if (simulador.Validar())
            {
                MessageBox.Show(
                    "El autómata es un AFN válido.",
                    "Validación"
                );
            }
            else
            {
                MessageBox.Show(
                    "El autómata no es un AFN válido.",
                    "Validación"
                );
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConvertirAFD_Click(object sender, EventArgs e)
        {
            SimuladorAFN simulador = new SimuladorAFN(automata);

            if (!simulador.Validar())
            {
                MessageBox.Show(
                    "El autómata no es un AFN válido.",
                    "Conversión"
                );

                return;
            }

            ConvertidorAFNaAFD convertidor =
                new ConvertidorAFNaAFD(automata);

            ResultadoConversion resultado =
                convertidor.Convertir();

            FrmConversionAFD ventana =
                new FrmConversionAFD(
                    resultado.Procedimiento,
                    resultado.AFD
                );

            ventana.ShowDialog();
        }
    }
}
