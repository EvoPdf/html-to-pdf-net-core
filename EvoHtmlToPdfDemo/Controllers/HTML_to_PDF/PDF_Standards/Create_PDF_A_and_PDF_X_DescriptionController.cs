﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.Controllers.HTML_to_PDF.PDF_Standards
{
    public class Create_PDF_A_and_PDF_X_DescriptionController : Controller
    {
        // GET: Create_PDF_A_and_PDF_X_Description
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

            // Set the PDF Standard
            // By default the full PDF standard is used
            if (collection["PdfStandard"] == "pdfARadioButton")
                htmlToPdfConverter.PdfDocumentOptions.PdfStandardSubset = PdfStandardSubset.Pdf_A_1b;
            else if (collection["PdfStandard"] == "pdfXRadioButton")
                htmlToPdfConverter.PdfDocumentOptions.PdfStandardSubset = PdfStandardSubset.Pdf_X_1a;

            // Convert the HTML page to a PDF document in a memory buffer
            byte[] outPdfBuffer = htmlToPdfConverter.ConvertUrl(collection["urlTextBox"]);
            
            // Send the PDF file to browser
            FileResult fileResult = new FileContentResult(outPdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "PDF_A_PDF_X.pdf";

            return fileResult;
        }
    }
}