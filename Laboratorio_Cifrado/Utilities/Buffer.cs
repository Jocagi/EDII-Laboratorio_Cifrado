using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Laboratorio_Cifrado.Controllers;
using Laboratorio_Cifrado.Models;

namespace Laboratorio_Cifrado.Utilities
{
    public class Buffer
    {
        public static void Leer(string text, string path)
        {
            List<char> tata = new List<char>();
            int txt = Convert.ToInt32(text);
            int bufferLength = txt;
            var buffer = new byte[bufferLength];
            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        buffer = reader.ReadBytes(bufferLength);
                        foreach (var item in buffer)
                        {
                            tata.Add((char)item);
                        }
                    }
                }
            }
        }
        public static void Escritura(string text, string path)
        {
            List<char> tata = new List<char>();
            int txt = Convert.ToInt32(text);
            int bufferLength = txt;
            var buffer = new byte[bufferLength];
            using (var file = new FileStream(path, FileMode.Create))
            {
                using (var writer = new BinaryWriter(file))
                {
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = Convert.ToByte(100 + i);
                    }
                    writer.Write(buffer);
                }
            }

        }

    }
}