using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Laboratorio_Cifrado.Controllers;
using Laboratorio_Cifrado.Models;
using Microsoft.Ajax.Utilities;
using System.Text;

namespace Laboratorio_Cifrado.Utilities
{
    public class RSA
    {
        private void Algoritmo(int P, int Q)
        {
            #region Primos
            List<int> Primos = new List<int>();
            Primos.Add(2);
            Primos.Add(3);
            Primos.Add(5);
            Primos.Add(7);
            Primos.Add(11);
            Primos.Add(13);
            Primos.Add(17);
            Primos.Add(19);
            Primos.Add(23);
            Primos.Add(29);
            Primos.Add(31);
            Primos.Add(37);
            Primos.Add(41);
            Primos.Add(43);
            Primos.Add(53);
            Primos.Add(59);
            Primos.Add(61);
            Primos.Add(67);
            Primos.Add(71);
            Primos.Add(73);
            Primos.Add(79);
            Primos.Add(83);
            Primos.Add(89);
            Primos.Add(97);
            #endregion
            int p = P;
            int q = Q;
            int n = p * q;
            int phi = ((p - 1) * (q - 1));
            List<int> divisiblesN = new List<int>();
            List<int> divisiblesPhi = new List<int>();
            for(int i=1; 1<= n; i++)
            {
                if (n % i == 0)
                    divisiblesN.Add(i);
            }
            for (int i=1; 1<= phi; i++)
            {
                if (phi % i == 0)
                    divisiblesPhi.Add(i);
            }
            List<int> Coprimos = new List<int>();
            Coprimos = divisiblesN.Union(divisiblesPhi).ToList();
            List<int> Diferentes = Primos.Except(Coprimos).ToList();
            int e = Diferentes.Last();
            int a;
            do
            {
                phi = phi / e;
                int resultado = phi * e;
                a = phi - resultado;
            }
           
        }
    }
}