using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public sealed class U2Salida
    {
        public bool _Error { get; set; } // Indica si hubo un error
        public string _MsjError { get; set; } // Detalle de dicho error
        public string _Metodo { get; set; } // Método utilizado para la resolución
        public double[] Resultado { get; set; }
        public int Iteraciones { get; set; }

        // Métodos
        public void AgregarMsjError(string msj)
        {
            _Error = true;
            _MsjError = msj;
        }
    }
}
