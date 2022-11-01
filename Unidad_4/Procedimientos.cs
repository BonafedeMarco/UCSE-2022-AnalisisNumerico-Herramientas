using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Calculus;

namespace Unidad_4
{
    public static class Procedimientos
    {
        public static U4Salida Resolucion(U4Entrada entrada)
        {
            Calculo Analizador = new Calculo();
            U4Salida Rta = new U4Salida();
            Rta._Metodo = entrada.Metodo.ToString();

            if (entrada.Metodo == 3 && entrada.CantidadSubintervalos % 2 != 0) //ver si hay que hacerlo dentro del 1/3 multiple
                entrada.Metodo = 4;

            if (Analizador.Sintaxis(entrada.Funcion, 'x'))
            {
                switch (entrada.Metodo)
                {
                    case 0:
                        Rta.Resultado = TrapecioSimple(entrada, Analizador);
                        break;
                    case 1:
                        Rta.Resultado = TrapecioMultiple(entrada, Analizador);
                        break;
                    case 2:
                        Rta.Resultado = SimpsonSimple(entrada, Analizador);
                        break;
                    case 3:
                        Rta.Resultado = SimpsonMultiple(entrada, Analizador);
                        break;
                    case 4:
                        U4Salida resultados = SimpsonOculto(entrada, Analizador);
                        Rta.Resultado = resultados.Resultado;
                        Rta.ResultadoImpar = resultados.ResultadoImpar;
                        
                        break;
                    default:
                        Rta.AgregarMsjError("El int del metodo ingresado no corresponde con ninguno codificado");
                        break;
                }
            }
            else
                Rta.AgregarMsjError("La sintaxis de la función no es correcta");

            return Rta;
        }
        public static double TrapecioSimple(U4Entrada datos, Calculo analizador)
        {
            double funcionA = analizador.EvaluaFx(datos.PuntoA);
            double funcionB = analizador.EvaluaFx(datos.PuntoB);

            return ((funcionA + funcionB) * (datos.PuntoB - datos.PuntoA)) / 2;
        }
        public static double TrapecioMultiple(U4Entrada datos, Calculo analizador)
        {
            double funcionA = analizador.EvaluaFx(datos.PuntoA);
            double funcionB = analizador.EvaluaFx(datos.PuntoB);

            double h = CalcularH(datos, 0);

            double Fxi = 0;

            for (int i = 1; i < datos.CantidadSubintervalos; i++)
            {
                Fxi += analizador.EvaluaFx(datos.PuntoA + i * h); //VER!
            }

            return (h / 2) * (funcionA + 2 * Fxi + funcionB);
        }
        public static double SimpsonSimple(U4Entrada datos, Calculo analizador)
        {
            double funcionA = analizador.EvaluaFx(datos.PuntoA);
            double funcionB = analizador.EvaluaFx(datos.PuntoB);

            double h = CalcularH(datos, 1);

            double funcionAH = analizador.EvaluaFx(datos.PuntoA + h);

            return (h / 3) * (funcionA + 4 * funcionAH + funcionB);
        }
        public static double SimpsonMultiple(U4Entrada datos, Calculo analizador)
        {
            double funcionA = analizador.EvaluaFx(datos.PuntoA);
            double funcionB = analizador.EvaluaFx(datos.PuntoB);

            double h = CalcularH(datos, 0);

            double ParesFxi = 0;
            double ImparesFxi = 0;
            for (int i = 1; i < datos.CantidadSubintervalos; i++)
            {
                if (i % 2 == 0)
                    ParesFxi += analizador.EvaluaFx(datos.PuntoA + i * h); //VER!
                else
                    ImparesFxi += analizador.EvaluaFx(datos.PuntoA + i * h); //VER TMB
            }

            return (h / 3) * (funcionA + 4 * ImparesFxi + 2 * ParesFxi + funcionB);
        }
        public static U4Salida SimpsonOculto(U4Entrada datos, Calculo analizador)
        {
            
            double funcionA = analizador.EvaluaFx(datos.PuntoA);
            double funcionB = analizador.EvaluaFx(datos.PuntoB);

            double h = CalcularH(datos, 2);

            double funcionAH = analizador.EvaluaFx(datos.PuntoA + h);
            double funcionA2H = analizador.EvaluaFx(datos.PuntoA + 2 * h);

            double SimpsonOculto = (3 * h / 8) * (funcionA + 3 * funcionAH + 3 * funcionA2H + funcionB);
            datos.CantidadSubintervalos -= 3; //-------------------------------------------------------------VER

            return new U4Salida { Resultado = SimpsonMultiple(datos, analizador) + SimpsonOculto, ResultadoImpar = SimpsonOculto };
        }
        public static double CalcularH(U4Entrada datos, int metodo)
        {
            switch (metodo)
            {
                case 0: //Metodos Multiples
                    return (datos.PuntoB - datos.PuntoA) / datos.CantidadSubintervalos;
                case 1: //Simpson 1/3 Simple
                    return (datos.PuntoB - datos.PuntoA) / 2;
                case 2: // Simpson 3/8
                    return (datos.PuntoB - datos.PuntoA) / 3;
            }
            return 0;
        }
    }
}
