using SimuladorAutomatas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Logica
{
    public class SimuladorAFN
    {
        private AutomataEditor automata;

        public SimuladorAFN(AutomataEditor automata)
        {
            this.automata = automata;
        }

        public bool Validar()
        {
            // Verificar estado inicial
            if (automata.EstadoInicial == null)
                return false;

            // Verificar estados
            if (automata.Estados.Count == 0)
                return false;

            // Verificar transiciones
            foreach (Transicion transicion in automata.Transiciones)
            {
                // Verificar origen
                if (!automata.Estados.Contains(transicion.Origen))
                    return false;

                // Verificar destino
                if (!automata.Estados.Contains(transicion.Destino))
                    return false;

                // Verificar símbolo
                if (string.IsNullOrWhiteSpace(transicion.Simbolo))
                    return false;

                // Verificar símbolo
                if (!transicion.EsEpsilon &&
                    !automata.Alfabeto.Contains(transicion.Simbolo))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Simular(string cadena)
        {
            // Verificar estado inicial
            if (automata.EstadoInicial == null)
                return false;

            List<Estado> estadosActuales = new List<Estado>();

            estadosActuales.Add(automata.EstadoInicial);

            // Aplicar epsilon
            estadosActuales = CierreEpsilon(estadosActuales);

            // Recorrer la cadena
            foreach (char caracter in cadena)
            {
                string simbolo = caracter.ToString();

                List<Estado> nuevosEstados = new List<Estado>();

                // Buscar transiciones
                foreach (Estado estado in estadosActuales)
                {
                    foreach (Transicion transicion in automata.Transiciones)
                    {
                        if (transicion.Origen == estado &&
                            transicion.Simbolo == simbolo)
                        {
                            if (!nuevosEstados.Contains(transicion.Destino))
                            {
                                nuevosEstados.Add(transicion.Destino);
                            }
                        }
                    }
                }

                // Aplicar epsilon
                estadosActuales = CierreEpsilon(nuevosEstados);

                // Si no hay estados
                if (estadosActuales.Count == 0)
                    return false;
            }

            // Verificar estados finales
            foreach (Estado estado in estadosActuales)
            {
                if (estado.EsFinal)
                    return true;
            }

            return false;
        }

        private List<Estado> CierreEpsilon(List<Estado> estados)
        {
            List<Estado> resultado = new List<Estado>(estados);

            bool huboCambio = true;

            while (huboCambio)
            {
                huboCambio = false;

                foreach (Estado estado in new List<Estado>(resultado))
                {
                    foreach (Transicion transicion in automata.Transiciones)
                    {
                        if (transicion.Origen == estado &&
                            transicion.EsEpsilon)
                        {
                            if (!resultado.Contains(transicion.Destino))
                            {
                                resultado.Add(transicion.Destino);
                                huboCambio = true;
                            }
                        }
                    }
                }
            }

            return resultado;
        }
    }
}
