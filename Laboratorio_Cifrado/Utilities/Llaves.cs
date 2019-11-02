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
            int n = P * Q;
            int phi = ((P - 1) * (Q - 1));
            int phi2 = ((P - 1) * (Q - 1));
            int phi3 = ((P - 1) * (Q - 1));
            int phi4 = ((P - 1) * (Q - 1));
            int a;
            int contador = 0;
            int d = 1;
            int e = NumerosPrimos.obtenerNumeroE(n, phi);
            int e2 = e;
            #endregion

            //Aqui se obtiene la primer columna
            List<int> Cocientes = new List<int>();

            do
            {
                int cociente = phi4 / e;
                int resultado = cociente * e;
                a = phi4 - resultado;
                phi4 = e;
                e = a; //La "a" funciona casi que como un contador
                Cocientes.Add(cociente);
            }
            while (a > 1);

            //Aqui ya se obtiene d
            int[] CocienteArreglo = Cocientes.ToArray();
            do
            {
                for (int i = 0; i < CocienteArreglo.Length; i++) //Realiza el for siempre y cuando el contador no supere el tamaño del arreglo
                {
                    int Producto = CocienteArreglo[i] * d; //Aquí calcula el producto que se usara para la resta
                    int c = phi2 - Producto; //Obtenesmos c la cual pasa a ser la d al final
                    if (c < 0) //Este if sirve para evitar los números negativos
                    {
                        c = phi3 % c; //Se aplica mod de phi siempre
                    }
                    phi2 = d; //Aquí ya va cambiando los valores para seguir con el ciclo
                    d = c; //Cuando el contador supere al arreglo, saldrá del ciclo y se obtendrá la d
                     contador++;
                }
            }
            while (contador < CocienteArreglo.Length);
            string ee = e2.ToString();
            string dd = d.ToString();
            string nn = n.ToString();           
            string[] LlavePublica = { ee, nn };
            string[] LlavePrivada = { dd, nn };


            using (StreamWriter LlavePublic = new StreamWriter(CifradoController.publicKey))
            {
                LlavePublic.WriteLine(LlavePublica[0] + "," + LlavePublica[1]);
            }
            using (StreamWriter LlavePrivate = new StreamWriter(CifradoController.privateKey))
            {
                LlavePrivate.WriteLine(LlavePrivada[0] + "," + LlavePrivada[1]);
            }
        }
    }
}