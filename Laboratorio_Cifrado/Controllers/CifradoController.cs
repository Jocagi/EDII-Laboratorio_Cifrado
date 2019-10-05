using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Laboratorio_Cifrado.Utilities;


namespace Laboratorio_Cifrado.Controllers
{
    public class CifradoController : Controller
    {
        public static string directorioArchivos = System.Web.HttpContext.Current.Server.MapPath("~/Archivos/");
        public static string directorioUploads = System.Web.HttpContext.Current.Server.MapPath("~/Archivos/Uploads/");
        public static string currentFile = "";

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string password, string cifrado, string operacion)
        {
            //Validar datos de entrada
            
            if (file != null)
            {
                //Subir Archivo
                if (validarPassword(password, cifrado))
                {
                    string path = Path.Combine(directorioUploads, Path.GetFileName(file.FileName) ?? "");
                    UploadFile(path, file);


                    switch (operacion)
                    {
                        case "1": //Cifrar
                            switch (cifrado)
                            {
                                case "1": //Cesar

                                    Cesar.Cifrado(path, password);

                                    break;
                                case "2": //Zig Zag

                                    int corrimiento = Convert.ToInt32(password);
                                    ZigZag.Cifrado(path, corrimiento);

                                    break;
                                case "3": //Espiral

                                    int clave = Convert.ToInt32(password);
                                    Espiral.Cifrar(path, clave);

                                    break;
                            }
                            break;
                        case "2": //Descifrar
                            switch (cifrado)
                            {
                                case "1": //Cesar

                                    Cesar.Descifrar(path, password);

                                    break;
                                case "2": //Zig Zag

                                    int corrimiento = Convert.ToInt32(password);
                                    ZigZag.Descifrar(path, corrimiento);

                                    break;
                                case "3": //Espiral

                                    int clave = Convert.ToInt32(password);
                                    Espiral.Descifrar(path, clave);

                                    break;
                            }
                            break;
                    }

                    return RedirectToAction("Download");
                }
            }
            else
            {
                ViewBag.Message = "No ha especificado un archivo.";
            }

            return View();
        }

        public ActionResult Download()
        {
            return View();
        }

        #region download
        public ActionResult DownloadFile()
        {
            string path = currentFile;

            if (!String.IsNullOrEmpty(path))
            {
                byte[] filedata = System.IO.File.ReadAllBytes(path);
                
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = Path.GetFileName(path),
                    Inline = true,
                };

                currentFile = "";

                Response.AppendHeader("Content-Disposition", cd.ToString());
                return File(filedata, "application/force-download");
            }
            else
            {
                ViewBag.Message = "No existe el archivo";
                return RedirectToAction("Download");
            }
        }
        #endregion
        
        #region upload
        public void UploadFile(string path, HttpPostedFileBase file)
        {
            //Subir archivos al servidor

            if (file != null && file.ContentLength > 0)
                try
                {
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    file.SaveAs(path);
                    ViewBag.Message = "Carga Exitosa";

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message;
                }
            else
            {
                ViewBag.Message = "No ha especificado un archivo.";
            }
        }
        #endregion

        #region Contraseña

        private bool validarPassword(string password, string cifrado)
        {
            bool resultado = false;

            int i;
            switch (cifrado)
            {
                case "1": //Cesar

                    if (password.All(char.IsLetter))
                    {
                        if (password.Distinct().Count() == password.Length)
                        {
                            resultado = true;
                        }
                        else
                        {
                            ViewBag.Message = "No se puede usar esa clave, porfavor elige una palabra con letras diferentes.";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "No se puede usar esa clave, porfavor escribe una palabra";
                    }
                    

                    break;
                case "2": //Zig Zag

                    if (int.TryParse(password, out i))
                    {
                        if (i < 10000 && i > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            ViewBag.Message = "La contraseña está fuera del rango";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "La contraseña debe consistir de números";
                    }
                    
                    break;
                case "3": //Espiral

                    if (int.TryParse(password, out i))
                    {
                        if (i < 10000 && i > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            ViewBag.Message = "La contraseña está fuera del rango";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "La contraseña debe consistir de números";
                    }

                    break;
            }
            
            return resultado;
        }
        
        #endregion
    }
}