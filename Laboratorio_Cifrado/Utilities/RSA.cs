using System;
using System.Collections.Generic;
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
        private const int _8bitDivide = 4;
        private const int _10bitDivide = 5;
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

            #region variables
            //Declaracion de variables
            int p = P;
            int q = Q;
            int n = p * q;
            int n2 = p * q;
            int phi = ((p - 1) * (q - 1));
            int phi2 = ((p - 1) * (q - 1)); //Utilizo un segundo phi para la obtenicón de la d
            int phi3 = ((p - 1) * (q - 1)); //El tercer phi solo sirve para aplicar el MOD, se crean 3 ya que en cada método se le iba cambiando el valor a phi.
            int a;
            int d = 1;
            int contador = 0;
            int e;
            #endregion

            //Aqui se obtiene la primer columna
            List<int> Cocientes = new List<int>();
            Dictionary<int, int> prueba = new Dictionary<int, int>();
            do
            {
                int cociente = phi / e;
                int resultado = cociente * e;
                a = phi - resultado;
                phi = e;
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
                    contador = contador++;
                }
            }
            while (contador < CocienteArreglo.Length);
        }

        private byte ConvertStringToByte(string value)
        {
            return Convert.ToByte(Convert.ToUInt32(value, 2));
        }

        private string ConvertByteToString(byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0'); //Produce cadena "00111111";
        }

        private void  WriteToFile(string path, byte[] value)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(value);
            bw.Close();
        }



        /*Inge sinceramente este buffer lo hice yo hace como 2 labs pero ahora ya solo Yisus
        Y quizas Jose saben como funciona. :c*/
        private void Cifrar(string path, int d, int e, int n)
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

                        foreach(var item in buffer)
                        {
                            //Comprimir
                            string _byte = ConvertByteToString(item);
                            string Encriptado = (n % (_byte ^ e));

                            CompresionBytes.Add(ConvertStringToByte(Encriptado));
                        }
                        WriteToFile(rutaCifrado, CompresionBytes.ToArray());
                    }
                }
            }

            CifradoController.currentFile = rutaCifrado;
        }
        private void Descifrar(string path, int d, string Encriptado, int n)
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
                            string _byte = ConvertByteToString(item);
                            string Descencriptado = (n % (_byte ^ d));

                            CompresionBytes.Add(ConvertStringToByte(Descencriptado));
                        }
                        WriteToFile(rutaCifrado, CompresionBytes.ToArray());
                    }
                }
            }

            CifradoController.currentFile = rutaCifrado;
        }
    }
}