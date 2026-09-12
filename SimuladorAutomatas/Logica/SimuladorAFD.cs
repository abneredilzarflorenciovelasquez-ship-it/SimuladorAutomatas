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

            // Verificar si terminó en un estado final
            return estadoActual.EsFinal;
        }
    }
}
