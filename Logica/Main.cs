using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Logica
{
    public static class Main
    {
        #region Unidad 1
        public static U1Salida Bisección(U1Entrada datos)
        {
            return Unidad_1.Procedimientos.MetodoCerrado(datos, true);
        }

        public static U1Salida ReglaFalsa(U1Entrada datos)
        {
            return Unidad_1.Procedimientos.MetodoCerrado(datos, false);
        }

        public static U1Salida Tangente(U1Entrada datos)
        {
            return Unidad_1.Procedimientos.MetodoAbierto(datos, true);
        }

        public static U1Salida Secante(U1Entrada datos)
        {
            return Unidad_1.Procedimientos.MetodoAbierto(datos, false);
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

        public static U3Salida RegresionLineal(U3Entrada entrada)
        {
            return Unidad_3.Procedimientos.Resolucion(entrada, 0);
        }

        public static U3Salida RegresionPolinomial(U3Entrada entrada)
        {
            return Unidad_3.Procedimientos.Resolucion(entrada, 1);
        }

        #endregion

        #region Unidad 4

        public static U4Salida Calcular(U4Entrada entrada)
        {
            return Unidad_4.Procedimientos.Resolucion(entrada);
        }

        #endregion
    }
}
