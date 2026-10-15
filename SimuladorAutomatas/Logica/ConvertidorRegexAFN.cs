using SimuladorAutomatas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Logica
{
    public class ConvertidorRegexAFN
    {
        private AutomataEditor automata;
        private string expresion;
        private int posicion;
        private int contadorEstados;

        private class Fragmento
        {
            public Estado Inicial { get; set; }
            public Estado Final { get; set; }

            public Fragmento(Estado inicial, Estado final)
            {
                Inicial = inicial;
                Final = final;
            }
        }

        public AutomataEditor Convertir(string texto)
        {
            ValidadorExpresionRegular validador =
                new ValidadorExpresionRegular();

            string mensaje;

            if (!validador.Validar(texto, out mensaje))
                throw new ArgumentException(mensaje);

            expresion = texto;
            posicion = 0;
            contadorEstados = 0;

            automata = new AutomataEditor();

            Fragmento resultado = ParsearExpresion();

            if (posicion != expresion.Length)
                throw new ArgumentException(
                    "No se pudo interpretar toda la expresión.");

            resultado.Inicial.EsInicial = true;
            resultado.Final.EsFinal = true;

            automata.EstadoInicial = resultado.Inicial;
            automata.EstadosFinales.Add(resultado.Final);

            return automata;
        }

        private Fragmento ParsearExpresion()
        {
            Fragmento izquierda = ParsearConcatenacion();

            while (posicion < expresion.Length
                && expresion[posicion] == '|')
            {
                posicion++;

                Fragmento derecha = ParsearConcatenacion();

                izquierda = CrearUnion(izquierda, derecha);
            }

            return izquierda;
        }

        private Fragmento ParsearConcatenacion()
        {
            Fragmento resultado = ParsearRepeticion();

            while (posicion < expresion.Length
                && expresion[posicion] != ')'
                && expresion[posicion] != '|')
            {
                Fragmento siguiente = ParsearRepeticion();

                resultado = CrearConcatenacion(
                    resultado, siguiente);
            }

            return resultado;
        }

        private Fragmento ParsearRepeticion()
        {
            Fragmento fragmento = ParsearAtomo();

            if (posicion < expresion.Length)
            {
                char operador = expresion[posicion];

                if (operador == '*'
                    || operador == '+'
                    || operador == '?')
                {
                    posicion++;

                    if (operador == '*')
                        fragmento = CrearKleene(fragmento);
                    else if (operador == '+')
                        fragmento = CrearPositiva(fragmento);
                    else
                        fragmento = CrearOpcional(fragmento);
                }
            }

            return fragmento;
        }

        private Fragmento ParsearAtomo()
        {
            if (posicion >= expresion.Length)
                throw new ArgumentException(
                    "Falta un símbolo en la expresión.");

            char simbolo = expresion[posicion];

            if (simbolo == '(')
            {
                posicion++;

                Fragmento fragmento = ParsearExpresion();

                if (posicion >= expresion.Length
                    || expresion[posicion] != ')')
                {
                    throw new ArgumentException(
                        "Falta cerrar un paréntesis.");
                }

                posicion++;

                return fragmento;
            }

            posicion++;

            Estado inicial = CrearEstado();
            Estado final = CrearEstado();

            string etiqueta = simbolo.ToString();

            if (simbolo == 'ε')
            {
                etiqueta = "ε";
            }
            else
            {
                automata.AgregarSimbolo(etiqueta);
            }

            automata.AgregarTransicion(
                new Transicion(inicial, etiqueta, final));

            return new Fragmento(inicial, final);
        }

        private Fragmento CrearConcatenacion(
            Fragmento primero, Fragmento segundo)
        {
            automata.AgregarTransicion(
                new Transicion(
                    primero.Final, "ε", segundo.Inicial));

            return new Fragmento(
                primero.Inicial, segundo.Final);
        }

        private Fragmento CrearUnion(
            Fragmento izquierda, Fragmento derecha)
        {
            Estado inicial = CrearEstado();
            Estado final = CrearEstado();

            automata.AgregarTransicion(
                new Transicion(inicial, "ε", izquierda.Inicial));

            automata.AgregarTransicion(
                new Transicion(inicial, "ε", derecha.Inicial));

            automata.AgregarTransicion(
                new Transicion(izquierda.Final, "ε", final));

            automata.AgregarTransicion(
                new Transicion(derecha.Final, "ε", final));

            return new Fragmento(inicial, final);
        }

        private Fragmento CrearKleene(Fragmento original)
        {
            Estado inicial = CrearEstado();
            Estado final = CrearEstado();

            automata.AgregarTransicion(
                new Transicion(inicial, "ε", original.Inicial));

            automata.AgregarTransicion(
                new Transicion(inicial, "ε", final));

            automata.AgregarTransicion(
                new Transicion(original.Final, "ε", original.Inicial));

            automata.AgregarTransicion(
                new Transicion(original.Final, "ε", final));

            return new Fragmento(inicial, final);
        }

        private Fragmento CrearPositiva(Fragmento original)
        {
            Estado inicial = CrearEstado();
            Estado final = CrearEstado();

            automata.AgregarTransicion(
                new Transicion(inicial, "ε", original.Inicial));

            automata.AgregarTransicion(
                new Transicion(original.Final, "ε", original.Inicial));

            automata.AgregarTransicion(
                new Transicion(original.Final, "ε", final));

            return new Fragmento(inicial, final);
        }

        private Fragmento CrearOpcional(Fragmento original)
        {
            Estado inicial = CrearEstado();
            Estado final = CrearEstado();

            automata.AgregarTransicion(
                new Transicion(inicial, "ε", original.Inicial));

            automata.AgregarTransicion(
                new Transicion(inicial, "ε", final));

            automata.AgregarTransicion(
                new Transicion(original.Final, "ε", final));

            return new Fragmento(inicial, final);
        }

        private Estado CrearEstado()
        {
            int numero = contadorEstados++;

            Estado estado = new Estado(
                "q" + numero,
                80 + (numero % 6) * 110,
                100 + (numero / 6) * 100);

            automata.AgregarEstado(estado);

            return estado;
        }
    }
}
