using SimuladorAutomatas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Logica
{
    public class MinimizadorAFD
    {
        private AutomataEditor afd;
        private StringBuilder procedimiento;

        public MinimizadorAFD(AutomataEditor afd)
        {
            this.afd = afd;
            procedimiento = new StringBuilder();
        }

        public ResultadoConversion Minimizar()
        {
            AutomataEditor resultado =
                new AutomataEditor();

            procedimiento.Clear();

            procedimiento.AppendLine(
                "MINIMIZACIÓN DE AFD"
            );

            procedimiento.AppendLine();

            // Verificar estado inicial
            if (afd.EstadoInicial == null)
            {
                procedimiento.AppendLine(
                    "El AFD no tiene estado inicial."
                );

                return new ResultadoConversion
                {
                    AFD = resultado,
                    Procedimiento =
                        procedimiento.ToString()
                };
            }

            // Verificar que sea un AFD válido
            SimuladorAFD simulador =
                new SimuladorAFD(afd);

            if (!simulador.Validar())
            {
                procedimiento.AppendLine(
                    "El autómata no es un AFD válido."
                );

                return new ResultadoConversion
                {
                    AFD = resultado,
                    Procedimiento =
                        procedimiento.ToString()
                };
            }

            procedimiento.AppendLine(
                "Método: Tabla de equivalencias"
            );

            procedimiento.AppendLine();

            // Crear tabla de pares
            List<ParEstados> pares =
                CrearPares();

            procedimiento.AppendLine(
                "1. Tabla inicial"
            );

            procedimiento.AppendLine();

            // Marcar pares donde uno es final
            foreach (ParEstados par in pares)
            {
                if (par.Estado1.EsFinal !=
                    par.Estado2.EsFinal)
                {
                    par.Distintos = true;
                }
            }

            procedimiento.AppendLine(
                "2. Se marcan los pares donde " +
                "un estado es final y el otro no."
            );

            procedimiento.AppendLine();

            bool cambio = true;

            while (cambio)
            {
                cambio = false;

                foreach (ParEstados par in pares)
                {
                    if (par.Distintos)
                        continue;

                    foreach (string simbolo
                        in afd.Alfabeto)
                    {
                        Estado destino1 =
                            ObtenerDestino(
                                par.Estado1,
                                simbolo
                            );

                        Estado destino2 =
                            ObtenerDestino(
                                par.Estado2,
                                simbolo
                            );

                        if (destino1 == null ||
                            destino2 == null)
                        {
                            continue;
                        }

                        if (destino1 == destino2)
                            continue;

                        ParEstados parDestino =
                            BuscarPar(
                                pares,
                                destino1,
                                destino2
                            );

                        if (parDestino != null &&
                            parDestino.Distintos)
                        {
                            par.Distintos = true;
                            cambio = true;

                            break;
                        }
                    }
                }
            }

            procedimiento.AppendLine(
                "3. Se revisan las transiciones."
            );

            procedimiento.AppendLine();

            List<List<Estado>> grupos =
                CrearGrupos(pares);

            procedimiento.AppendLine(
                "4. Estados equivalentes:"
            );

            procedimiento.AppendLine();

            foreach (List<Estado> grupo
                in grupos)
            {
                procedimiento.AppendLine(
                    ObtenerNombreGrupo(grupo)
                );
            }

            procedimiento.AppendLine();

            CrearAFDMinimizado(
                resultado,
                grupos
            );

            procedimiento.AppendLine(
                "5. AFD minimizado creado."
            );

            procedimiento.AppendLine();

            return new ResultadoConversion
            {
                AFD = resultado,
                Procedimiento =
                    procedimiento.ToString()
            };
        }

        private List<ParEstados> CrearPares()
        {
            List<ParEstados> pares =
                new List<ParEstados>();

            for (int i = 0;
                i < afd.Estados.Count;
                i++)
            {
                for (int j = i + 1;
                    j < afd.Estados.Count;
                    j++)
                {
                    pares.Add(
                        new ParEstados(
                            afd.Estados[i],
                            afd.Estados[j]
                        )
                    );
                }
            }

            return pares;
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

        private ParEstados BuscarPar(
            List<ParEstados> pares,
            Estado estado1,
            Estado estado2)
        {
            foreach (ParEstados par in pares)
            {
                if ((par.Estado1 == estado1 &&
                     par.Estado2 == estado2) ||
                    (par.Estado1 == estado2 &&
                     par.Estado2 == estado1))
                {
                    return par;
                }
            }

            return null;
        }

        private List<List<Estado>> CrearGrupos(
            List<ParEstados> pares)
        {
            List<List<Estado>> grupos =
                new List<List<Estado>>();

            foreach (Estado estado in afd.Estados)
            {
                bool agregado = false;

                foreach (List<Estado> grupo
                    in grupos)
                {
                    Estado representante =
                        grupo[0];

                    ParEstados par =
                        BuscarPar(
                            pares,
                            estado,
                            representante
                        );

                    if (estado == representante ||
                        (par != null &&
                         !par.Distintos))
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

        private string ObtenerNombreGrupo(
            List<Estado> grupo)
        {
            return "{" +
                string.Join(
                    ",",
                    grupo
                        .OrderBy(
                            e => e.Nombre
                        )
                        .Select(
                            e => e.Nombre
                        )
                ) +
                "}";
        }

        private void CrearAFDMinimizado(
            AutomataEditor resultado,
            List<List<Estado>> grupos)
        {
            Dictionary<Estado, Estado>
                equivalencias =
                new Dictionary<Estado, Estado>();

            int posicion = 0;

            foreach (List<Estado> grupo
                in grupos)
            {
                Estado nuevo =
                    new Estado(
                        ObtenerNombreGrupo(grupo),
                        100 +
                        (posicion % 4) * 130,
                        150 +
                        (posicion / 4) * 150
                    );

                posicion++;

                resultado.AgregarEstado(nuevo);

                if (grupo.Contains(
                    afd.EstadoInicial))
                {
                    nuevo.EsInicial = true;
                    resultado.EstadoInicial = nuevo;
                }

                foreach (Estado estado
                    in grupo)
                {
                    equivalencias[estado] =
                        nuevo;

                    if (estado.EsFinal)
                    {
                        nuevo.EsFinal = true;

                        if (!resultado.EstadosFinales
                            .Contains(nuevo))
                        {
                            resultado.EstadosFinales.Add(
                                nuevo
                            );
                        }
                    }
                }
            }

            foreach (string simbolo
                in afd.Alfabeto)
            {
                resultado.AgregarSimbolo(
                    simbolo
                );
            }

            foreach (List<Estado> grupo
                in grupos)
            {
                Estado representante =
                    grupo[0];

                foreach (string simbolo
                    in afd.Alfabeto)
                {
                    Estado destino =
                        ObtenerDestino(
                            representante,
                            simbolo
                        );

                    if (destino == null)
                        continue;

                    Estado origenNuevo =
                        equivalencias[
                            representante
                        ];

                    Estado destinoNuevo =
                        equivalencias[
                            destino
                        ];

                    bool existe = resultado
                        .Transiciones
                        .Any(
                            t =>
                            t.Origen ==
                            origenNuevo &&
                            t.Destino ==
                            destinoNuevo &&
                            t.Simbolo ==
                            simbolo
                        );

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
        }

        private class ParEstados
        {
            public Estado Estado1 { get; set; }

            public Estado Estado2 { get; set; }

            public bool Distintos { get; set; }

            public ParEstados(
                Estado estado1,
                Estado estado2)
            {
                Estado1 = estado1;
                Estado2 = estado2;
                Distintos = false;
            }
        }
    }
}
