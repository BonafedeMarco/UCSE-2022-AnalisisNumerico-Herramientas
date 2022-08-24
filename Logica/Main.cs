using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculus;

namespace Logica
{
    public class Main
    {
        #region Unidad 1
        public Salida Bisección(Entrada datos)
        {
            return Unidad_1.MetodoCerrado(datos, true);
        }

        public Salida ReglaFalsa(Entrada datos)
        {
            return Unidad_1.MetodoCerrado(datos, false);
        }

        public Salida Tangente(Entrada datos)
        {
            return Unidad_1.MetodoAbierto(datos, true);
        }
        
        public Salida Secante(Entrada datos)
        {
            return Unidad_1.MetodoAbierto(datos, false);
        }

        #endregion

        #region Unidad 2

        #endregion

        #region Unidad 3

        #endregion

        #region Unidad 4

        #endregion
    }
}
