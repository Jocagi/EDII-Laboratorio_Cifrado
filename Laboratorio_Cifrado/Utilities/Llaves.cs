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
    public class Llaves
    { 

        public static void GenerarLlaves(int P, int Q)
        {

            #region variables
            //Declaracion de variables
            int p = P;
            int q = Q;
            int n = P * Q;
            int phi = ((p - 1) * (q - 1));
            int a;
            int contador = 0;
            int d = 1;
            int e = NumerosPrimos.obtenerNumeroE(n, phi);
            #endregion

            //Aqui se obtiene la primer columna
            List<int> Cocientes = new List<int>();

            do
            {
                int AUXe = e;
                int AuxPhi = phi;
                int cociente = AuxPhi / AUXe;
                int resultado = cociente * AUXe;
                a = AuxPhi - resultado;
                AuxPhi = AUXe;
                AUXe = a; //La "a" funciona casi que como un contador
                Cocientes.Add(cociente);
            }
            while (a > 1);

            //Aqui ya se obtiene d
            int[] CocienteArreglo = Cocientes.ToArray();
            do
            {
                for (int i = 0; i < CocienteArreglo.Length; i++) //Realiza el for siempre y cuando el contador no supere el tamaño del arreglo
                {
                    int AuxPhi = phi;
                    int AuxPhi2 = phi;
                    int Producto = CocienteArreglo[i] * d; //Aquí calcula el producto que se usara para la resta
                    int c = AuxPhi - Producto; //Obtenesmos c la cual pasa a ser la d al final
                    if (c < 0) //Este if sirve para evitar los números negativos
                    {
                        c = AuxPhi2 % c; //Se aplica mod de phi siempre
                    }
                    AuxPhi = d; //Aquí ya va cambiando los valores para seguir con el ciclo
                    d = c; //Cuando el contador supere al arreglo, saldrá del ciclo y se obtendrá la d
                     contador++;
                }
            }
            while (contador < CocienteArreglo.Length);
            string ee = e.ToString();
            string dd = d.ToString();
            string nn = n.ToString();           
            string[] LlavePublica = { ee, nn };
            string[] LlavePrivada = { dd, nn };


            using (StreamWriter LlavePublic = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/Llaves/LlavePublica.txt")))
            {
                LlavePublic.WriteLine(LlavePublica[0] + "," + LlavePublica[1]);
            }
            using (StreamWriter LlavePrivate = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/Llaves/LlavePrivada.txt")))
            {
                LlavePrivate.WriteLine(LlavePrivada[0] + "," + LlavePrivada[1]);
            }
        }
    }
}