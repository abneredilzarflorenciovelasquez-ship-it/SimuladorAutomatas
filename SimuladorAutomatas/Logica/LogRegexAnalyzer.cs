using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Logica
{
    public static class LogRegexAnalyzer
    {
        public static List<string> Buscar(string rutaArchivo, string expresion)
        {
            List<string> resultados = new List<string>();

            if (!File.Exists(rutaArchivo))
                return resultados;

            if (string.IsNullOrWhiteSpace(expresion))
                return resultados;

            Regex regex;

            try
            {
                regex = new Regex(expresion, RegexOptions.Compiled);
            }
            catch (ArgumentException)
            {
                return resultados;
            }

            string[] lineas = File.ReadAllLines(rutaArchivo);

            foreach (string linea in lineas)
            {
                if (regex.IsMatch(linea))
                {
                    resultados.Add(linea);
                }
            }

            return resultados;
        }
    }
}
