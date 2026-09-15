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

namespace SimuladorAutomatas.Formularios
{
    public partial class FrmMinimizacionAFD : Form
    {
        private AutomataEditor afd;
        private AutomataEditor afdMinimizado;
        private Dictionary<string, Label> celdasTabla;
        private StringBuilder procedimiento;
        private DibujadorAutomata dibujador;

        public FrmMinimizacionAFD(AutomataEditor afd)
        {
            InitializeComponent();
            this.afd = afd;

            celdasTabla =
                new Dictionary<string, Label>();

            procedimiento =
                new StringBuilder();

            dibujador =
                new DibujadorAutomata();

            CrearTabla();

            SegundaRonda();

            panelAFDMinimizado.Paint +=
                panelAFDMinimizado_Paint;

            AplicarEstilo();
        }


        private void AplicarEstilo()
        {
            EstiloInterfaz.AplicarEstiloFormulario(
                this
            );

            EstiloInterfaz.AplicarEstiloPanel(
                panelTabla
            );

            EstiloInterfaz.AplicarEstiloPanel(
                panelAFDMinimizado
            );

            EstiloInterfaz.AplicarEstiloRichTextBox(
                txtProcedimientoMin
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnValidarAFDMin
            );

            EstiloInterfaz.AplicarEstiloBoton(
                btnSimularAFDMin
            );

            EstiloInterfaz.AplicarEstiloTextBox(
                txtCadenaAFDMin
            );

            EstiloInterfaz.AplicarEstiloLabel(
                lblResultadoAFDMin
            );
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

            // Crear celdas
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

            etiqueta.Text =
                texto;

            etiqueta.Location =
                new Point(
                    x,
                    y
                );

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

            etiqueta.BackColor =
                EstiloInterfaz.ColorEstado;

            etiqueta.ForeColor =
                EstiloInterfaz.TextoPrincipal;

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

        private void SegundaRonda()
        {
            procedimiento.Clear();

            procedimiento.AppendLine(
                "MINIMIZACIÓN DE AFD"
            );

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "PRIMERA RONDA"
            );

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "Se marcan los pares donde " +
                "un estado es final y el otro no."
            );

            procedimiento.AppendLine();

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

                    if (estado1.EsFinal !=
                        estado2.EsFinal)
                    {
                        MarcarCelda(
                            estado1,
                            estado2,
                            "X"
                        );

                        procedimiento.AppendLine(
                            "(" +
                            estado1.Nombre +
                            "," +
                            estado2.Nombre +
                            ") → X"
                        );
                    }
                    else
                    {
                        procedimiento.AppendLine(
                            "(" +
                            estado1.Nombre +
                            "," +
                            estado2.Nombre +
                            ") → sin marcar"
                        );
                    }
                }
            }

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "SEGUNDA RONDA"
            );

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "Se analizan los pares que no fueron marcados."
            );

            procedimiento.AppendLine();

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

                    string marca =
                        ObtenerMarca(
                            estado1,
                            estado2
                        );

                    if (marca == "X")
                        continue;

                    AnalizarPar(
                        estado1,
                        estado2
                    );
                }
            }

            MarcarEquivalentes();

            List<List<Estado>> grupos =
                CrearGruposEquivalentes();

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "GRUPOS DE ESTADOS EQUIVALENTES"
            );

            procedimiento.AppendLine();

            foreach (List<Estado> grupo in grupos)
            {
                procedimiento.AppendLine(
                    "[" +
                    string.Join(
                        ",",
                        grupo.ConvertAll(
                            e => e.Nombre
                        )
                    ) +
                    "]"
                );
            }

            afdMinimizado =
                CrearAFDMinimizado(grupos);

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "AFD MINIMIZADO"
            );

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "Estados:"
            );

            foreach (Estado estado
                in afdMinimizado.Estados)
            {
                procedimiento.AppendLine(
                    estado.Nombre
                );
            }

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "Estado inicial:"
            );

            if (afdMinimizado.EstadoInicial != null)
            {
                procedimiento.AppendLine(
                    afdMinimizado.EstadoInicial.Nombre
                );
            }

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "Estados finales:"
            );

            foreach (Estado estado
                in afdMinimizado.EstadosFinales)
            {
                procedimiento.AppendLine(
                    estado.Nombre
                );
            }

            procedimiento.AppendLine();

            procedimiento.AppendLine(
                "Transiciones:"
            );

            foreach (Transicion transicion
                in afdMinimizado.Transiciones)
            {
                procedimiento.AppendLine(
                    "δ(" +
                    transicion.Origen.Nombre +
                    "," +
                    transicion.Simbolo +
                    ") = " +
                    transicion.Destino.Nombre
                );
            }

            txtProcedimientoMin.Text =
                procedimiento.ToString();
        }

        private void AnalizarPar(
    Estado estado1,
    Estado estado2)
        {
            procedimiento.AppendLine(
                "Analizamos el par (" +
                estado1.Nombre +
                "," +
                estado2.Nombre +
                ")"
            );

            procedimiento.AppendLine();

            foreach (string simbolo in afd.Alfabeto)
            {
                Estado destino1 =
                    ObtenerDestino(
                        estado1,
                        simbolo
                    );

                Estado destino2 =
                    ObtenerDestino(
                        estado2,
                        simbolo
                    );

                procedimiento.AppendLine(
                    "δ(" +
                    estado1.Nombre +
                    "," +
                    simbolo +
                    ") = " +
                    ObtenerNombre(destino1)
                );

                procedimiento.AppendLine(
                    "δ(" +
                    estado2.Nombre +
                    "," +
                    simbolo +
                    ") = " +
                    ObtenerNombre(destino2)
                );

                if (destino1 != null &&
                    destino2 != null)
                {
                    if (destino1 == destino2)
                    {
                        procedimiento.AppendLine(
                            "Ambos llegan al mismo estado."
                        );
                    }
                    else
                    {
                        procedimiento.AppendLine(
                            "Se analiza el par (" +
                            destino1.Nombre +
                            "," +
                            destino2.Nombre +
                            ")."
                        );

                        if (ObtenerMarca(
                            destino1,
                            destino2) == "X")
                        {
                            procedimiento.AppendLine(
                                "El par está marcado con X."
                            );

                            MarcarCelda(
                                estado1,
                                estado2,
                                "ⓧ"
                            );

                            procedimiento.AppendLine(
                                "Se marca (" +
                                estado1.Nombre +
                                "," +
                                estado2.Nombre +
                                ") con ⓧ."
                            );

                            procedimiento.AppendLine();

                            return;
                        }
                        else
                        {
                            procedimiento.AppendLine(
                                "El par no está marcado."
                            );
                        }
                    }
                }

                procedimiento.AppendLine();
            }

            procedimiento.AppendLine(
                "El par (" +
                estado1.Nombre +
                "," +
                estado2.Nombre +
                ") continúa sin marcar."
            );

            procedimiento.AppendLine(
                "Por lo tanto, los estados siguen siendo equivalentes."
            );

            procedimiento.AppendLine();
        }

        private Estado ObtenerDestino(
    Estado estado,
    string simbolo)
        {
            foreach (Transicion transicion
                in afd.Transiciones)
            {
                if (transicion.Origen == estado &&
                    transicion.Simbolo == simbolo)
                {
                    return transicion.Destino;
                }
            }

            return null;
        }

        private string ObtenerNombre(
    Estado estado)
        {
            if (estado == null)
                return "∅";

            return estado.Nombre;
        }

        private string ObtenerMarca(
    Estado estado1,
    Estado estado2)
        {
            string clave1 =
                ObtenerClave(
                    estado1,
                    estado2
                );

            string clave2 =
                ObtenerClave(
                    estado2,
                    estado1
                );

            if (celdasTabla.ContainsKey(clave1))
            {
                return celdasTabla[clave1].Text;
            }

            if (celdasTabla.ContainsKey(clave2))
            {
                return celdasTabla[clave2].Text;
            }

            return "";
        }

        private void MarcarEquivalentes()
        {
            procedimiento.AppendLine(
                "PARES EQUIVALENTES"
            );

            procedimiento.AppendLine();

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

                    string marca =
                        ObtenerMarca(
                            estado1,
                            estado2
                        );

                    if (string.IsNullOrEmpty(marca))
                    {
                        MarcarCelda(
                            estado1,
                            estado2,
                            "≡"
                        );

                        procedimiento.AppendLine(
                            "(" +
                            estado1.Nombre +
                            "," +
                            estado2.Nombre +
                            ") ≡"
                        );
                    }
                }
            }
        }

        private void MarcarCelda(
            Estado estado1,
            Estado estado2,
            string marca)
        {
            string clave =
                ObtenerClave(
                    estado1,
                    estado2
                );

            if (celdasTabla.ContainsKey(clave))
            {
                celdasTabla[clave].Text =
                    marca;
            }
        }

        private List<List<Estado>> CrearGruposEquivalentes()
        {
            List<List<Estado>> grupos =
                new List<List<Estado>>();

            foreach (Estado estado in afd.Estados)
            {
                bool agregado = false;

                foreach (List<Estado> grupo in grupos)
                {
                    Estado representante =
                        grupo[0];

                    string marca =
                        ObtenerMarca(
                            estado,
                            representante
                        );

                    if (marca == "≡" ||
                        estado == representante)
                    {
                        grupo.Add(estado);
                        agregado = true;
                        break;
                    }
                }

                if (!agregado)
                {
                    grupos.Add(
                        new List<Estado>
                        {
                    estado
                        }
                    );
                }
            }

            return grupos;
        }

        private AutomataEditor CrearAFDMinimizado(
    List<List<Estado>> grupos)
        {
            AutomataEditor resultado =
                new AutomataEditor();

            Dictionary<Estado, Estado> equivalencias =
                new Dictionary<Estado, Estado>();

            int posicion = 0;

            foreach (List<Estado> grupo in grupos)
            {
                string nombre =
                    "[" +
                    string.Join(
                        ",",
                        grupo.ConvertAll(
                            e => e.Nombre
                        )
                    ) +
                    "]";

                Estado nuevo =
                    new Estado(
                        nombre,
                        150 +
                        (posicion % 3) * 180,
                        180 +
                        (posicion / 3) * 150
                    );

                resultado.AgregarEstado(nuevo);

                foreach (Estado estado in grupo)
                {
                    equivalencias[estado] =
                        nuevo;

                    if (estado == afd.EstadoInicial)
                    {
                        nuevo.EsInicial = true;

                        resultado.EstadoInicial =
                            nuevo;
                    }

                    if (estado.EsFinal)
                    {
                        nuevo.EsFinal = true;

                        if (!resultado.EstadosFinales.Contains(
                            nuevo))
                        {
                            resultado.EstadosFinales.Add(
                                nuevo
                            );
                        }
                    }
                }

                posicion++;
            }

            foreach (string simbolo in afd.Alfabeto)
            {
                resultado.AgregarSimbolo(simbolo);
            }

            foreach (List<Estado> grupo in grupos)
            {
                Estado representante =
                    grupo[0];

                foreach (string simbolo in afd.Alfabeto)
                {
                    Estado destino =
                        ObtenerDestino(
                            representante,
                            simbolo
                        );

                    if (destino == null)
                        continue;

                    Estado origenNuevo =
                        equivalencias[representante];

                    Estado destinoNuevo =
                        equivalencias[destino];

                    bool existe = false;

                    foreach (Transicion transicion
                        in resultado.Transiciones)
                    {
                        if (transicion.Origen ==
                            origenNuevo &&
                            transicion.Destino ==
                            destinoNuevo &&
                            transicion.Simbolo ==
                            simbolo)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado.AgregarTransicion(
                            new Transicion(
                                origenNuevo,
                                simbolo,
                                destinoNuevo
                            )
                        );
                    }
                }
            }

            return resultado;
        }

        private void panelAFDMinimizado_Paint(
    object sender,
    PaintEventArgs e)
        {
            if (afdMinimizado == null)
                return;

            dibujador.Dibujar(
                e.Graphics,
                afdMinimizado,
                this.Font
            );
        }

        private void FrmMinimizacionAFD_Load(object sender, EventArgs e)
        {

        }

        private void btnValidarAFDMin_Click(object sender, EventArgs e)
        {
            if (afdMinimizado == null)
            {
                MessageBox.Show(
                    "No existe un AFD minimizado.",
                    "Validación"
                );

                return;
            }

            SimuladorAFD simulador =
                new SimuladorAFD(
                    afdMinimizado
                );

            if (simulador.Validar())
            {
                MessageBox.Show(
                    "El AFD minimizado es válido.",
                    "Validación"
                );
            }
            else
            {
                MessageBox.Show(
                    "El AFD minimizado no es válido.",
                    "Validación"
                );
            }
        }

        private void btnSimularAFDMin_Click(object sender, EventArgs e)
        {
            if (afdMinimizado == null)
            {
                MessageBox.Show(
                    "No existe un AFD minimizado.",
                    "Simulación"
                );

                return;
            }

            SimuladorAFD simulador =
                new SimuladorAFD(
                    afdMinimizado
                );

            bool aceptada =
                simulador.Simular(
                    txtCadenaAFDMin.Text
                );

            if (aceptada)
            {
                lblResultadoAFDMin.Text =
                    "Resultado: Cadena aceptada";
            }
            else
            {
                lblResultadoAFDMin.Text =
                    "Resultado: Cadena rechazada";
            }
        }
    }
}
