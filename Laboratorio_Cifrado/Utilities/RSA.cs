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

        private static int ConvertByteStringToInt(string value)
        {
            return Convert.ToInt16(value, 2);
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

        public static int byteSize(long x)
        {
            if (x < 0) return -1;
            int s = 0;
            int actualSize = 0;
            while (x > actualSize)
            {
                actualSize += (int)Math.Pow(2, s);
                s++;
            }
            
            return s;
        }

        //Utilities.RSA.Cifrar(pathArchivo, power, N);
        /*Inge sinceramente este buffer lo hice yo hace como 2 labs pero ahora ya solo Yisus
        Y quizas Jose saben como funciona. :c*/
        public static void Cifrar(string path, int power, int N)
        {
            #region Crear Archivo
            string NuevoArchivo = Path.GetFileNameWithoutExtension(path) + ".txt";
            string rutaCifrado = CifradoController.directorioArchivos + NuevoArchivo;
            Archivo.crearArchivo(rutaCifrado);
            #endregion
            
            using (var file = new FileStream(path, FileMode.Open))
            {
                
                using (var reader = new BinaryReader(file))
                {
                    List<byte> CompresionBytes = new List<byte>();
                    string individualBits = "";
                    int ByteSize = byteSize(N);
                    int ReadLenght = 8;
                    string readBits = "";

                    //Agregaar un flag para saber si es un archivo cifrado o descifrado
                    byte firstByte = reader.ReadByte();
                    if (firstByte != (byte)0)
                    {
                        CompresionBytes.Add((byte)0);
                        reader.BaseStream.Position = 0;
                    }
                    else
                    {
                        ReadLenght = ByteSize;
                        ByteSize = 8;
                    }

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        var buffer = reader.ReadBytes(bufferLength);
                        int actual = 0;

                        foreach (var item in buffer)
                        {
                            if (readBits.Length < ReadLenght)
                            {
                                readBits += Convert.ToString((int) item, 2).PadLeft(8, '0');
                            }
                            if (readBits.Length >= ReadLenght)
                            {
                                actual = ConvertByteStringToInt(readBits.Substring(0, ReadLenght));
                                readBits = readBits.Remove(0, ReadLenght);

                                BigInteger Potencia = BigInteger.Pow(actual, power);
                                BigInteger Mod = (Potencia % N);
                                string bits = Convert.ToString((int)Mod, 2).PadLeft(ByteSize, '0');

                                individualBits += bits;

                                while (individualBits.Length > 8)
                                {
                                    CompresionBytes.Add((Convert.ToByte(individualBits.Substring(0, 8), 2)));
                                    individualBits = individualBits.Remove(0, 8);
                                }

                                WriteToFile(rutaCifrado, CompresionBytes.ToArray());
                                CompresionBytes.Clear();
                            }
                        }
                    }

                    if (individualBits.Length != 0)
                    {
                        while (individualBits.Length < 8)
                        {
                            individualBits += "0";
                        }
                        WriteToFile(rutaCifrado, new []{Convert.ToByte(individualBits, 2)});
                    }
                }
            }
            
            CifradoController.currentFile = rutaCifrado;
        }
      
    }
}
