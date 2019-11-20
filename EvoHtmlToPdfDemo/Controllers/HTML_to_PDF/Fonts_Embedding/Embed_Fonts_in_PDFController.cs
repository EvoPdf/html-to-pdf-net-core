using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.Controllers
{
    public class Embed_Fonts_in_PDFController : Controller
    {
        // GET: Embed_Fonts_in_PDF
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConvertHtmlToPdf(IFormCollection collection)
        {
            // Create a HTML to PDF converter object with default settings
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            htmlToPdfConverter.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

            // Set an adddional delay in seconds to wait for JavaScript or AJAX calls after page load completed
            // Set this property to 0 if you don't need to wait for such asynchcronous operations to finish
            htmlToPdfConverter.ConversionDelay = 2;

            // Set if the fonts are embedded in the generated PDF document
            // Leave it not set to embed the fonts in the generated PDF document
            htmlToPdfConverter.PdfDocumentOptions.EmbedFonts = collection["embedFontsCheckBox"].Count > 0;

            // The buffer to receive the generated PDF document
            byte[] outPdfBuffer = null;

            if (collection["HtmlPageSource"] == "convertUrlRadioButton")
            {
                string url = collection["urlTextBox"];

                // Convert the HTML page given by an URL to a PDF document in a memory buffer
                outPdfBuffer = htmlToPdfConverter.ConvertUrl(url);
            }
            else
            {
                string htmlString = collection["htmlStringTextBox"];
                string baseUrl = collection["baseUrlTextBox"];

                // Convert a HTML string with a base URL to a PDF document in a memory buffer
                outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlString, baseUrl);
            }

            // Send the PDF file to browser
            FileResult fileResult = new FileContentResult(outPdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "Embed_Fonts_in_PDF.pdf";

            return fileResult;
        }
    }
}