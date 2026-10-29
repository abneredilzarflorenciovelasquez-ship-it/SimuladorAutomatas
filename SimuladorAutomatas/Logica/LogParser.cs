using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SimuladorAutomatas.Logica
{
    public static class LogParser
    {
        private static readonly Regex Formato = new Regex(
            @"^(?<fecha>\d{4}-\d{2}-\d{2}) (?<hora>\d{2}:\d{2}:\d{2}) (?<nivel>INFO|WARNING|ERROR|CRITICAL) (?<modulo>[A-Z_]+) (?<mensaje>.+)$",
            RegexOptions.Compiled);

        public static bool TryParse(string linea, out LogEvent evento, out string detalle)
        {
            evento = null;
            detalle = string.Empty;

            if (string.IsNullOrWhiteSpace(linea))
            {
                detalle = "La línea está vacía.";
                return false;
            }

            Match coincidencia = Formato.Match(linea.Trim());
            if (!coincidencia.Success)
            {
                detalle = "Formato incorrecto. Use AAAA-MM-DD HH:MM:SS NIVEL MODULO Mensaje.";
                return false;
            }

            DateTime fechaHora;
            if (!DateTime.TryParseExact(
                coincidencia.Groups["fecha"].Value + " " + coincidencia.Groups["hora"].Value,
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fechaHora))
            {
                detalle = "La fecha u hora no es válida.";
                return false;
            }

            evento = new LogEvent
            {
                FechaHora = fechaHora,
                Nivel = coincidencia.Groups["nivel"].Value,
                Modulo = coincidencia.Groups["modulo"].Value,
                Mensaje = coincidencia.Groups["mensaje"].Value
            };

            detalle = "El registro cumple con el formato.";
            return true;
        }
    }
}
