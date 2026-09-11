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

        public int X { get; set; }

        public int Y { get; set; }

        public bool EsInicial { get; set; }

        public bool EsFinal { get; set; }

        public Estado(string nombre, int x, int y)
        {
            Nombre = nombre;
            X = x;
            Y = y;
            EsInicial = false;
            EsFinal = false;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
