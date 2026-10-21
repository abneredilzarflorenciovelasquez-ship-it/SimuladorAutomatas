using System.Collections.Generic;

namespace SimuladorAutomatas.Logica
{
    public class LogAnalysisResult
    {
        public int Total { get; set; }
        public int Validos { get; set; }
        public int Invalidos { get; set; }
        public int Info { get; set; }
        public int Warning { get; set; }
        public int Error { get; set; }
        public List<LogEvent> Eventos { get; private set; }
        public List<string> RegistrosInvalidos { get; private set; }

        public LogAnalysisResult()
        {
            Eventos = new List<LogEvent>();
            RegistrosInvalidos = new List<string>();
        }
    }
}
