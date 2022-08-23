using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Salida
    {
        public bool _Error { get; set; }
        public string _MsjError { get; set; }
        public bool Converge { get; set; }        
        public int Iteraciones { get; set; }
        public double ErrorRelativo { get; set; }
        public double Raiz { get; set; }
        public void AgregarMsjError(string msj)
        {
            _Error = true;
            _MsjError = msj;
        }
    }
}
