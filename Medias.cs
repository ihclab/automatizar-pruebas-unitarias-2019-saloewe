using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatizarPruebasUnitarias {
    
    class Medias {

        /**
         * Calcula y regresa la media artimética
         */
        public static double mediaAritmetica(params object[] vals) { 
            double suma = 0;
            vals = quitNulls(vals);
            if(vals.Length == 0) 
                throw new Exception("All the values cannot be null.");

            for (int i = 0; i < vals.Length; i++)
            {   
                suma += Convert.ToDouble(vals[i]);
            }
            double result = suma / vals.Length;
            return Math.Truncate(result * 10000) / 10000;
        }

        /**
         * Calcula y regresa la raíz enésima = x^(1/n)
         */
        private static double raizEnesima(double x, int n) {
            return Math.Pow(x, 1/n);
        }

        /**
         *  Usa raizEnesima para calcular y regresar la media geométrica
         */
        public double mediaGeometrica(params object[] vals) { 
            double radicand = 0;

            vals = quitNulls(vals);
            if(vals.Length == 0) 
                throw new Exception("All the values cannot be null.");

            for (int i = 0; i < vals.Length; i++)
            {
                if((double) vals[i] <= 0) 
                {
                    throw new Exception("Cannot obtain geometric mean of non-positive values.");
                }
                radicand *= (double) vals[i];
            }
            double result = raizEnesima(radicand, vals.Length);
            return Math.Truncate(result * 10000) / 10000;
        }

        /**
         * Este método no está implementado.
         */
        public static double mediaArmonica(params object[] vals) {
            double suma = 0;

            vals = quitNulls(vals);
            if(vals.Length == 0) 
                throw new Exception("All the values cannot be null.");
            vals = vals.Where(val => (double) val != 0).ToArray();
            if(vals.Length == 0)
                throw new Exception("All the values cannot be zero.");
            for (int i = 0; i < vals.Length; i++)
            {
                suma += 1 / (double) vals[i];
            }
            double result = vals.Length / suma;
            return Math.Truncate(result * 10000) / 10000;
        }
        
        private static object[] quitNulls(object[] values) 
        {
            return values.Where(val => val != null).ToArray();
        }
    }
}
