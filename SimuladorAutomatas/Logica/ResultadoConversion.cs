using SimuladorAutomatas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Logica
{
    public class ResultadoConversion
    {
        public AutomataEditor AFD { get; set; }

        public string Procedimiento { get; set; }

        public ResultadoConversion()
        {
            AFD = new AutomataEditor();
            Procedimiento = "";
        }
    }
}
