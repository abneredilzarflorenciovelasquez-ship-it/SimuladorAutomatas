using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimuladorAutomatas.Modelos;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Logica
{
    public class SimuladorAFD
    {
        private AutomataEditor automata;

        public SimuladorAFD(AutomataEditor automata)
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

                // Verificar epsilon
                if (transicion.EsEpsilon)
                    return false;

                // Verificar símbolo
                if (!automata.Alfabeto.Contains(transicion.Simbolo))
                    return false;
            }

            // Verificar transiciones repetidas
            for (int i = 0; i < automata.Transiciones.Count; i++)
            {
                for (int j = i + 1; j < automata.Transiciones.Count; j++)
                {
                    Transicion t1 = automata.Transiciones[i];
                    Transicion t2 = automata.Transiciones[j];

                    if (t1.Origen == t2.Origen &&
                        t1.Simbolo == t2.Simbolo)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool Simular(string cadena)
        {
            // Verificar estado inicial
            if (automata.EstadoInicial == null)
                return false;

            Estado estadoActual = automata.EstadoInicial;

            // Recorrer la cadena
            foreach (char caracter in cadena)
            {
                string simbolo = caracter.ToString();

                Transicion transicionEncontrada = null;

                foreach (Transicion transicion in automata.Transiciones)
                {
                    if (transicion.Origen == estadoActual &&
                        transicion.Simbolo == simbolo)
                    {
                        transicionEncontrada = transicion;
                        break;
                    }
                }

                // Si no existe transición
                if (transicionEncontrada == null)
                    return false;

                estadoActual = transicionEncontrada.Destino;
            }

            // Verificar estado final
            return estadoActual.EsFinal;
        }
    }
}
