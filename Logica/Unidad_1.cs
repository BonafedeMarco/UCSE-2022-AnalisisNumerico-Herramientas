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
        public Salida MetodoCerrado(Entrada datos, bool bit) //true = Biseccion
        {
            Salida Resultado = new Salida();
            Calculo Analizador = new Calculo();

            if (Analizador.Sintaxis(datos.Funcion, 'x')) ///Se fija si la sintaxis de la F está bien en torno al char x.
            {
                int c = 0;
                double Xr = 0;
                double Xant = 0;
                double ErrorRelat = 0;
                while (c < datos.MaxIter)
                {
                    if (Analizador.EvaluaFx(datos.Xi) * Analizador.EvaluaFx(datos.Xd) == 0)
                    {
                        Resultado.Converge = true;
                        Resultado.Iteraciones = c;
                        Resultado.ErrorRelativo = ErrorRelat;
                        if (Analizador.EvaluaFx(datos.Xi) == 0)
                        {
                            Resultado.Raiz = datos.Xi;
                            return Resultado;
                        }
                        Resultado.Raiz = datos.Xd;
                        return Resultado;
                    }
                    else if (Analizador.EvaluaFx(datos.Xi) * Analizador.EvaluaFx(datos.Xd) < 0)
                    {
                        Resultado.AgregarMsjError("No capo");
                        break;
                    }
                    else if (Analizador.EvaluaFx(datos.Xi) * Analizador.EvaluaFx(datos.Xd) > 0)
                    {
                        c++;

                        if (bit)
                            Xr = FormulaBiseccion(datos);
                        else
                            Xr = FormulaReglaFalsa(datos, Analizador);

                        ErrorRelat = Math.Abs((Xr + Xant) / Xr);
                        if (Math.Abs(Analizador.EvaluaFx(Xr)) < datos.Tolerancia || ErrorRelat < datos.Tolerancia)
                            break;                        
                    }
                    if (Analizador.EvaluaFx(datos.Xi) * Analizador.EvaluaFx(Xr) < 0)
                        datos.Xd = Xr;
                    else
                        datos.Xi = Xr;
                    Xant = Xr;                    
                }
                if (!Resultado._Error)
                {
                    Resultado.Iteraciones = c;
                    Resultado.ErrorRelativo = ErrorRelat;
                    if (c >= datos.MaxIter)//VERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
                        Resultado.Converge = false;
                    else
                        Resultado.Converge = true;
                    Resultado.Raiz = Xr;
                    return Resultado;
                }
                return Resultado;
            }
            Resultado.AgregarMsjError("Funcion no válida");
            return Resultado;
        }
        public Salida MetodoAbierto(Entrada datos, bool bit) //true = Tangente
        {
            Calculo Analizador = new Calculo();
            Salida Resultado = new Salida();
            if (Analizador.Sintaxis(datos.Funcion, 'x'))
            {
                int c = 0;
                double Xant = 0;
                double Xr = 0;
                double Errorx = 0;
                while (Math.Abs(Analizador.EvaluaFx(datos.Xi)) < datos.Tolerancia || c < datos.MaxIter) //Xi hace de Xini por ahora
                {
                    if (Analizador.Dx(datos.Xi) != 0)
                    {
                        c++;

                        if (bit)
                            Xr = FormulaTangente(datos, Analizador);
                        else
                            Xr = FormulaSecante(datos, Analizador);

                        Errorx = Math.Abs((Xr - Xant) / Xr);//Xr puede ser 0 en este punto? VERRRRRRRR
                        if (Math.Abs(Analizador.EvaluaFx(Xr)) < datos.Tolerancia || Errorx < datos.Tolerancia)
                            break;
                    }
                    else
                    {
                        Resultado.AgregarMsjError("Division por 0 no che");
                        break;
                    }
                    datos.Xi = Xr;
                    Xant = Xr;
                }
                if (!Resultado._Error)
                {
                    Resultado.Iteraciones = c;
                    Resultado.ErrorRelativo = Errorx;
                    if (c == 0)
                    {
                        if (bit)
                        {
                            Resultado.Raiz = datos.Xi;
                            Resultado.Converge = true;
                        }
                        return Resultado;
                    }

                    Resultado.Raiz = Xr;

                    if (c <= datos.Tolerancia) //VERRRRRRRR
                        Resultado.Converge = true;
                    else
                        Resultado.Converge = false;

                    return Resultado;
                }
                Resultado.Iteraciones = c;
                return Resultado;
            }
            Resultado.AgregarMsjError("Funcion no válida");
            return Resultado;
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
        public double FormulaTangente(Entrada datos, Calculo Analizador)
        {
            return datos.Xi - (Analizador.EvaluaFx(datos.Xi) / Analizador.Dx(datos.Xi));
        }
        public double FormulaSecante(Entrada datos, Calculo Analizador)
        {
            return ((Analizador.EvaluaFx(datos.Xd) * datos.Xi) - (Analizador.EvaluaFx(datos.Xi) * datos.Xd)) / (Analizador.EvaluaFx(datos.Xd) - Analizador.EvaluaFx(datos.Xi));
        }
    }
}
