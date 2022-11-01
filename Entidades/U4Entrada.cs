using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public sealed class U4Entrada
    {
        public string Funcion { get; set; }
        public double PuntoA { get; set; }
        public double PuntoB { get; set; }
        public int CantidadSubintervalos { get; set; }
        public int Metodo { get; set; }
    }
}
