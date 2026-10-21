using System;

namespace SimuladorAutomatas.Logica
{
    public class LogEvent
    {
        public DateTime FechaHora { get; set; }
        public string Nivel { get; set; }
        public string Modulo { get; set; }
        public string Mensaje { get; set; }
    }
}
