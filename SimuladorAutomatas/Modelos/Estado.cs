using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Modelos
{
    public class Estado
    {
        public string Nombre { get; set; }

        public bool EsInicial { get; set; }

        public bool EsFinal { get; set; }

        public Estado(string nombre)
        {
            Nombre = nombre;
            EsInicial = false;
            EsFinal = false;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
