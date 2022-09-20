using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
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

        public static U2Salida GaussJordan(U2Entrada entrada)
        {
            return Unidad_2.Procedimientos.MetodoGaussJordan(entrada);
        }

        public static U2Salida GaussSeidel(U2Entrada entrada)
        {
            return Unidad_2.Procedimientos.MetodoGaussSeidel(entrada);
        }

        #endregion

        #region Unidad 3

        #endregion

        #region Unidad 4

        #endregion
    }
}
