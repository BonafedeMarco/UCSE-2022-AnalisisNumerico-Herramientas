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

            if (Analizador.Sintaxis(entrada.Funcion, 'x'))
            {
                switch (entrada.Metodo)
                {
                    case 0:
                        //TrapecioSimple
                        break;
                    case 1:
                        //TrapecioMultiple
                        break;
                    case 2:
                        //Simpson(1/3)Simple
                        break;
                    case 3:
                        //Simpson(1/3)Multiple
                        break;
                    case 4:
                        //Simpson(3/8)Oculto
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

            return (funcionA + funcionB * (datos.PuntoB - datos.PuntoA)) / 2;
        }
        public static double TrapecioMultiple(U4Entrada datos, Calculo analizador)
        {
            double funcionA = analizador.EvaluaFx(datos.PuntoA);
            double funcionB = analizador.EvaluaFx(datos.PuntoB);

            double h = CalcularH(datos, 0);
            
            double Fxi = 0;
            
            for (int i = 1; i < datos.CantidadSubintervalos; i++) //VER SI HAY QUE PONER CANTSUBINTERVALOS-1
            {
                Fxi += analizador.EvaluaFx(datos.PuntoA + i * h);
            }

            return (funcionA + funcionB * (datos.PuntoA - datos.PuntoB)) / 2;
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
