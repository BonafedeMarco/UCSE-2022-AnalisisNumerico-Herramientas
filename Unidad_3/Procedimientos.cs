using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Unidad_3
{
    public static class Procedimientos
    {
        public class Datos
        {
            public int CantPuntos { get; set; }
            public double SumX { get; set; }
            public double SumY { get; set; }
            public double SumXY { get; set; }
            public double SumX2 { get; set; }
        }
        public static U3Salida Resolucion(U3Entrada entrada)
        {
            Datos datos = new Datos()
            {
                CantPuntos= entrada.PuntosCargados.Count, 
                SumX= entrada.PuntosCargados[0].Sum(), 
                SumY= entrada.PuntosCargados[1].Sum(), 
                SumXY= entrada.PuntosCargados[0].Sum() * entrada.PuntosCargados[1].Sum(), 
                SumX2= entrada.PuntosCargados[0].Sum() * entrada.PuntosCargados[0].Sum()
            };

            if (entrada.MetodoUtilizado==0)       
                return RegresionLineal(datos, entrada);            

            return new U3Salida();
        }
        public static U3Salida RegresionLineal(Datos datos, U3Entrada entrada)
        {
            double a1 = (datos.CantPuntos*datos.SumXY-datos.SumX*datos.SumY)/(datos.CantPuntos*datos.SumX2-Math.Pow(datos.SumX,2)); //ver si calcula bien
            double a0 = (datos.SumY/datos.CantPuntos)-a1*(datos.SumX/datos.CantPuntos);

            double St = 0;
            double Sr = 0;

            foreach (double[] item in entrada.PuntosCargados)
            {
                St += Math.Pow(datos.SumY / datos.CantPuntos - item[1],2);
                Sr += Math.Pow(a1*item[0]+a0-item[0],2);
            }

            double coefCorrelacion = Math.Sqrt((St - Sr) / St) * 100;

            return new U3Salida { Funcion = $"y = {a1}x{(a0 > 0 ? "+" : "")}{a0}",
                FuncionGraficador = $"{a1}*x{(a0 > 0 ? "+" : "")}{a0}",
                PorcentajeEfectividad=coefCorrelacion, 
                EfectividadAjuste=coefCorrelacion>entrada.Tolerancia};
        }
    }
}
