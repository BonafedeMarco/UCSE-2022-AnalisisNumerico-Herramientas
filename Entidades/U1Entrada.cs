using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class U1Entrada
    {
        public string Funcion { get; set; }
        public double Xi { get; set; } // En caso de ser un método abierto, se usa como "XIni"
        public double Xd { get; set; }
        public int MaxIter { get; set; }
        public double Tolerancia { get; set; }
    }
}
