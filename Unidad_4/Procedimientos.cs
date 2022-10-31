using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Unidad_4
{
    public static class Procedimientos
    {
        public static U4Salida Resolucion(U4Entrada entrada)
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
                    break;
            }
            return new U4Salida();
        }
        public static double TrapecioSimple (U4Salida datos)
        {

            return 0;
        }
    }
}
