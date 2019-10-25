using System;
using System.Collections.Generic;
using System.Linq;


namespace Laboratorio_Cifrado.Utilities
{
    public class NumerosPrimos
    {
        public static bool esNumeroPrimo(int n)
        {
            if (n > 1)
            {
                for (int i = 2; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        return false;
                    }   
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static int obtenerNumeroE(int N, int Phi)
        {
            List<int> factoresN = Factorizar(N);
            List<int> factoresPhi = Factorizar(Phi);

            var comprimos = factoresN.Except(factoresPhi); 

            int e = comprimos.Last(); //Numero mas grande

            return e;
        }

        private static List<int> Factorizar(int n)
        {
            List<int> factores = new List<int>();

            //default
            factores.Add(1);
            factores.Add(n);

            if (n % 2 == 0) //si es un numero par
            {
              factores.Add(2);   
            }

            for (int i = 3; i < n/2; i+=2)
            {
                if (n % i == 0)
                {
                    factores.Add(2);
                }
            }

            return factores;
        }
    }
}