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
        /*
        private static byte ConvertStringToByte(string value)
        {
            return Convert.ToByte(Convert.ToUInt32(value, 2));
        }

        private static string ConvertByteToString(byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0'); //Produce cadena "00111111";
        }

        private static void WriteToFile(string path, byte[] value)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(value);
            bw.Close();
        }*/


        /*Inge sinceramente este buffer lo hice yo hace como 2 labs pero ahora ya solo Yisus
        Y quizas Jose saben como funciona. :c*/
        /*public static void Cifrar(string path, string path2, string path3)
        {
            #region Crear Archivo
            string NuevoArchivo = Path.GetFileNameWithoutExtension(path) + ".scif";
            string rutaCifrado = CifradoController.directorioArchivos + NuevoArchivo;
            Archivo.crearArchivo(rutaCifrado);
            #endregion

            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        var buffer = reader.ReadBytes(bufferLength);
                        List<byte> CompresionBytes = new List<byte>();

                        foreach (var item in buffer)
                        {
                            //Comprimir
                            int _byte = (int)item;
                            string Encriptado = Convert.ToString((_byte ^ e) % n);

                            CompresionBytes.Add(ConvertStringToByte(Encriptado));
                        }
                        WriteToFile(rutaCifrado, CompresionBytes.ToArray());
                    }
                }
            }

            CifradoController.currentFile = rutaCifrado;
        }
        private void Descifrar(string path, int d, int n)
        {
            #region Crear Archivo
            string NuevoArchivo = Path.GetFileNameWithoutExtension(path) + ".scif";
            string rutaCifrado = CifradoController.directorioArchivos + NuevoArchivo;
            Archivo.crearArchivo(rutaCifrado);
            #endregion

            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        var buffer = reader.ReadBytes(bufferLength);
                        List<byte> CompresionBytes = new List<byte>();

                        foreach (var item in buffer)
                        {
                            //Comprimir
                            int _byte = (int)item;
                            string Descencriptado = Convert.ToString((_byte ^ d) % n);

                            CompresionBytes.Add(ConvertStringToByte(Descencriptado));
                        }
                        WriteToFile(rutaCifrado, CompresionBytes.ToArray());
                    }
                }
            }

            CifradoController.currentFile = rutaCifrado;
        }*/
    }
}
