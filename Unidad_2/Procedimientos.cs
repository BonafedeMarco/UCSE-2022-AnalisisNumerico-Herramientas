using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad_2
{
    public static class Procedimientos
    {
        ///ME ANOTO LO QUE ESCRIBIÓ LE PROFE ^-^/
        /* GAUSS JORDAN:
         * 1- el primer bucle (general) consta de recorrer cada fila en donde esta el 
         * coeficiente de la diagonal principal => igual a dimensión. 
         * Acá obtenemos el coeficiente (ej: matriz [0;0], matriz [1;1], matriz [2;2])
         * 2- Luego de hacer un bucle para recorrer las columnas dividiendo el valor 
         * de la posicion porr el coeficiente obtenido => dimensión + 1
         * 3- Salgo del bucle deñ paso 2 y vuelvo a hacer otro que recorra nuevamente 
         * las filas, pero salteo la que es igual a la del bucle principal =>
         * if(rowDiago != row)
         * 4- Dentro del if resuelvo:
         *      1. Obtengo el coeficiente que quiero hacer cero => Ej.: Matriz[1;0]
         *      2. Hacer un bucle para recorrer las columas y aplicamos la formula para 
         *      anular el valor => ej: Matriz[1;0] - (coeficiente * Matriz[0;0] -> Valor de la fila anterior normalizada)
         * 5- Fuera del bucle general intanciamos un vector resultado y hacemos un 
         * bucle para obtener los valores resultantes en los 
         * terminos independientes => (b03, b13, b23) -> ya procesados
         *      
         *      El tema del pivoteo se puede aclarar en la teoría
         */
         //llega un integer con la dimensión de la matriz, una matriz de dos dimensiones tipo double, un bool para el metodo
         public static Salida MetodoGaussJordan(Entrada datos)
        {
            Salida salida = new Salida();
            
            for (int x = 0; x < datos.Dimension; x++)
            {
                for (int y = 0; y < datos.Dimension; y++)
                {
                    if(x==y)
                    {

                    }
                }
            }
            return salida;
        }
        public static Salida MetodoGaussSeidel(Entrada datos)
        {
            Salida salida = new Salida();

            return salida;
        }
    }
}
