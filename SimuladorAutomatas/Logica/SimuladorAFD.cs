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
            // Debe existir un estado inicial
            if (automata.EstadoInicial == null)
                return false;

            // Debe haber estados
            if (automata.Estados.Count == 0)
                return false;

            // El estado inicial debe pertenecer al automata
            if (!automata.Estados.Contains(
                automata.EstadoInicial))
            {
                return false;
            }

            // Comprobar que todas las transiciones sean válidas
            foreach (Transicion transicion
                in automata.Transiciones)
            {
                if (!automata.Estados.Contains(
                    transicion.Origen))
                {
                    return false;
                }

                if (!automata.Estados.Contains(
                    transicion.Destino))
                {
                    return false;
                }

                // Un AFD no permite transiciones epsilon
                if (transicion.EsEpsilon)
                    return false;

                // El símbolo debe pertenecer al alfabeto
                if (!automata.Alfabeto.Contains(
                    transicion.Simbolo))
                {
                    return false;
                }
            }

            // Comprobar que no haya transiciones duplicadas
            for (int i = 0;
                i < automata.Transiciones.Count;
                i++)
            {
                for (int j = i + 1;
                    j < automata.Transiciones.Count;
                    j++)
                {
                    Transicion t1 =
                        automata.Transiciones[i];

                    Transicion t2 =
                        automata.Transiciones[j];

                    if (t1.Origen == t2.Origen &&
                        t1.Simbolo == t2.Simbolo)
                    {
                        return false;
                    }
                }
            }

            // NUEVO: comprobar que todas las transiciones estén definidas
            foreach (Estado estado in automata.Estados)
            {
                foreach (string simbolo in automata.Alfabeto)
                {
                    int cantidadTransiciones = 0;

                    foreach (Transicion transicion
                        in automata.Transiciones)
                    {
                        if (transicion.Origen == estado &&
                            transicion.Simbolo == simbolo)
                        {
                            cantidadTransiciones++;
                        }
                    }

                    // Debe existir exactamente una transición
                    if (cantidadTransiciones != 1)
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

            // Verificar alfabeto
            if (automata.Alfabeto.Count == 0)
                return false;

            // Verificar símbolos de la cadena
            foreach (char caracter in cadena)
            {
                string simbolo = caracter.ToString();

                if (!automata.Alfabeto.Contains(simbolo))
                    return false;
            }

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
