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
                    if (x == y)
                    {
                        for (int col = 0; col < datos.Dimension + 1; col++) //Divido todos los coeficientes de la ecuación por el coeficiente de la DP
                        {
                            datos.Matriz[x, col] = datos.Matriz[x, col] / datos.Matriz[x, y];
                        }
                        for (int row = 0; row < datos.Dimension; row++)//Fuera de ese renglón tengo que hacer 0 todos los coeficientes de arriba y abajo
                        {                                                //Tmb modificando los demás coeficientes de renglones ajenos.
                            if (row != x)
                            {
                                for (int col = 0; col < datos.Dimension + 1; col++)
                                {
                                    datos.Matriz[row, col] = datos.Matriz[row, col] - (datos.Matriz[row, col] * datos.Matriz[x, y]);
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < datos.Dimension; i++) //En algunos pongo dimensión+1 y en otros no porque solamente hay una columna extra (los valores b resultados)
            {
                salida.Resultado[i] = datos.Matriz[i, datos.Dimension + 1];
            }
            return salida;
        }
        public static Salida MetodoGaussSeidel(Entrada datos)//Transcribo lo que mandó el profe (as good as I can)
        {
            double tolerancia = 0.0001;
            bool menorTolerancia = false;
            int contador = 0;
            double[] vectorAnterior = new double[datos.Dimension];
            double resultado = 0;
            Salida salida = new Salida();
            salida.Resultado.Initialize(); //Rellena el vector con 0s
            while (contador<=100||!menorTolerancia)
            {
                contador++;
                if (contador>1)                
                    salida.Resultado.CopyTo(vectorAnterior, 0);
                for (int row = 0; row < datos.Dimension; row++)
                {
                    for (int col = 0; col < datos.Dimension; col++)
                    {
                        if (row!=col)                        
                            resultado = datos.Matriz[row, datos.Dimension] - (datos.Matriz[row,col]*salida.Resultado[col]);                        
                    }
                    //double coefIncognit = resultado / coefIncognit;
                    salida.Resultado[row] = resultado / (salida.Resultado[row] * resultado);
                }
                int contarMismoResultado = 0;
                for (int i = 0; i < datos.Dimension; i++)
                {
                    if (Math.Abs(salida.Resultado[i]-vectorAnterior[i])<tolerancia)
                    {
                        contarMismoResultado++;
                    }
                }
                menorTolerancia = contarMismoResultado == datos.Dimension;
            }
            if (contador > 100)
                salida.AgregarMsjError("Se superó el número máximo de iteraciones antes de llegar a un resultado tolerable");
            
            return salida;
        }
    }
}
