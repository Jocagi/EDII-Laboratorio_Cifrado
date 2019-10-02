using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using Laboratorio_Cifrado.Controllers;

namespace Laboratorio_Cifrado.Utilities
{
    public class ZigZag
    {
        public static void Cifrado(string path, int corrimiento)
        {
            string Data = System.IO.File.ReadAllText(path, Encoding.Default);
           // List<char> Cifrar = Data.ToList<Char>();

            //Obtener todos los caracteres posibles
            Dictionary<string, int> diccionario = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
            {
                diccionario.Add(((char)i).ToString(), i);
            }

            string mensaje = Data;
            //Quitar caracteres en espacios
            mensaje = Regex.Replace(mensaje, @"[^A-Z0-9]", string.Empty);
            var lineas = new List<StringBuilder>();
            int niveles = corrimiento;

            for( int i = 0; i<corrimiento; i++)
            {
                lineas.Add(new StringBuilder());
            }
            int ActualL = 0;
            int Direccion = 1;

            //For para saber donde empezamos

            for(int i = 0; i<mensaje.Length; i++)
            {
                lineas[ActualL].Append(mensaje[i]);

                if (ActualL == 0)
                    Direccion = 1;
                else if (ActualL == corrimiento - 1)
                    Direccion = -1;

                ActualL += Direccion;
            }

            StringBuilder CifradoFinal = new StringBuilder();

            //Saber donde se encuentra cada caracter
            for (int i = 0; i < corrimiento; i++)
                CifradoFinal.Append(lineas[i].ToString());

            string MensCifrado = CifradoFinal.ToString();

        }

        public void Descifrar(string path, int corrimiento)
        {

            string Data = System.IO.File.ReadAllText(path, Encoding.Default);
            string mensaje = Data;
            mensaje = Regex.Replace(mensaje, @"[^A-Z0-9]", string.Empty);
            var lineas = new List<StringBuilder>();
            int niveles = corrimiento;


            for (int i = 0; i < corrimiento; i++)
            {
                lineas.Add(new StringBuilder());
            }

            int[] LineaI = Enumerable.Repeat(0, corrimiento).ToArray();

            int ActualL = 0;
            int Direccion = 1;

            //Donde inicia
            for(int i =0; i<mensaje.Length; i++)
            {
                LineaI[ActualL]++;

                if (ActualL == 0)
                    Direccion = 1;
                else if (ActualL == corrimiento - 1)
                    Direccion = -1;

                ActualL += Direccion;
            }

            int ActualPosicion = 0;

            for(int j = 0; j<corrimiento; j++)
            {
                for(int c = 0; c<LineaI[j]; c++)
                {
                    lineas[j].Append(mensaje[ActualPosicion]);
                    ActualPosicion++;
                }
            }

            StringBuilder descifrado = new StringBuilder();

            ActualL = 0;
            Direccion = 1;

            int[] LP = Enumerable.Repeat(0, corrimiento).ToArray();

            //Une el nuevo orden de las letras
            for(int i = 0; i<mensaje.Length; i++)
            {
                descifrado.Append(lineas[ActualL][LP[ActualL]]);
                LP[ActualL]++;

                if (ActualL == 0)
                    Direccion = 1;
                else if (ActualL == corrimiento - 1)
                    Direccion = -1;

                ActualL += Direccion;
            }

            string DescifradoF = descifrado.ToString();

        }
    }
}