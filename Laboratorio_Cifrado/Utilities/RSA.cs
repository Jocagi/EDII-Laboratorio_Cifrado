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
        private const int bufferLength = 1024;
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
        }

        //Utilities.RSA.Cifrar(pathArchivo, power, N);
        /*Inge sinceramente este buffer lo hice yo hace como 2 labs pero ahora ya solo Yisus
        Y quizas Jose saben como funciona. :c*/
        public static void Cifrar(string path, int power, int N)
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
                            double Potencia = Math.Pow(_byte,power);
                            int res = Convert.ToInt32(Potencia);
                            int Mod = (res % N);
                            string Encriptado = Mod.ToString();
                            CompresionBytes.Add(ConvertStringToByte(Encriptado));
                        }
                        WriteToFile(rutaCifrado, CompresionBytes.ToArray());
                    }
                }
            }

            CifradoController.currentFile = rutaCifrado;
        }
      
    }
}
