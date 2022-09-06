using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculus;

using Unidad_2;

namespace Logica
{
    public static class Main
    {
        #region Unidad 1
        public static Salida Bisección(Entrada datos)
        {
            return Unidad_1.MetodoCerrado(datos, true);
        }

        public static Salida ReglaFalsa(Entrada datos)
        {
            return Unidad_1.MetodoCerrado(datos, false);
        }

        public static Salida Tangente(Entrada datos)
        {
            return Unidad_1.MetodoAbierto(datos, true);
        }
        
        public static Salida Secante(Entrada datos)
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
