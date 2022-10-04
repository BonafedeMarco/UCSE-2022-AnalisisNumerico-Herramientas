using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public sealed class U3Entrada
    {
        public List<double[]> PuntosCargados { get; set; }
        public double Tolerancia { get; set; }
        public int MetodoUtilizado { get; set; } //0: Regresión Lineal
    }
}
