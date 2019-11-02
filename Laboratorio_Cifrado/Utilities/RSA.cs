using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using Laboratorio_Cifrado.Controllers;
using Laboratorio_Cifrado.Models;
using Microsoft.Ajax.Utilities;
using System.Text;
using System.Numerics;

namespace Laboratorio_Cifrado.Utilities
{
    public class RSA
    {
        private const int bufferLength = 1024;
        private static byte ConvertStringToByte(string value)
        {
            return Convert.ToByte(value, 2);
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


            int leer = Convert.ToInt32(N);
            string n = Convert.ToString(leer, 2);
            decimal cant_bytes = Math.Ceiling(Convert.ToDecimal(n.Length) / 8);
            List<byte> escribir = new List<byte>();
            var buffer = new byte[bufferLength];
            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        // var buffer = reader.ReadBytes(bufferLength);
                        List<byte> CompresionBytes = new List<byte>();

                        foreach (var item in buffer)
                        {
                            //Comprimir
                            // int _byte = (int)item; 
                            BigInteger Potencia = BigInteger.Pow(item, power);
                            BigInteger Mod = (Potencia % N);
                            string bits = Convert.ToString((int)Mod, 2);
                            string pp = bits.PadLeft(Convert.ToInt32((cant_bytes = 8)), '0');
                            while (pp.Length != 8)
                            {
                                escribir.Add((Convert.ToByte(pp.Substring(0, 8), 2)));
                                pp = pp.Remove(0, 8);
                            }
                            string Encriptado = pp;
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
