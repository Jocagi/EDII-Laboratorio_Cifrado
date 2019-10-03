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
            string Data = System.IO.File.ReadAllText(path, Encoding.Default);
            #region mientras
            //List<char> Cifrar = Data.ToList<Char>();
            /* char[] MensajeCifrado = new char[];
             MensajeCifrado = Corrimiento.ToArray();
             MensajeCifrado = alfabeto.ToArray();
             //MensajeCifrado = Data.ToCharArray();
             #
             List<char> Lista = MensajeCifrado.Cast<char>().ToList(); 
             List<char> result = alfabeto.Except(Lista).ToList();*/
            #endregion

            // var dic = result.Zip(alfabeto, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            //List<char> alfabeto = new List<char>();
            //alfabeto.Add('A');
            List<char> ListaFinal = new List<char>();
            List<char> ListaClave = llave.ToList();
            List<char> Repetidos = ListaClave.Where(i => alfabeto.Contains(i)).ToList();
            List<char> Diferentes = (ListaClave.AsQueryable().Intersect(alfabeto)).ToList();
            ListaFinal = Repetidos.Union(Diferentes).ToList();
            var diccionario = alfabeto.Zip(ListaFinal, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

        }

        public void Descifrar()
        {

        }
    }
}