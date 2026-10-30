using SimuladorAutomatas.Formularios;
using SimuladorAutomatas.Logica;
using SimuladorAutomatas.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private bool modoMover;

        //Transiciones
        private bool modoTransicion;
        private Estado estadoOrigenTransicion;

        public Form1()
        {
            InitializeComponent();
            automata = new AutomataEditor();
            AplicarEstilo();
        }

        private void AplicarEstilo()
        {
            EstiloInterfaz.AplicarEstiloFormulario(
                this
            );

            EstiloInterfaz.AplicarEstiloPanel(
                panelEditor
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnEstadoInicial
            );

            EstiloInterfaz.AplicarEstiloBoton(
    btnEstablecerAlfabeto
);


            EstiloInterfaz.AplicarEstiloBoton(
    btnGuardar
);
            EstiloInterfaz.AplicarEstiloBoton(
    btnNuevo
);
            EstiloInterfaz.AplicarEstiloBoton(
                btnAbrir
            );

            EstiloInterfaz.AplicarEstiloBoton(
    btnMover
);
            EstiloInterfaz.AplicarEstiloBoton(
    btnExpresionesRegulares
);

            EstiloInterfaz.AplicarEstiloBoton(
    btnAnalizarLogs
);
            EstiloInterfaz.AplicarEstiloBoton(
                btnEstadoFinal
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnTransicion
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnValidarAFD
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnValidarAFN
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnSimular
            );

            EstiloInterfaz.AplicarEstiloBoton(
                button2
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnConvertirAFD
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnMinimizarAFD
            );

            EstiloInterfaz.AplicarEstiloLabel(
                label1
            );

            EstiloInterfaz.AplicarEstiloTextBox(
                txtCadena
            );

            EstiloInterfaz.AplicarEstiloLabel(
                lblResultado
            );
        }

        public Form1(AutomataEditor automataConvertido)
        {
            InitializeComponent();

            automata = automataConvertido;
        }

        private List<string> ObtenerSimbolosInvalidos(string cadena)
        {
            List<string> simbolosInvalidos = new List<string>();

            foreach (char caracter in cadena)
            {
                string simbolo = caracter.ToString();

                if (!automata.Alfabeto.Contains(simbolo) &&
                    !simbolosInvalidos.Contains(simbolo))
                {
                    simbolosInvalidos.Add(simbolo);
                }
            }

            return simbolosInvalidos;
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

            // Dibujar estado
            using (Pen lapiz =
                new Pen(EstiloInterfaz.CianActivo, 2))
            {
                using (Brush pincel =
                    new SolidBrush(EstiloInterfaz.ColorEstado))
                {
                    g.FillEllipse(
                        pincel,
                        circulo
                    );

                    g.DrawEllipse(
                        lapiz,
                        circulo
                    );
                }

                // Si es estado final
                if (estado.EsFinal)
                {
                    int margen = 5;

                    Rectangle segundoCirculo =
                        new Rectangle(
                            x + margen,
                            y + margen,
                            diametro - margen * 2,
                            diametro - margen * 2
                        );

                    g.DrawEllipse(
                        lapiz,
                        segundoCirculo
                    );
                }
            }

            // Nombre del estado
            using (Brush pincel =
                new SolidBrush(
                    EstiloInterfaz.TextoPrincipal))
            {
                StringFormat formato =
                    new StringFormat();

                formato.Alignment =
                    StringAlignment.Center;

                formato.LineAlignment =
                    StringAlignment.Center;

                g.DrawString(
                    estado.Nombre,
                    this.Font,
                    pincel,
                    circulo,
                    formato
                );
            }

            // Flecha del estado inicial
            if (estado.EsInicial)
            {
                using (Pen lapizFlecha =
                    new Pen(
                        EstiloInterfaz.ColorLinea,
                        2))
                {
                    lapizFlecha.CustomEndCap =
                        new System.Drawing.Drawing2D.AdjustableArrowCap(
                            5,
                            5
                        );

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

        private void DibujarTransicion(
    Graphics g,
    Transicion transicion)
        {
            Estado origen = transicion.Origen;
            Estado destino = transicion.Destino;

            // Si es un bucle
            if (origen == destino)
            {
                DibujarBucle(
                    g,
                    transicion
                );

                return;
            }

            // Obtener todas las transiciones del mismo sentido
            List<Transicion> mismasTransiciones =
                new List<Transicion>();

            foreach (Transicion t
                in automata.Transiciones)
            {
                if (t.Origen == origen &&
                    t.Destino == destino)
                {
                    mismasTransiciones.Add(t);
                }
            }

            // Solo dibujar una vez
            if (transicion != mismasTransiciones[0])
                return;

            // Obtener símbolos
            List<string> simbolos =
                new List<string>();

            foreach (Transicion t
                in mismasTransiciones)
            {
                if (!simbolos.Contains(t.Simbolo))
                {
                    simbolos.Add(t.Simbolo);
                }
            }

            string textoSimbolos =
                string.Join(",", simbolos);

            // Buscar transición en sentido contrario
            bool existeSentidoContrario = false;

            foreach (Transicion t
                in automata.Transiciones)
            {
                if (t.Origen == destino &&
                    t.Destino == origen)
                {
                    existeSentidoContrario = true;
                    break;
                }
            }

            float dx =
                destino.X - origen.X;

            float dy =
                destino.Y - origen.Y;

            double distancia =
                Math.Sqrt(
                    dx * dx +
                    dy * dy
                );

            // Si la línea tiene longitud cero
            if (distancia == 0)
                return;

            float nx =
                dx / (float)distancia;

            float ny =
                dy / (float)distancia;

            float px = -ny;
            float py = nx;

            float desplazamiento = 0;

            // Si existen transiciones en sentido contrario,
            // usar una curva para separarlas
            if (existeSentidoContrario)
            {
                desplazamiento = 35;
            }

            float inicioX =
                origen.X +
                nx * RADIO_ESTADO;

            float inicioY =
                origen.Y +
                ny * RADIO_ESTADO;

            float finalX =
                destino.X -
                nx * RADIO_ESTADO;

            float finalY =
                destino.Y -
                ny * RADIO_ESTADO;

            float medioX =
                (inicioX + finalX) / 2;

            float medioY =
                (inicioY + finalY) / 2;

            float controlX =
                medioX +
                px * desplazamiento;

            float controlY =
                medioY +
                py * desplazamiento;

            using (Pen lapiz =
                new Pen(EstiloInterfaz.ColorLinea,2))
            {
                lapiz.CustomEndCap =
                    new System.Drawing.Drawing2D.AdjustableArrowCap(
                        5,
                        5
                    );

                if (!existeSentidoContrario)
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

                        g.DrawPath(
                            lapiz,
                            ruta
                        );
                    }
                }
            }

            float textoX;
            float textoY;

            if (!existeSentidoContrario)
            {
                textoX =
                    (inicioX + finalX) / 2;

                textoY =
                    (inicioY + finalY) / 2 - 15;
            }
            else
            {
                textoX = controlX;
                textoY = controlY;
            }

            using (Brush pincel =
                new SolidBrush(EstiloInterfaz.ColorSimbolo))
            {
                StringFormat formato =
                    new StringFormat();

                formato.Alignment =
                    StringAlignment.Center;

                formato.LineAlignment =
                    StringAlignment.Center;

                RectangleF areaTexto =
                    new RectangleF(
                        textoX - 30,
                        textoY - 15,
                        60,
                        30
                    );

                g.DrawString(
                    textoSimbolos,
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

            int ancho = 35;
            int alto = 35;

            float x = estado.X - ancho / 2;
            float y = estado.Y - RADIO_ESTADO - alto + 5;

            RectangleF rectanguloBucle = new RectangleF(
                x,
                y,
                ancho,
                alto
            );

            using (Pen lapiz = new Pen(EstiloInterfaz.ColorLinea,2))
            {
                lapiz.CustomEndCap =
                    new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

                g.DrawArc(
                    lapiz,
                    rectanguloBucle,
                    200,
                    290
                );
            }

            // Obtener todos los símbolos del mismo bucle
            List<Transicion> bucles =
                new List<Transicion>();

            foreach (Transicion t in automata.Transiciones)
            {
                if (t.Origen == estado &&
                    t.Destino == estado)
                {
                    bucles.Add(t);
                }
            }

            // Solo dibujar una vez
            if (transicion != bucles[0])
                return;

            List<string> simbolos =
                new List<string>();

            foreach (Transicion t in bucles)
            {
                if (!simbolos.Contains(t.Simbolo))
                {
                    simbolos.Add(t.Simbolo);
                }
            }

            string textoSimbolos =
                string.Join(",", simbolos);

            float textoX = estado.X;
            float textoY = y - 8;

            using (Brush pincel =
                new SolidBrush(EstiloInterfaz.ColorSimbolo))
            {
                StringFormat formato =
                    new StringFormat();

                formato.Alignment =
                    StringAlignment.Center;

                formato.LineAlignment =
                    StringAlignment.Center;

                RectangleF areaTexto =
                    new RectangleF(
                        textoX - 30,
                        textoY - 15,
                        60,
                        30
                    );

                g.DrawString(
                    textoSimbolos,
                    this.Font,
                    pincel,
                    areaTexto,
                    formato
                );
            }
        }

        private void panelEditor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = automata.Transiciones.Count - 1; i >= 0; i--)
            {
                Transicion transicion = automata.Transiciones[i];

                if (EstaSobreTransicion(transicion, e.X, e.Y))
                {
                    string nuevoSimbolo =
                        Microsoft.VisualBasic.Interaction.InputBox(
                            "Ingrese el nuevo símbolo:",
                            "Editar transición",
                            transicion.Simbolo
                        );

                    if (string.IsNullOrWhiteSpace(nuevoSimbolo))
                        return;

                    nuevoSimbolo = nuevoSimbolo.Trim();

                    if (nuevoSimbolo != "ε" &&
                        !automata.Alfabeto.Contains(nuevoSimbolo))
                    {
                        MessageBox.Show(
                            "El símbolo \"" + nuevoSimbolo +
                            "\" no pertenece al alfabeto.\n\n" +
                            "Alfabeto actual: " +
                            string.Join(", ", automata.Alfabeto),
                            "Símbolo inválido",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        return;
                    }

                    string simboloAnterior = transicion.Simbolo;

                    transicion.Simbolo = nuevoSimbolo;

                    LogService.Info(
                        "TRANSITION",
                        "Transicion " +
                        transicion.Origen.Nombre +
                        "->" +
                        transicion.Destino.Nombre +
                        " modificada de " +
                        simboloAnterior +
                        " a " +
                        nuevoSimbolo
                    );

                    panelEditor.Invalidate();

                    return;
                }
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
                        LogService.Info("STATE", "Estado " + estado.Nombre + " eliminado");

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
                        LogService.Info("TRANSITION", "Transicion " + transicion.Origen.Nombre + "->" + transicion.Destino.Nombre + " eliminada");

                        panelEditor.Invalidate();
                        return;
                    }
                }

                return;
            }

            // -----------------------------------------
            // EVITAR CREAR ESTADOS CON DOBLE CLIC
            // -----------------------------------------

            if (e.Button == MouseButtons.Left && e.Clicks > 1)
                return;


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
                        LogService.Info("STATE", "Estado " + estado.Nombre + " establecido como inicial");

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

                        LogService.Info("STATE", "Estado " + estado.Nombre + (estado.EsFinal ? " marcado como final" : " desmarcado como final"));
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
                        string simbolos = Microsoft.VisualBasic.Interaction.InputBox(
                            "Ingrese el símbolo o varios separados por coma:",
                            "Nueva transición",
                            ""
                        );

                        // Si el usuario cancela o deja vacío
                        if (string.IsNullOrWhiteSpace(simbolos))
                        {
                            estadoOrigenTransicion = null;
                            return;
                        }

                        // Separar símbolos
                        string[] listaSimbolos = simbolos.Split(',');
                        foreach (string simboloTexto in listaSimbolos)
                        {
                            string simbolo = simboloTexto.Trim();

                            if (string.IsNullOrWhiteSpace(simbolo))
                                continue;

                            // ε se permite como transición epsilon
                            if (simbolo == "ε")
                            {
                                Transicion nuevaTransicion = new Transicion(
                                    estadoOrigenTransicion,
                                    simbolo,
                                    destino
                                );

                                automata.AgregarTransicion(nuevaTransicion);
                                LogService.Info("TRANSITION", "Transicion " + estadoOrigenTransicion.Nombre + "->" + destino.Nombre + " creada con simbolo epsilon");

                                continue;
                            }

                            // Verificar que el símbolo pertenezca al alfabeto
                            if (!automata.Alfabeto.Contains(simbolo))
                            {
                                MessageBox.Show(
                                    "El símbolo \"" + simbolo +
                                    "\" no pertenece al alfabeto.\n\n" +
                                    "Alfabeto actual: " +
                                    string.Join(", ", automata.Alfabeto),
                                    "Símbolo inválido",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );

                                LogService.Warning("TRANSITION", "Intento de crear transicion con simbolo fuera del alfabeto: " + simbolo);
                                estadoOrigenTransicion = null;
                                return;
                            }

                            Transicion nuevaTransicionValida = new Transicion(
                                estadoOrigenTransicion,
                                simbolo,
                                destino
                            );

                            automata.AgregarTransicion(
                                nuevaTransicionValida
                            );
                            LogService.Info("TRANSITION", "Transicion " + estadoOrigenTransicion.Nombre + "->" + destino.Nombre + " creada con simbolo " + simbolo);
                        }

                        // Reiniciar origen para otra transición
                        estadoOrigenTransicion = null;

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

                return;
            }

            // -----------------------------------------
            // CLIC SOBRE UNA TRANSICIÓN
            // -----------------------------------------

            for (int i = automata.Transiciones.Count - 1; i >= 0; i--)
            {
                Transicion transicion = automata.Transiciones[i];

                if (EstaSobreTransicion(transicion, e.X, e.Y))
                {
                    return;
                }
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
            LogService.Info("STATE", "Estado " + nuevoEstado.Nombre + " creado");

            panelEditor.Invalidate();
        }

        private void panelEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            estadoFueMovido = false;

            if (!modoMover)
                return;

            foreach (Estado estado in automata.Estados)
            {
                if (EstaDentroDelEstado(estado, e.X, e.Y))
                {
                    estadoSeleccionado = estado;
                    moviendoEstado = true;

                    LogService.Info(
                        "STATE",
                        "Estado seleccionado para mover: " + estado.Nombre);

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
            // Posición del símbolo de la transición
            float textoX;
            float textoY;

            if (!existeSentidoContrario)
            {
                textoX = (inicioX + finalX) / 2;
                textoY = (inicioY + finalY) / 2 - 15;
            }
            else
            {
                textoX = controlX;
                textoY = controlY;
            }

            // Área donde se muestra el símbolo
            RectangleF areaTexto = new RectangleF(
                textoX - 30,
                textoY - 15,
                60,
                30
            );

            // Si se hizo clic sobre el símbolo
            if (areaTexto.Contains(x, y))
                return true;

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

                return distanciaLinea <= 25;
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

            estadoFueMovido = true;

            estadoSeleccionado.X = e.X;
            estadoSeleccionado.Y = e.Y;

            panelEditor.Invalidate();
        }

        private void panelEditor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (moviendoEstado && estadoSeleccionado != null && estadoFueMovido)
                LogService.Info("STATE", "Estado " + estadoSeleccionado.Nombre + " movido a (" + estadoSeleccionado.X + "," + estadoSeleccionado.Y + ")");

            moviendoEstado = false;
            estadoSeleccionado = null;
            estadoFueMovido = false;
        }

        private void btnEstadoInicial_Click(object sender, EventArgs e)
        {
            modoEstadoInicial = true;
            modoEstadoFinal = false;
            modoTransicion = false;
            modoMover = false;
            estadoOrigenTransicion = null;
        }

        private void btnEstadoFinal_Click(object sender, EventArgs e)
        {
            modoEstadoFinal = true;
            modoEstadoInicial = false;
            modoTransicion = false;
            modoMover = false;
            estadoOrigenTransicion = null;
        }

        private void btnTransicion_Click(object sender, EventArgs e)
        {
            modoTransicion = !modoTransicion;

            modoEstadoInicial = false;
            modoEstadoFinal = false;
            modoMover = false;
            estadoOrigenTransicion = null;
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            LogService.Info("SIMULATION", "Inicio de simulacion AFD con cadena: " + txtCadena.Text.Trim());
            SimuladorAFD simulador = new SimuladorAFD(automata);

            // Verificar que exista un alfabeto
            if (automata.Alfabeto.Count == 0)
            {
                lblResultado.Text =
                    "Resultado: Debe establecer el alfabeto antes de simular";
                LogService.Warning("SIMULATION", "Simulacion AFD cancelada: no hay alfabeto");

                return;
            }

            // Validar AFD
            if (!simulador.Validar())
            {
                lblResultado.Text =
                    "Resultado: El autómata no es un AFD válido";
                LogService.Warning("VALIDATION", "El automata no es un AFD valido");

                return;
            }

            string cadena = txtCadena.Text.Trim();

            // Validar símbolos de la cadena
            List<string> simbolosInvalidos =
                ObtenerSimbolosInvalidos(cadena);

            if (simbolosInvalidos.Count > 0)
            {
                lblResultado.Text =
                    "Resultado: Símbolos inválidos: " +
                    string.Join(", ", simbolosInvalidos);
                LogService.Warning("SIMULATION", "Cadena con simbolos invalidos: " + string.Join(", ", simbolosInvalidos));

                return;
            }

            // Simular cadena
            bool aceptada = simulador.Simular(cadena);

            if (aceptada)
            {
                lblResultado.Text =
                    "Resultado: Cadena aceptada";
                LogService.Info("SIMULATION", "Cadena aceptada por AFD");
            }
            else
            {
                lblResultado.Text =
                    "Resultado: Cadena rechazada";
                LogService.Info("SIMULATION", "Cadena rechazada por AFD");
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
                LogService.Info("VALIDATION", "AFD valido");
                MessageBox.Show(
                    "El autómata es un AFD válido.",
                    "Validación"
                );
            }
            else
            {
                LogService.Warning("VALIDATION", "AFD invalido");
                MessageBox.Show(
                    "El autómata no es un AFD válido.",
                    "Validación"
                );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Verificar que exista un alfabeto
            if (automata.Alfabeto.Count == 0)
            {
                lblResultado.Text =
                    "Resultado: Debe establecer el alfabeto antes de simular";

                return;
            }

            SimuladorAFN simulador = new SimuladorAFN(automata);

            // Validar AFN
            if (!simulador.Validar())
            {
                lblResultado.Text =
                    "Resultado: El autómata no es un AFN válido";

                return;
            }

            string cadena = txtCadena.Text.Trim();

            // Validar símbolos de la cadena
            List<string> simbolosInvalidos =
                ObtenerSimbolosInvalidos(cadena);

            if (simbolosInvalidos.Count > 0)
            {
                lblResultado.Text =
                    "Resultado: Símbolos inválidos: " +
                    string.Join(", ", simbolosInvalidos);

                return;
            }

            // Simular cadena
            bool aceptada = simulador.Simular(cadena);

            if (aceptada)
            {
                lblResultado.Text =
                    "Resultado: Cadena aceptada";
            }
            else
            {
                lblResultado.Text =
                    "Resultado: Cadena rechazada";
            }

        }

        private void btnValidarAFN_Click(object sender, EventArgs e)
        {
            SimuladorAFN simulador = new SimuladorAFN(automata);

            if (simulador.Validar())
            {
                LogService.Info("VALIDATION", "AFN valido");
                MessageBox.Show(
                    "El autómata es un AFN válido.",
                    "Validación"
                );
            }
            else
            {
                LogService.Warning("VALIDATION", "AFN invalido");
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
            LogService.Info("CONVERSION", "Inicio de conversion AFN a AFD");
            SimuladorAFN simulador = new SimuladorAFN(automata);

            if (!simulador.Validar())
            {
                LogService.Warning(
                    "CONVERSION",
                    "Conversion AFN a AFD cancelada: el automata no es un AFN valido");

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
            LogService.Info("CONVERSION", "Conversion AFN a AFD completada");

            FrmConversionAFD ventana =
                new FrmConversionAFD(
                    resultado.Procedimiento,
                    resultado.AFD
                );

            ventana.ShowDialog();
        }

        private void btnMinimizarAFD_Click(object sender, EventArgs e)
        {
            LogService.Info("MINIMIZATION", "Inicio de minimizacion de AFD");
            // Verificar que exista un alfabeto
            if (automata.Alfabeto.Count == 0)
            {
                LogService.Warning(
    "MINIMIZATION",
    "Minimizacion cancelada: no hay alfabeto establecido");

                MessageBox.Show(
                    "Debe establecer el alfabeto antes de minimizar el AFD.",
                    "Minimización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            SimuladorAFD simulador =
                new SimuladorAFD(automata);

            // Validar AFD
            if (!simulador.Validar())
            {
                LogService.Warning(
    "MINIMIZATION",
    "Minimizacion cancelada: el automata no es un AFD valido");

                MessageBox.Show(
                    "El autómata no es un AFD válido.",
                    "Minimización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            FrmMinimizacionAFD ventana =
                new FrmMinimizacionAFD(automata);
            LogService.Info("MINIMIZATION", "Minimizacion de AFD abierta");

            ventana.ShowDialog();
        }

        private void btnMover_Click(object sender, EventArgs e)
        {
            modoMover = !modoMover;

            modoEstadoInicial = false;
            modoEstadoFinal = false;
            modoTransicion = false;

            estadoOrigenTransicion = null;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar =
    new SaveFileDialog();

            guardar.Title =
                "Guardar proyecto";

            guardar.Filter =
                "Proyecto de autómata (*.aut)|*.aut";

            guardar.DefaultExt = "aut";

            guardar.AddExtension = true;

            if (guardar.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                GestorProyectos.Guardar(
                    automata,
                    guardar.FileName
                );

                LogService.Info("PROJECT", "Automata guardado en " + guardar.FileName);
                MessageBox.Show(
                    "Proyecto guardado correctamente.",
                    "Guardar proyecto"
                );
            }
            catch (Exception ex)
            {
                LogService.Error("PROJECT", "No se pudo guardar el automata", ex);
                MessageBox.Show(
                    "No se pudo guardar el proyecto.\n\n" +
                    ex.Message,
                    "Error"
                );
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir =
    new OpenFileDialog();

            abrir.Title =
                "Abrir proyecto";

            abrir.Filter =
                "Proyecto de autómata (*.aut)|*.aut";

            if (abrir.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                AutomataEditor proyecto =
                    GestorProyectos.Cargar(
                        abrir.FileName
                    );

                automata = proyecto;

                panelEditor.Invalidate();

                LogService.Info("PROJECT", "Automata abierto desde " + abrir.FileName);
                MessageBox.Show(
                    "Proyecto abierto correctamente.",
                    "Abrir proyecto"
                );
            }
            catch (Exception ex)
            {
                LogService.Error("PROJECT", "No se pudo abrir el automata", ex);
                MessageBox.Show(
                    "No se pudo abrir el proyecto.\n\n" +
                    ex.Message,
                    "Error"
                );
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Deseas crear un proyecto nuevo?\n\nSe perderá el autómata actual si no lo has guardado.",
                "Nuevo proyecto",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
                return;

            automata = new AutomataEditor();
            LogService.Info("PROJECT", "Nuevo automata creado");

            panelEditor.Invalidate();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnEstablecerAlfabeto_Click(object sender, EventArgs e)
        {
            string entrada = txtAlfabeto.Text.Trim();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                MessageBox.Show(
                    "Debe ingresar al menos un símbolo.",
                    "Alfabeto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                LogService.Warning("ALPHABET", "Intento de establecer alfabeto vacio");
                return;
            }

            string[] simbolos = entrada.Split(',');

            List<string> nuevoAlfabeto = new List<string>();

            foreach (string simboloTexto in simbolos)
            {
                string simbolo = simboloTexto.Trim();

                if (string.IsNullOrWhiteSpace(simbolo))
                    continue;

                if (simbolo == "ε")
                {
                    MessageBox.Show(
                        "El símbolo ε no pertenece al alfabeto.",
                        "Alfabeto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (simbolo.Length > 1)
                {
                    MessageBox.Show(
                        "Cada símbolo debe tener un solo carácter.\n\n" +
                        "Ejemplos válidos: 0,1 o a,b",
                        "Alfabeto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (!nuevoAlfabeto.Contains(simbolo))
                {
                    nuevoAlfabeto.Add(simbolo);
                }
            }

            if (nuevoAlfabeto.Count == 0)
            {
                MessageBox.Show(
                    "Debe ingresar símbolos válidos.",
                    "Alfabeto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            automata.Alfabeto = nuevoAlfabeto;
            LogService.Info("ALPHABET", "Alfabeto establecido: " + string.Join(", ", automata.Alfabeto));

            MessageBox.Show(
                "Alfabeto establecido correctamente:\n\n" +
                string.Join(", ", automata.Alfabeto),
                "Alfabeto",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnAnalizarLogs_Click(object sender, EventArgs e)
        {
            LogService.Info("LOG", "Analizador de logs abierto");
            string rutaLog = LogService.GetLogFilePath();
            FrmAnalizadorLogs formulario =
                new FrmAnalizadorLogs(rutaLog);

            formulario.ShowDialog(this);
        }

        private void btnExpresionesRegulares_Click(object sender, EventArgs e)
        {
            LogService.Info("REGEX", "Modulo de expresiones regulares abierto");
            FrmExpresionesRegulares formulario =
    new FrmExpresionesRegulares();

            formulario.ShowDialog();
        }
    }
}
