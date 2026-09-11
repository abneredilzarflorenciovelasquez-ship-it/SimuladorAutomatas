using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorAutomatas.Modelos
{
    public abstract class Automata
    {
        public List<Estado> Estados { get; set; }

        public List<string> Alfabeto { get; set; }

        public Estado EstadoInicial { get; set; }

        public List<Estado> EstadosFinales { get; set; }

        public List<Transicion> Transiciones { get; set; }

        public Automata()
        {
            Estados = new List<Estado>();
            Alfabeto = new List<string>();
            EstadosFinales = new List<Estado>();
            Transiciones = new List<Transicion>();
        }

        public void AgregarEstado(Estado estado)
        {
            Estados.Add(estado);
        }

        public void AgregarSimbolo(string simbolo)
        {
            if (!Alfabeto.Contains(simbolo))
            {
                Alfabeto.Add(simbolo);
            }
        }

        public void AgregarTransicion(Transicion transicion)
        {
            Transiciones.Add(transicion);
        }
    }
}
