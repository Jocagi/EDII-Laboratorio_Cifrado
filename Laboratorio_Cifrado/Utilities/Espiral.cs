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
        private const int bufferLength = 1024;

        public static void Cifrar(string path, int password)
        {
            #region Crear_Archivo

            string NuevoArchivo = Path.GetFileName(path);
            string rutaCifrado = CifradoController.directorioArchivos + NuevoArchivo;
            Archivo.crearArchivo(rutaCifrado);

            #endregion

            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    //Buffer para cifrar
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        var buffer = reader.ReadBytes(count: bufferLength);

                        #region Variables

                        int bufferPosition = 0;

                        int fileLength = buffer.Length;

                        int m = password;
                        int n = (int)fileLength / m;
                        if (fileLength % m != 0) { n++; }//Redondear al siguinete entero

                        char[,] matriz = new char[n, m];
                        List<byte> respuesta = new List<byte>();
                        
                        int inicio = 0;
                        int limitefila = n;
                        int limitecolumna = m;

                        int valores = 1; //valores dentro de la matriz

                        int i = 0, j = 0;

                        FileStream fs = new FileStream(rutaCifrado, FileMode.Append);
                        BinaryWriter bw = new BinaryWriter(fs);

                        #endregion

                        #region Llenado
                        //llenado original de la matriz
                        for (int k = 0; k < m; k++)
                        {
                            for (int l = 0; l < n; l++)
                            {
                                if (bufferPosition < buffer.Length)
                                {
                                    if (buffer[bufferPosition] != default(byte))
                                    {
                                        matriz[l, k] = (char)buffer[bufferPosition];
                                        bufferPosition++;
                                    }
                                    else
                                    {
                                        matriz[l, k] = EOF;
                                    }
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
                                respuesta.Add((byte) matriz[i, j]); 
                                valores++;
                            }
                            for (i = inicio + 1; i < limitefila; i++)
                            {
                                respuesta.Add((byte) matriz[i, j - 1]);
                                valores++;
                            }
                            for (j = limitecolumna - 1; j > inicio && i > inicio + 1; j--)
                            {
                                respuesta.Add((byte) matriz[i - 1, j - 1]);
                                valores++;
                            }
                            for (i = limitefila - 1; i > inicio + 1; i--)
                            {
                                respuesta.Add((byte) matriz[i - 1, j]);
                                valores++;
                            }

                            inicio++;
                            limitecolumna--;
                            limitefila--;
                        }
                        #endregion

                        bw.Write(respuesta.ToArray());
                        bw.Close();
                    }
                }
            }
            
            CifradoController.currentFile = rutaCifrado;
        }

        public static void Descifrar(string path, int password)
        {
            
            #region Crear_Archivo

            string NuevoArchivo = Path.GetFileName(path);
            string rutaDescifrado = CifradoController.directorioArchivos + NuevoArchivo;
            Archivo.crearArchivo(rutaDescifrado);

            #endregion

            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    //Buffer para descifrar
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        
                        #region Variables

                        int bufferPosition = 0;

                        int fileLength = bufferLength;

                        int m = password;
                        int n = (int)fileLength / m;
                        if (fileLength % m != 0) { n++; }//Redondear al siguinete entero

                        //Corregir error
                        while (fileLength < (n*m))
                        {
                            fileLength++;
                        }
                        
                        var buffer = reader.ReadBytes(count: fileLength);

                        char[,] matriz = new char[n, m];
                        List<byte> respuesta = new List<byte>();

                        int inicio = 0;
                        int limitefila = n;
                        int limitecolumna = m;

                        int valores = 1; //valores dentro de la matriz

                        int i = 0, j = 0;

                        FileStream fs = new FileStream(rutaDescifrado, FileMode.Append);
                        BinaryWriter bw = new BinaryWriter(fs);

                        #endregion

                        //Escritura Espiral
                        #region Espiral

                        while (bufferPosition < buffer.Length)
                        {
                            for (j = inicio; j < limitecolumna; j++)
                            {
                                if (bufferPosition < buffer.Length)
                                {
                                    matriz[i, j] = (char) buffer[bufferPosition];
                                    bufferPosition++;
                                    valores++;
                                }
                            }
                            for (i = inicio + 1; i < limitefila; i++)
                            {
                                if (bufferPosition < buffer.Length)
                                {
                                    matriz[i, j - 1] = (char) buffer[bufferPosition];
                                    bufferPosition++;
                                    valores++;
                                }
                            }
                            for (j = limitecolumna - 1; j > inicio && i > inicio + 1; j--)
                            {
                                if (bufferPosition < buffer.Length)
                                {
                                    matriz[i - 1, j - 1] = (char) buffer[bufferPosition];
                                    bufferPosition++;
                                    valores++;
                                }
                            }
                            for (i = limitefila - 1; i > inicio + 1; i--)
                            {
                                if (bufferPosition < buffer.Length)
                                {
                                    matriz[i - 1, j] = (char) buffer[bufferPosition];
                                    bufferPosition++;
                                    valores++;
                                }
                            }

                            inicio++;
                            limitecolumna--;
                            limitefila--;
                        }
                        #endregion

                        //Lectura final
                        #region Matriz
                        for (int k = 0; k < m; k++)
                        {
                            for (int l = 0; l < n; l++)
                            {
                                if (matriz[l, k] != EOF && matriz[l, k] != default(char))
                                {
                                    respuesta.Add((byte) matriz[l, k]);
                                }
                            }
                        }
                        #endregion
                        
                        bw.Write(respuesta.ToArray());
                        bw.Close();
                    }
                }
            }

            CifradoController.currentFile = rutaDescifrado;
        }
    }
}