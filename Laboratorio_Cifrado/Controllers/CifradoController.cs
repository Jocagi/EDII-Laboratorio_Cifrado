using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using Laboratorio_Cifrado.Utilities;


namespace Laboratorio_Cifrado.Controllers
{
    public class CifradoController : Controller
    {
        public static string directorioUploads = System.Web.HttpContext.Current.Server.MapPath("~/Archivos/Uploads");
        public static string currentFile = "";

        // GET: Cifrado
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string password, string cifrado, string operacion)
        {
            //Validar datos de entrada
            //ToDo...

            switch (operacion)
            {
                case "1": //Cifrar
                    switch (cifrado)
                    {
                        case "1": //Cesar
                            break;
                        case "2": //Zig Zag
                            break;
                        case "3": //Espiral
                            break;
                    }
                    break;
                case "2": //Descifrar
                    switch (cifrado)
                    {
                        case "1": //Cesar
                            break;
                        case "2": //Zig Zag
                            break;
                        case "3": //Espiral
                            break;
                    }
                    break;
            }
            
            return View();
        }
        
        #region download
        public ActionResult DownloadFile()
        {
            string path = currentFile;

            byte[] filedata = System.IO.File.ReadAllBytes(path);
            string contentType = MimeMapping.GetMimeMapping(path);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = Path.GetFileName(path),
                Inline = true,
            };

            currentFile = "";

            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(filedata, contentType);
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

        #region ZigZag

        public ActionResult CifrarZigZag()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CifrarZigZag(HttpPostedFileBase file)
        {
            try
            {
                string path = Path.Combine(directorioUploads, Path.GetFileName(file.FileName));

                UploadFile(path, file);
            //    ZigZag.Cifrado(path, corrimiento);
                
                
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message;
                throw;
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}