using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Logica
{
    public class ValidadorExpresionRegular
    {
        private string expresion;
        private int posicion;

        public bool Validar(string texto, out string mensaje)
        {
            expresion = texto;
            posicion = 0;

            if (string.IsNullOrEmpty(expresion))
            {
                mensaje = "Ingresa una expresión regular.";
                return false;
            }

            string error;

            if (!ParsearExpresion(out error))
            {
                mensaje = error;
                return false;
            }

            if (posicion != expresion.Length)
            {
                mensaje = "Hay un símbolo inesperado en la posición "
                    + (posicion + 1) + ".";
                return false;
            }

            mensaje = "La expresión regular tiene una sintaxis válida.";
            return true;
        }

        private bool ParsearExpresion(out string error)
        {
            if (!ParsearConcatenacion(out error))
                return false;

            while (posicion < expresion.Length
                && expresion[posicion] == '|')
            {
                posicion++;

                if (!ParsearConcatenacion(out error))
                {
                    error = "Falta una expresión después de '|'.";
                    return false;
                }
            }

            error = "";
            return true;
        }

        private bool ParsearConcatenacion(out string error)
        {
            bool encontroElemento = false;

            while (posicion < expresion.Length
                && expresion[posicion] != ')'
                && expresion[posicion] != '|')
            {
                if (!ParsearRepeticion(out error))
                    return false;

                encontroElemento = true;
            }

            if (!encontroElemento)
            {
                error = "Se esperaba un símbolo o un grupo válido.";
                return false;
            }

            error = "";
            return true;
        }

        private bool ParsearRepeticion(out string error)
        {
            if (!ParsearAtomo(out error))
                return false;

            if (posicion < expresion.Length
                && EsOperadorRepeticion(expresion[posicion]))
            {
                posicion++;

                if (posicion < expresion.Length
                    && EsOperadorRepeticion(expresion[posicion]))
                {
                    error = "No se pueden encadenar operadores "
                        + "de repetición sin un símbolo intermedio.";
                    return false;
                }
            }

            error = "";
            return true;
        }

        private bool ParsearAtomo(out string error)
        {
            if (posicion >= expresion.Length)
            {
                error = "Falta un símbolo.";
                return false;
            }

            char simbolo = expresion[posicion];

            if (char.IsWhiteSpace(simbolo))
            {
                error = "No se permiten espacios dentro de la expresión.";
                return false;
            }

            if (simbolo == '(')
            {
                posicion++;

                if (!ParsearExpresion(out error))
                    return false;

                if (posicion >= expresion.Length
                    || expresion[posicion] != ')')
                {
                    error = "Falta cerrar un paréntesis.";
                    return false;
                }

                posicion++;
                error = "";
                return true;
            }

            if (simbolo == ')'
                || simbolo == '|'
                || EsOperadorRepeticion(simbolo))
            {
                error = "El símbolo '" + simbolo
                    + "' está en una posición incorrecta.";
                return false;
            }

            // Cualquier otro carácter se considera un símbolo literal.
            posicion++;
            error = "";
            return true;
        }

        private bool EsOperadorRepeticion(char simbolo)
        {
            return simbolo == '*'
                || simbolo == '+'
                || simbolo == '?';
        }
    }
}
