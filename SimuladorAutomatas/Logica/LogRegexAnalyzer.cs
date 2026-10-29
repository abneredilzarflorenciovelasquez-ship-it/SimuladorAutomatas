using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SimuladorAutomatas.Logica
{
    public static class LogRegexAnalyzer
    {
        // ==========================================
        // BÚSQUEDA SIMPLE MEDIANTE REGEX
        // ==========================================

        public static List<string> Buscar(
            string rutaArchivo,
            string expresion)
        {
            return Buscar(
                rutaArchivo,
                expresion,
                "Todos",
                "Todos");
        }

        // ==========================================
        // BÚSQUEDA CON FILTROS COMBINADOS
        // ==========================================

        public static List<string> Buscar(
            string rutaArchivo,
            string expresion,
            string nivel,
            string modulo)
        {
            List<string> resultados =
                new List<string>();

            if (!File.Exists(rutaArchivo))
            {
                return resultados;
            }

            if (string.IsNullOrWhiteSpace(expresion))
            {
                return resultados;
            }

            Regex regex;

            try
            {
                regex = new Regex(
                    expresion,
                    RegexOptions.Compiled);
            }
            catch (ArgumentException)
            {
                return resultados;
            }

            string[] lineas =
                File.ReadAllLines(rutaArchivo);

            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea))
                {
                    continue;
                }

                // ==========================================
                // PRIMERO: REGEX
                // ==========================================

                if (!regex.IsMatch(linea))
                {
                    continue;
                }

                // ==========================================
                // PARSEAR REGISTRO
                // ==========================================

                LogEvent evento;
                string detalle;

                if (!LogParser.TryParse(
                    linea,
                    out evento,
                    out detalle))
                {
                    continue;
                }

                // ==========================================
                // FILTRO POR NIVEL
                // ==========================================

                if (!string.IsNullOrWhiteSpace(nivel) &&
                    nivel != "Todos")
                {
                    if (!evento.Nivel.Equals(
                        nivel,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                // ==========================================
                // FILTRO POR MÓDULO
                // ==========================================

                if (!string.IsNullOrWhiteSpace(modulo) &&
                    modulo != "Todos")
                {
                    if (!evento.Modulo.Equals(
                        modulo,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                // ==========================================
                // REGISTRO ACEPTADO
                // ==========================================

                resultados.Add(linea);
            }

            return resultados;
        }
    }
}