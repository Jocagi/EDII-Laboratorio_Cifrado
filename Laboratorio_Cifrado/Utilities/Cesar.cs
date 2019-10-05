using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using Laboratorio_Cifrado.Controllers;
using Laboratorio_Cifrado.Models;
using Laboratorio_Cifrado.Utilities;

namespace Laboratorio_Cifrado.Utilities
{
    public class Cesar
    {
        private const int bufferLength = 1024;

        public static void Cifrado(string path, string llave)
        {

            #region Crear_Archivo

            string NuevoArchivo = Path.GetFileName(path);
            string Cifradoo = CifradoController.directorioUploads + NuevoArchivo;
            Archivo.crearArchivo(Cifradoo);

            #endregion
             
            #region Alfabeto Mayusculas
            List<char> AlfabetoM = new List<char>();
            AlfabetoM.Add('A');
            AlfabetoM.Add('B');
            AlfabetoM.Add('C');
            AlfabetoM.Add('D');
            AlfabetoM.Add('E');
            AlfabetoM.Add('F');
            AlfabetoM.Add('G');
            AlfabetoM.Add('H');
            AlfabetoM.Add('I');
            AlfabetoM.Add('J');
            AlfabetoM.Add('K');
            AlfabetoM.Add('L');
            AlfabetoM.Add('M');
            AlfabetoM.Add('N');
            AlfabetoM.Add('O');
            AlfabetoM.Add('P');
            AlfabetoM.Add('Q');
            AlfabetoM.Add('R');
            AlfabetoM.Add('S');
            AlfabetoM.Add('T');
            AlfabetoM.Add('U');
            AlfabetoM.Add('V');
            AlfabetoM.Add('W');
            AlfabetoM.Add('X');
            AlfabetoM.Add('Y');
            AlfabetoM.Add('Z');
            AlfabetoM.Add(' ');
            #endregion
            
            string Data = System.IO.File.ReadAllText(path, Encoding.Default);

            #region Listas
            string clave = llave.ToUpper();
            string claveM = llave.ToLower();

            List<char> ListaFinal = new List<char>(); //Lista que sera el abecedario modificado
            List<char> ListaClave = clave.ToList();

            List<char> ListaClave2 = claveM.ToList();
            #endregion
            #region Diccionario Mayusculas
            List<char> Diferentes = AlfabetoM.Except(ListaClave).ToList();
            List<char> Repetidos = (ListaClave.AsQueryable().Intersect(AlfabetoM)).ToList(); //Expresion Lamba para encontrar repetidos
            ListaFinal = Repetidos.Union(Diferentes).ToList();
            var DiccionarioM = AlfabetoM.Zip(ListaFinal, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v); //Combinar listas y volverlas diccionario
            #endregion
            #region Diccionario Minusculas
            List<char> DiferentesP = AlfabetoP.Except(ListaClave2).ToList();
            List<char> RepetidosP = (ListaClave2.AsQueryable().Intersect(AlfabetoP)).ToList(); //Expresion Lamba para encontrar repetidos
            ListaFinal2 = RepetidosP.Union(DiferentesP).ToList();
            var DiccionarioP = AlfabetoP.Zip(ListaFinal2, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v); //Combinar listas y volverlas diccionario
            #endregion

            
            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    //Buffer para cifrar
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        var buffer = reader.ReadBytes(count: bufferLength);

                        List<char> Cifrado = new List<char>();
                        List<byte> CifradoFinal = new List<byte>();

                        foreach (var item in buffer)
                        { 
                            Cifrado.Add((char) item);
                        }
                        foreach (var item in Cifrado)
                        {
                            if (DiccionarioM.ContainsKey(item))
                            {
                                CifradoFinal.Add((byte) DiccionarioM[item]);
                            }
                            else if (DiccionarioP.ContainsKey(item))
                            {
                                CifradoFinal.Add((byte) DiccionarioP[item]);
                            }
                            else
                            {
                                CifradoFinal.Add((byte) item);
                            }
                        }
                        
                        FileStream fs = new FileStream(Cifradoo, FileMode.Append);
                        BinaryWriter bw = new BinaryWriter(fs);

                        bw.Write(CifradoFinal.ToArray());
                        bw.Close();
                    }
                }
            }
            
            CifradoController.currentFile = Cifradoo; //Aqui no se que movi, pero no tocar!
        }

        public void Descifrar(string path, string llave)
        {
            
            #region Alfabeto Mayusculas
            List<char> AlfabetoM = new List<char>();
            AlfabetoM.Add('A');
            AlfabetoM.Add('B');
            AlfabetoM.Add('C');
            AlfabetoM.Add('D');
            AlfabetoM.Add('E');
            AlfabetoM.Add('F');
            AlfabetoM.Add('G');
            AlfabetoM.Add('H');
            AlfabetoM.Add('I');
            AlfabetoM.Add('J');
            AlfabetoM.Add('K');
            AlfabetoM.Add('L');
            AlfabetoM.Add('M');
            AlfabetoM.Add('N');
            AlfabetoM.Add('O');
            AlfabetoM.Add('P');
            AlfabetoM.Add('Q');
            AlfabetoM.Add('R');
            AlfabetoM.Add('S');
            AlfabetoM.Add('T');
            AlfabetoM.Add('U');
            AlfabetoM.Add('V');
            AlfabetoM.Add('W');
            AlfabetoM.Add('X');
            AlfabetoM.Add('Y');
            AlfabetoM.Add('Z');
            AlfabetoM.Add(' ');
            #endregion
            #region Alfabeto Minusculas
            List<char> AlfabetoP = new List<char>();
            AlfabetoP.Add('a');
            AlfabetoP.Add('b');
            AlfabetoP.Add('c');
            AlfabetoP.Add('d');
            AlfabetoP.Add('e');
            AlfabetoP.Add('f');
            AlfabetoP.Add('g');
            AlfabetoP.Add('h');
            AlfabetoP.Add('i');
            AlfabetoP.Add('j');
            AlfabetoP.Add('k');
            AlfabetoP.Add('l');
            AlfabetoP.Add('m');
            AlfabetoP.Add('n');
            AlfabetoP.Add('o');
            AlfabetoP.Add('p');
            AlfabetoP.Add('q');
            AlfabetoP.Add('r');
            AlfabetoP.Add('s');
            AlfabetoP.Add('t');
            AlfabetoP.Add('u');
            AlfabetoP.Add('v');
            AlfabetoP.Add('w');
            AlfabetoP.Add('x');
            AlfabetoP.Add('y');
            AlfabetoP.Add('z');
            AlfabetoP.Add(' ');
            #endregion

            #region Crear_Archivo

            string NuevoArchivo = Path.GetFileName(path);
            string Descifradoo = CifradoController.directorioUploads + NuevoArchivo;
            Archivo.crearArchivo(Descifradoo);

            #endregion
            #region Listas
            string clave = llave.ToUpper();
            string claveM = llave.ToLower();

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
            List<char> ListaClave2 = claveM.ToList();
            #endregion
            #region Diccionario Mayusculas
            List<char> Diferentes = AlfabetoM.Except(ListaClave).ToList();
            List<char> Repetidos = (ListaClave.AsQueryable().Intersect(AlfabetoM)).ToList(); //Expresion Lamba para encontrar repetidos
            ListaFinal = Repetidos.Union(Diferentes).ToList();
            var DiccionarioM = ListaFinal.Zip(AlfabetoM, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v); //Combinar listas y volverlas diccionario
            #endregion
            #region Diccionario Minusculas
            List<char> DiferentesP = AlfabetoP.Except(ListaClave2).ToList();
            List<char> RepetidosP = (ListaClave2.AsQueryable().Intersect(AlfabetoP)).ToList(); //Expresion Lamba para encontrar repetidos
            ListaFinal2 = RepetidosP.Union(DiferentesP).ToList();
            var DiccionarioP = ListaFinal2.Zip(AlfabetoP, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v); //Combinar listas y volverlas diccionario
            #endregion

            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    //Buffer para descifrar
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        var buffer = reader.ReadBytes(count: bufferLength);
                        List<byte> DescifradoFinal = new List<byte>();

                        foreach (var item in buffer)
                        {
                            if (DiccionarioM.ContainsKey((char) item))
                            {
                                DescifradoFinal.Add((byte) DiccionarioM[(char) item]);
                            }
                            else if (DiccionarioP.ContainsKey((char) item))
                            {
                                DescifradoFinal.Add((byte) DiccionarioP[(char) item]);
                            }
                            else
                            {
                                DescifradoFinal.Add(item);
                            }
                        }
                        
                        FileStream fs = new FileStream(Descifradoo, FileMode.Append);
                        BinaryWriter bw = new BinaryWriter(fs);

                        bw.Write(DescifradoFinal.ToArray());
                        bw.Close();
                    }
                }
            }
            CifradoController.currentFile =Descifradoo; //Aqui no se que movi, pero no tocar!

        }
    }
    
}