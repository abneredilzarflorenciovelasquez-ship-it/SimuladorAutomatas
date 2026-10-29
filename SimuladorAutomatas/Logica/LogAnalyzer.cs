using System.IO;
using System.Text;

namespace SimuladorAutomatas.Logica
{
    public static class LogAnalyzer
    {
        public static LogAnalysisResult Analyze(string rutaArchivo)
        {
            LogAnalysisResult resultado = new LogAnalysisResult();

            if (!File.Exists(rutaArchivo))
                return resultado;

            string[] lineas = File.ReadAllLines(rutaArchivo, Encoding.UTF8);

            for (int i = 0; i < lineas.Length; i++)
            {
                string linea = lineas[i];
                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                resultado.Total++;

                LogEvent evento;
                string detalle;
                if (LogParser.TryParse(linea, out evento, out detalle))
                {
                    resultado.Validos++;
                    resultado.Eventos.Add(evento);

                    if (evento.Nivel == "INFO") resultado.Info++;
                    else if (evento.Nivel == "WARNING") resultado.Warning++;
                    else if (evento.Nivel == "ERROR") resultado.Error++;
                    else if (evento.Nivel == "CRITICAL") resultado.Critical++;
                }
                else
                {
                    resultado.Invalidos++;
                    resultado.RegistrosInvalidos.Add("Línea " + (i + 1) + ": " + detalle + " | " + linea);
                }
            }

            return resultado;
        }
    }
}
