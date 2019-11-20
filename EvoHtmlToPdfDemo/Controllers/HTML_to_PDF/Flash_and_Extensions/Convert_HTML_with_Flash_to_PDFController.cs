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
    public class Convert_HTML_with_Flash_to_PDFController : Controller
    {
        // GET: Convert_HTML_with_Flash_to_PDF
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

            // Enable extensions including the Flash support
            // The converter does not have a built-in Flash player and it uses the installed Flash plugin
            // for Google Chrome or Mozilla Firefox
            htmlToPdfConverter.ExtensionsEnabled = collection["extensionsEnabledCheckBox"].Count > 0;

            // Set an adddional delay in seconds to wait for Flash to run
            // Set this property to 0 if you want to start conversion immediately
            htmlToPdfConverter.ConversionDelay = int.Parse(collection["conversionDelayTextBox"]);

            // Convert the HTML page with Flash to a PDF document in a memory buffer
            byte[] outPdfBuffer = htmlToPdfConverter.ConvertUrl(collection["urlTextBox"]);
            
            // Send the PDF file to browser
            FileResult fileResult = new FileContentResult(outPdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "Flash_to_PDF.pdf";

            return fileResult;
        }
    }
}