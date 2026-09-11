using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Modelos
{
    public class Transicion
    {
        public Estado Origen { get; set; }

        public string Simbolo { get; set; }

        public Estado Destino { get; set; }

        public Transicion(Estado origen, string simbolo, Estado destino)
        {
            Origen = origen;
            Simbolo = simbolo;
            Destino = destino;
        }

        public override string ToString()
        {
            return $"{Origen} --{Simbolo}--> {Destino}";
        }
    }
}
