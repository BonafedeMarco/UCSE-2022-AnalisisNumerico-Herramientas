using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculus;


namespace Logica
{
    public class Unidad_1
    {
        //4 metodos (Biseccion - Regla Falsa - Tangente - Secante)
        //Una region para cada metodo (o dos para abiertos y cerrados), una seccion para calculos globale
        //con generincs se puede pasar el metodo de la (para pasar el same (privado)dfd y qué metodo usar () para que llame a la funcion y utilizar el calculo que corresponda)
        public Salida MetodoCerrado(Entrada datos)
        {
            Salida Resultado = new Salida();
            int c = 0;
            bool flag = false;
            double Xr = 999; //Corregir--------------------------------------
            double Xant = 0;
            double ErrorRelat = 0;
            Calculo Analizador = new Calculo();

            while (c < datos.MaxIter || flag == false)
            {
                if (Analizador.Sintaxis(datos.Funcion, 'x')) //Se fija si la sintaxis de la F está bien en torno al char x.
                {
                    if (Analizador.EvaluaFx(datos.Xi) * Analizador.EvaluaFx(datos.Xd) == 0)
                    {
                        Resultado.Converge = true;
                        if (Analizador.EvaluaFx(datos.Xi) == 0)
                        {
                            Resultado.Raiz = datos.Xi;
                            break;
                        }
                        Resultado.Raiz = datos.Xd;
                        break;
                    }
                    else if(Analizador.EvaluaFx(datos.Xi) * Analizador.EvaluaFx(datos.Xd) < 0)
                    {
                        Resultado.AgregarMsjError("No capo");
                        break;
                    }
                    else if (Analizador.EvaluaFx(datos.Xi) * Analizador.EvaluaFx(datos.Xd) > 0)
                    {
                        c++;
                        Xr = FormulaBiseccion(datos);
                        ErrorRelat = Math.Abs((Xr + Xant) / Xr);
                        if (Math.Abs(Analizador.EvaluaFx(Xr))<datos.Tolerancia||ErrorRelat<datos.Tolerancia)
                        {
                            break;
                        }
                    }
                }
            }
            Resultado.Iteraciones = c;
            Resultado.ErrorRelativo = ErrorRelat;
            if (c >= datos.MaxIter)//VERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
                Resultado.Converge = false;
            else
                Resultado.Converge = true;
            Resultado.Raiz = Xr;
            

            return Resultado;
        }
        public Salida MetodoAbierto(Entrada datos)
        {

            return new Salida();
        }
        public double FormulaBiseccion(Entrada datos)
        {
            return (datos.Xi + datos.Xd) / 2;
        }
        public double FormulaReglaFalsa(Entrada datos, Calculo Analizador)
        {
            double fxi = Analizador.EvaluaFx(datos.Xi);
            double fxd = Analizador.EvaluaFx(datos.Xd);
            return ((fxi * datos.Xd) - (fxd * datos.Xi)) / (fxi - fxd);
        }

    }
}
