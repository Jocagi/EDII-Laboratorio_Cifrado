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
            alfabeto.Add(' ');
            #endregion
            
            string Data = System.IO.File.ReadAllText(path, Encoding.Default);

            string clave = llave.ToUpper();
            List<char> ListaFinal = new List<char>(); //Lista que sera el abecedario modificado
            List<char> ListaClave = clave.ToList();
            List<char> Diferentes = alfabeto.Except(ListaClave).ToList();
            List<char> Repetidos = (ListaClave.AsQueryable().Intersect(alfabeto)).ToList(); //Expresion Lamba para encontrar repetidos
            ListaFinal = Repetidos.Union(Diferentes).ToList();
            var diccionario = alfabeto.Zip(ListaFinal, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v); //Combinar listas y volverlas diccionario
           // List<char> Cifrado = Data.ToList();
            //List<char> CifradoFinal = new List<char>();
            string CifradoFinal;
            foreach (var item in Data)
            {
                if(diccionario.ContainsKey(item))
                {
                    // CifradoFinal.Add(diccionario[item]);
                    CifradoFinal = diccionario.Count.ToString();
                }
            }

            Console.ReadKey();

        }

        public void Descifrar(string path, string llave)
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
            alfabeto.Add(' ');
            #endregion
            string Data = System.IO.File.ReadAllText(path, Encoding.Default);

            string clave = llave.ToUpper();
            List<char> ListaFinal = new List<char>(); //Lista que sera el abecedario modificado
            List<char> ListaClave = clave.ToList();
            List<char> Diferentes = alfabeto.Except(ListaClave).ToList();
            List<char> Repetidos = (ListaClave.AsQueryable().Intersect(alfabeto)).ToList(); //Expresion Lamba para encontrar repetidos
            ListaFinal = Repetidos.Union(Diferentes).ToList();
            var diccionario = ListaFinal.Zip(alfabeto, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v); //Combinar listas y volverlas diccionario
            List<char> Descifrado = Data.ToList();
            List<char> DescifradoFinal = new List<char>();
            /*foreach (var item in Data)
            {
                if (diccionario.ContainsKey(item))
                {
                    DescifradoFinal.Add('k');
                }
            }*/

            Console.WriteLine(DescifradoFinal);

            Console.ReadKey();

        }
    }
    
}