using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class U1Salida
    {
        // Propiedades de debug

        public bool _Error { get; set; } // Indica si hubo un error
        public string _MsjError { get; set; } // Detalle de dicho error
        public string _Metodo { get; set; } // Método utilizado para la resolución
        
        // Propiedades del cálculo

        public bool Converge { get; set; }        
        public int Iteraciones { get; set; }
        public double ErrorRelativo { get; set; }
        public double Raiz { get; set; }
        
        // Métodos

        public void AgregarMsjError(string msj)
        {
            _Error = true;
            _MsjError = msj;
        }
    }
}
