using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Entrada
    {
        public string Funcion { get; set; }
        public double Xi { get; set; } //Xini
        public double Xd { get; set; }
        public int MaxIter { get; set; }
        public double ErrorRelat { get; set; }
        public double Tolerancia { get; set; }
    }
}
