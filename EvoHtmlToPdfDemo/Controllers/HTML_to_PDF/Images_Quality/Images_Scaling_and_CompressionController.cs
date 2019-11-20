using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// Use EVO PDF Namespace
using EvoPdf;

namespace EvoHtmlToPdfDemo.Controllers.HTML_to_PDF.Images_Quality
{
    public class Images_Scaling_and_CompressionController : Controller
    {
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

            // Set the JPEG Compression Level
            // A higher compression level produces smaller PDF documents but lower quality images in PDF
            // Leave it not set for a default compression level
            htmlToPdfConverter.PdfDocumentOptions.JpegCompressionLevel = int.Parse(collection["jpegCompressionLevelTextBox"]);

            // Set images scaling before rendering to PDF
            // This option enables the converter to scale the images used in HTML down to their display size in HTML before rendering them in PDF. 
            // This can lead to smaller memory consumption during conversion and to a smaller PDF document size but the images quality in PDF can be lower
            htmlToPdfConverter.PdfDocumentOptions.ImagesScalingEnabled = collection["imagesScalingCheckBox"].Count > 0;

            // Convert the HTML page to a PDF document in a memory buffer
            byte[] outPdfBuffer = htmlToPdfConverter.ConvertUrl(collection["urlTextBox"]);

            // Send the PDF file to browser
            FileResult fileResult = new FileContentResult(outPdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "Images_Scaling_and_JPEG_Compression.pdf";

            return fileResult;
        }
    }
}