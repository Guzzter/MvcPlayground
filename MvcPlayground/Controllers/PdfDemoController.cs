
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPlayground.Models;
using Rotativa;
using Rotativa.Options;

namespace DownloadPdf.Controllers
{
    /// <summary>
    /// Class to generate pdf
    /// </summary>
    public class PdfDemoController : Controller
    {
        //
        // GET: /Download/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action Method using viewAsPdf class to create view as pdf
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadPdf()
        {
            try
            {
                var model = new GeneratePDFModel();

                //get the information to display in pdf from database
                //for the time
                //Hard coding values are here, these are the content to display in pdf 
                var content = "<h2>WOW Rotativa<h2>" +
                 "<p>Ohh This is very easy to generate pdf using Rotativa <p>";
                var logoFile = @"/Images/logo.png";

                model.PDFContent = content;
                model.PDFLogo = Server.MapPath(logoFile);

                string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";

                //Use ViewAsPdf Class to generate pdf using GeneratePDF.cshtml view
                return new Rotativa.ViewAsPdf("GeneratePDF", model)
                {
                    FileName = "firstPdf.pdf",
                    CustomSwitches = footer,
                    CustomHeaders = new Dictionary<string, string> { { "Test", "Title" } }

                };
            }
            catch
            {

                throw;
            }
        }



        public ActionResult DownloadPNGImage()
        {
            return new ActionAsImage("GeneratePDF", new { name = "Guus" }) { FileName = "Test.png", Format = ImageFormat.png };
        }

        /// <summaroy>
        /// This method is using ActionAsPdf class to generate pdf
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadActionAsPDF()
        {
            try
            {
                //will take ActionMethod and generate the pdf
                return new Rotativa.ActionAsPdf("GeneratePDF") { FileName = "SecondPdf.pdf" };
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Action method to return view as pdf
        /// </summary>
        /// <returns></returns>
        public ActionResult GeneratePDF(string name = "there")
        {
            try
            {
                var model = new GeneratePDFModel();
                //Your data from db

                //hard coded value for test purpose
                var content = "<h2>PDF Created<h2>" +
                "<p>Ohh This is very easy to generate pdf using Rotativa<p>";
                var logoFile = @"/Images/logo.png";

                model.PDFContent = content;
                model.PDFLogo = Server.MapPath(logoFile);
                model.Name = name;

                return View(model);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// using partial view for pdf generation
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadPartialViewPDF()
        {
            var model = new GeneratePDFModel();

            //hard coded value for test purpose
            var content = "<h2>PDF Created<h2>" +
            "<p>Ohh This is very easy to generate pdf using Rotativa<p>";
            var logoFile = @"/Images/logo.png";

            model.PDFContent = content;
            model.PDFLogo = Server.MapPath(logoFile);

            return new Rotativa.PartialViewAsPdf("_PartialViewTest", model) { FileName = "partialViewAsPdf.pdf" };
        }

        /// <summary>
        /// The method will geneate ulr content in pdf doc
        /// </summary>
        /// <returns></returns>
        public ActionResult UrlAsPDF()
        {

            //this will generate google home page to in a pdf doc
            return new Rotativa.UrlAsPdf("http://www.Google.com")
            {
                FileName = "urltest.pdf",
            };
        }

        public FileResult DisplayPdfFromDisk()
        {
            return File("/Images/Temp.pdf", "application/pdf");
        }

        public FileResult DownloadPdfFromDisk()
        {
            return File("/Images/Temp.pdf", "application/pdf", "downloadFilename.pdf");
        }

        public FileResult DownloadPdfFromBytes()
        {
            string filepath = Server.MapPath("/Images/Temp.pdf");
            byte[] pdfByte = Helper.GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf", "downloadBytesFilename.pdf");
        }

    }

    class Helper
    {
        public static byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

        }
    }
}
