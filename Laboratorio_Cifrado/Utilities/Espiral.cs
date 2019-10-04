using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Laboratorio_Cifrado.Controllers;
using Laboratorio_Cifrado.Models;

namespace Laboratorio_Cifrado.Utilities
{
    public class Espiral
    {
        private const char EOF = '\u0003';

        public static void Cifrar(string path, int password)
        {

            #region Crear_Archivo

            string NuevoArchivo = Path.GetFileName(path);
            string rutaCifrado = CifradoController.directorioUploads + NuevoArchivo;
            Archivo.crearArchivo(rutaCifrado);
            
            #endregion

            #region Variables

            FileInfo file = new FileInfo(path);
            string texto = File.ReadAllText(path);

            int m = password;
            int n = (int) Math.Ceiling((double) (file.Length / m));

            char[,] matriz = new char[n, m];
            string respuesta = "";

            int inicio = 0;
            int limitefila = n;
            int limitecolumna = m;

            int valores = 1; //valores dentro de la matriz

            int i = 0, j = 0;

            #endregion

            //llenado original de la matriz

            #region Llenado

            for (int k = 0; k < n; k++)
            {

                for (int l = 0; l < m; l++)
                {
                    if (!String.IsNullOrEmpty(texto))
                    {

                        matriz[l, k] = Convert.ToChar(texto.Substring(0, 1));
                        texto = texto.Remove(0, 1);
                    }
                    else
                    {
                        matriz[l, k] = EOF;
                    }
                }
            }
            

            #endregion

            //Recorrido Espiral

            #region Recorrido

            while (valores <= matriz.Length)
            {

                for (j = inicio; j < limitecolumna; j++)
                {
                    respuesta += matriz[i,j];
                    valores++;
                }
                for (i = inicio + 1; i < limitefila; i++)
                {
                    respuesta += matriz[i, j - 1];
                    valores++;
                }
                for (j = limitecolumna - 1; j > inicio && i > inicio + 1; j--)
                {
                    respuesta += matriz[i - 1 , j - 1];
                    valores++;
                }
                for (i = limitefila - 1; i > inicio + 1; i--)
                {
                    respuesta += matriz[i - 1 , j];
                    valores++;
                }

                inicio++;
                limitecolumna--;
                limitefila--;
            }
            #endregion

            File.WriteAllText(rutaCifrado, respuesta);
            CifradoController.currentFile = rutaCifrado;
        }


        public static void Descifrar(string path, int password)
        {

        }
    }
}