using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using Laboratorio_Cifrado.Controllers;

namespace Laboratorio_Cifrado.Utilities
{
    public class Cesar
    {
        public static void Cifrado(string path, string llave)
        {
            #region Alfabeto
            List<char> alfabeto = new List<char>();
            alfabeto.Add('A');
            alfabeto.Add('B');
            alfabeto.Add('C');
            alfabeto.Add('D');
            alfabeto.Add('E');
            alfabeto.Add('F');
            alfabeto.Add('G');
            alfabeto.Add('H');
            alfabeto.Add('I');
            alfabeto.Add('J');
            alfabeto.Add('K');
            alfabeto.Add('L');
            alfabeto.Add('M');
            alfabeto.Add('N');
            alfabeto.Add('O');
            alfabeto.Add('P');
            alfabeto.Add('Q');
            alfabeto.Add('R');
            alfabeto.Add('S');
            alfabeto.Add('T');
            alfabeto.Add('U');
            alfabeto.Add('V');
            alfabeto.Add('W');
            alfabeto.Add('X');
            alfabeto.Add('Y');
            alfabeto.Add('Z');
            #endregion
            int Corrimiento = Convert.ToInt32(llave);
            string Data = System.IO.File.ReadAllText(path, Encoding.Default);
             //List<char> Cifrar = Data.ToList<Char>();
            /* char[] MensajeCifrado = new char[];
             MensajeCifrado = Corrimiento.ToArray();
             MensajeCifrado = alfabeto.ToArray();
             //MensajeCifrado = Data.ToCharArray();
             List<char> Lista = MensajeCifrado.Cast<char>().ToList();
             List<char> result = alfabeto.Except(Lista).ToList();
             var dic = result.Zip(alfabeto, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);*/
            int i, j;
            char[] MensajeCifrado = new char[Data.Length];
            MensajeCifrado = Data.ToCharArray();

            for(i=0; i<MensajeCifrado.Length; i++)
            {
                for(j=0; j<Corrimiento; j++)
                {
                    if ((MensajeCifrado[i] >= 65 && MensajeCifrado[i] < 90 || (MensajeCifrado[i] >= 97 && MensajeCifrado[i] < 122)))
                        {
                        MensajeCifrado[i]++;
                    }
                    else if (MensajeCifrado[i] == 90)
                        MensajeCifrado[i] = 'A';
                    else if (MensajeCifrado[i] == 122)
                        MensajeCifrado[i] = 'a';
                }
            }
            List<char> Lista = MensajeCifrado.Cast<char>().ToList();
            Data = Lista.ToString();

        }

        public void Descifrar()
        {

        }
    }
}