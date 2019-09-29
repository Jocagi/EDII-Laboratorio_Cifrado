using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using Laboratorio_Cifrado.Models;

namespace Laboratorio_Cifrado.Controllers
{
    public class CifradoController : Controller
    {
        public static string currentFile = "";
        // GET: Cifrado
        public ActionResult Index()
        {
            return View();
        }

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
    }
}