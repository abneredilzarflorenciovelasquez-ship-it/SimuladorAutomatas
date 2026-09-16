using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimuladorAutomatas.Modelos
{
    public static class GestorProyectos
    {
        public static void Guardar(
            AutomataEditor automata,
            string ruta)
        {
            XElement raiz = new XElement(
                "Automata"
            );

            // Guardar alfabeto
            XElement alfabeto =
                new XElement("Alfabeto");

            foreach (string simbolo in automata.Alfabeto)
            {
                alfabeto.Add(
                    new XElement(
                        "Simbolo",
                        simbolo
                    )
                );
            }

            raiz.Add(alfabeto);

            // Guardar estados
            XElement estados =
                new XElement("Estados");

            foreach (Estado estado in automata.Estados)
            {
                XElement elementoEstado =
                    new XElement(
                        "Estado",
                        new XAttribute(
                            "Nombre",
                            estado.Nombre
                        ),
                        new XAttribute(
                            "X",
                            estado.X
                        ),
                        new XAttribute(
                            "Y",
                            estado.Y
                        ),
                        new XAttribute(
                            "EsInicial",
                            estado.EsInicial
                        ),
                        new XAttribute(
                            "EsFinal",
                            estado.EsFinal
                        )
                    );

                estados.Add(elementoEstado);
            }

            raiz.Add(estados);

            // Guardar transiciones
            XElement transiciones =
                new XElement("Transiciones");

            foreach (
                Transicion transicion
                in automata.Transiciones)
            {
                XElement elementoTransicion =
                    new XElement(
                        "Transicion",
                        new XAttribute(
                            "Origen",
                            transicion.Origen.Nombre
                        ),
                        new XAttribute(
                            "Simbolo",
                            transicion.Simbolo
                        ),
                        new XAttribute(
                            "Destino",
                            transicion.Destino.Nombre
                        )
                    );

                transiciones.Add(
                    elementoTransicion
                );
            }

            raiz.Add(transiciones);

            XDocument documento =
                new XDocument(
                    new XDeclaration(
                        "1.0",
                        "utf-8",
                        "yes"
                    ),
                    raiz
                );

            documento.Save(ruta);
        }

        public static AutomataEditor Cargar(
            string ruta)
        {
            XDocument documento =
                XDocument.Load(ruta);

            AutomataEditor automata =
                new AutomataEditor();

            XElement raiz =
                documento.Element("Automata");

            if (raiz == null)
                throw new Exception(
                    "El archivo no contiene un proyecto válido."
                );

            // Cargar alfabeto
            XElement alfabeto =
                raiz.Element("Alfabeto");

            if (alfabeto != null)
            {
                foreach (
                    XElement elemento
                    in alfabeto.Elements("Simbolo"))
                {
                    automata.AgregarSimbolo(
                        elemento.Value
                    );
                }
            }

            // Cargar estados
            XElement estados =
                raiz.Element("Estados");

            if (estados != null)
            {
                foreach (
                    XElement elemento
                    in estados.Elements("Estado"))
                {
                    string nombre =
                        (string)elemento.Attribute(
                            "Nombre"
                        );

                    int x =
                        (int)elemento.Attribute("X");

                    int y =
                        (int)elemento.Attribute("Y");

                    bool esInicial =
                        (bool)elemento.Attribute(
                            "EsInicial"
                        );

                    bool esFinal =
                        (bool)elemento.Attribute(
                            "EsFinal"
                        );

                    Estado estado =
                        new Estado(
                            nombre,
                            x,
                            y
                        );

                    estado.EsInicial =
                        esInicial;

                    estado.EsFinal =
                        esFinal;

                    automata.AgregarEstado(
                        estado
                    );

                    if (esInicial)
                    {
                        automata.EstadoInicial =
                            estado;
                    }

                    if (esFinal)
                    {
                        automata.EstadosFinales.Add(
                            estado
                        );
                    }
                }
            }

            // Cargar transiciones
            XElement transiciones =
                raiz.Element("Transiciones");

            if (transiciones != null)
            {
                foreach (
                    XElement elemento
                    in transiciones.Elements(
                        "Transicion"))
                {
                    string nombreOrigen =
                        (string)elemento.Attribute(
                            "Origen"
                        );

                    string simbolo =
                        (string)elemento.Attribute(
                            "Simbolo"
                        );

                    string nombreDestino =
                        (string)elemento.Attribute(
                            "Destino"
                        );

                    Estado origen =
                        automata.Estados.FirstOrDefault(
                            e => e.Nombre ==
                            nombreOrigen
                        );

                    Estado destino =
                        automata.Estados.FirstOrDefault(
                            e => e.Nombre ==
                            nombreDestino
                        );

                    if (origen == null ||
                        destino == null)
                    {
                        continue;
                    }

                    Transicion transicion =
                        new Transicion(
                            origen,
                            simbolo,
                            destino
                        );

                    automata.AgregarTransicion(
                        transicion
                    );
                }
            }

            return automata;
        }
    }
}
