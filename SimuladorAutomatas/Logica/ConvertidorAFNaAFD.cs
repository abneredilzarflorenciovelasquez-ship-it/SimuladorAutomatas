using SimuladorAutomatas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Logica
{
    public class ConvertidorAFNaAFD
    {
        private AutomataEditor afn;

        private StringBuilder procedimiento;

        public ConvertidorAFNaAFD(AutomataEditor afn)
        {
            this.afn = afn;
            procedimiento = new StringBuilder();
        }

        public ResultadoConversion Convertir()
        {
            AutomataEditor afd = new AutomataEditor();

            procedimiento.Clear();

            procedimiento.AppendLine("CONVERSIÓN AFN → AFD");
            procedimiento.AppendLine();
            procedimiento.AppendLine(
                "Método: Construcción por subconjuntos"
            );
            procedimiento.AppendLine();

            // Verificar estado inicial
            if (afn.EstadoInicial == null)
            {
                procedimiento.AppendLine(
                    "El AFN no tiene estado inicial."
                );

                return new ResultadoConversion
                {
                    AFD = afd,
                    Procedimiento =
                        procedimiento.ToString()
                };
            }

            // Crear estado inicial
            List<Estado> conjuntoInicial =
                CierreEpsilon(
                    new List<Estado>
                    {
                        afn.EstadoInicial
                    }
                );

            string nombreInicial =
                ObtenerNombreConjunto(
                    conjuntoInicial
                );

            procedimiento.AppendLine(
                "1. Estado inicial"
            );

            procedimiento.AppendLine(
                "Cierre-ε({" +
                afn.EstadoInicial.Nombre +
                "}) = " +
                nombreInicial
            );

            procedimiento.AppendLine();

            Estado estadoInicial =
                new Estado(
                    nombreInicial,
                    100,
                    150
                );

            afd.AgregarEstado(
                estadoInicial
            );

            afd.EstadoInicial =
                estadoInicial;

            estadoInicial.EsInicial = true;

            // Verificar estado final
            if (ContieneEstadoFinal(
                conjuntoInicial))
            {
                estadoInicial.EsFinal = true;

                afd.EstadosFinales.Add(
                    estadoInicial
                );
            }

            Queue<List<Estado>> pendientes =
                new Queue<List<Estado>>();

            pendientes.Enqueue(
                conjuntoInicial
            );

            List<string> procesados =
                new List<string>();

            while (pendientes.Count > 0)
            {
                List<Estado> conjuntoActual =
                    pendientes.Dequeue();

                string nombreActual =
                    ObtenerNombreConjunto(
                        conjuntoActual
                    );

                if (procesados.Contains(
                    nombreActual))
                {
                    continue;
                }

                procesados.Add(
                    nombreActual
                );

                procedimiento.AppendLine(
                    "Procesando " +
                    nombreActual
                );

                procedimiento.AppendLine();

                foreach (string simbolo
                    in afn.Alfabeto)
                {
                    // Ignorar epsilon
                    if (simbolo == "ε")
                        continue;

                    procedimiento.AppendLine(
                        "Símbolo: " +
                        simbolo
                    );

                    List<Estado> nuevosEstados =
                        Mover(
                            conjuntoActual,
                            simbolo
                        );

                    string resultadoMover =
                        ObtenerNombreConjunto(
                            nuevosEstados
                        );

                    procedimiento.AppendLine(
                        "Mover(" +
                        nombreActual +
                        ", " +
                        simbolo +
                        ") = " +
                        resultadoMover
                    );

                    if (nuevosEstados.Count == 0)
                    {
                        procedimiento.AppendLine();

                        continue;
                    }

                    nuevosEstados =
                        CierreEpsilon(
                            nuevosEstados
                        );

                    string resultadoEpsilon =
                        ObtenerNombreConjunto(
                            nuevosEstados
                        );

                    procedimiento.AppendLine(
                        "Cierre-ε(" +
                        resultadoMover +
                        ") = " +
                        resultadoEpsilon
                    );

                    procedimiento.AppendLine();

                    string nombreDestino =
                        resultadoEpsilon;

                    Estado estadoDestino =
                        BuscarEstado(
                            afd,
                            nombreDestino
                        );

                    if (estadoDestino == null)
                    {
                        int indice =
                            afd.Estados.Count;

                        int x =
                            100 +
                            (indice % 4) * 130;

                        int y =
                            150 +
                            (indice / 4) * 150;

                        estadoDestino =
                            new Estado(
                                nombreDestino,
                                x,
                                y
                            );

                        afd.AgregarEstado(
                            estadoDestino
                        );

                        if (ContieneEstadoFinal(
                            nuevosEstados))
                        {
                            estadoDestino.EsFinal =
                                true;

                            afd.EstadosFinales.Add(
                                estadoDestino
                            );
                        }

                        pendientes.Enqueue(
                            nuevosEstados
                        );

                        procedimiento.AppendLine(
                            "Se crea el estado " +
                            nombreDestino
                        );

                        procedimiento.AppendLine();
                    }

                    afd.AgregarSimbolo(
                        simbolo
                    );

                    Estado estadoOrigen =
                        BuscarEstado(
                            afd,
                            nombreActual
                        );

                    Transicion transicion =
                        new Transicion(
                            estadoOrigen,
                            simbolo,
                            estadoDestino
                        );

                    afd.AgregarTransicion(
                        transicion
                    );
                }

                procedimiento.AppendLine(
                    "--------------------------------"
                );

                procedimiento.AppendLine();
            }

            procedimiento.AppendLine(
                "CONVERSIÓN TERMINADA"
            );

            return new ResultadoConversion
            {
                AFD = afd,
                Procedimiento =
                    procedimiento.ToString()
            };
        }

        private List<Estado> Mover(
            List<Estado> estados,
            string simbolo)
        {
            List<Estado> resultado =
                new List<Estado>();

            foreach (Estado estado
                in estados)
            {
                foreach (Transicion transicion
                    in afn.Transiciones)
                {
                    if (transicion.Origen == estado &&
                        transicion.Simbolo == simbolo)
                    {
                        if (!resultado.Contains(
                            transicion.Destino))
                        {
                            resultado.Add(
                                transicion.Destino
                            );
                        }
                    }
                }
            }

            return resultado;
        }

        private List<Estado> CierreEpsilon(
            List<Estado> estados)
        {
            List<Estado> resultado =
                new List<Estado>(
                    estados
                );

            bool huboCambio = true;

            while (huboCambio)
            {
                huboCambio = false;

                foreach (Estado estado
                    in new List<Estado>(
                        resultado))
                {
                    foreach (Transicion transicion
                        in afn.Transiciones)
                    {
                        if (transicion.Origen == estado &&
                            transicion.EsEpsilon)
                        {
                            if (!resultado.Contains(
                                transicion.Destino))
                            {
                                resultado.Add(
                                    transicion.Destino
                                );

                                huboCambio = true;
                            }
                        }
                    }
                }
            }

            return resultado;
        }

        private bool ContieneEstadoFinal(
            List<Estado> estados)
        {
            foreach (Estado estado
                in estados)
            {
                if (estado.EsFinal)
                    return true;
            }

            return false;
        }

        private string ObtenerNombreConjunto(
            List<Estado> estados)
        {
            if (estados.Count == 0)
                return "∅";

            return "{" +
                string.Join(
                    ",",
                    estados
                        .OrderBy(
                            e => e.Nombre
                        )
                        .Select(
                            e => e.Nombre
                        )
                ) +
                "}";
        }

        private Estado BuscarEstado(
            AutomataEditor automata,
            string nombre)
        {
            foreach (Estado estado
                in automata.Estados)
            {
                if (estado.Nombre == nombre)
                    return estado;
            }

            return null;
        }
    }
}
