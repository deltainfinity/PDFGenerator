﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PDFGenerator.Controllers.Base;
using PDFGenerator.DTOs;
using PDFGenerator.Services.Interfaces;

namespace PDFGenerator.Controllers.V1
{
    /// <summary>
    /// Controller for performing all PDF generation functions
    /// </summary>
    [ApiVersion("1.0")]
    public class PDFGeneratorController : BaseApiController
    {
        private IPdfService PdfService { get; set; }
        private IMemoryCache MemoryCache { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache">Memory cache</param>
        /// <param name="pdfService">Service for performing PDF creation functions</param>
        public PDFGeneratorController(IMemoryCache cache, IPdfService pdfService) : base(cache)
        {
            PdfService = pdfService;
            MemoryCache = cache;
        }

        /// <summary>
        /// Create a PDF from a URL
        /// </summary>
        /// <param name="pageUrl">The URL of the page</param>
        /// <returns>A PDF of the HTML page located at the URL</returns>
        [HttpGet]
        [Route("PDFFromURL")]
        public ActionResult CreatePdfFromUrl()
        {
            try
            {
                var pdf = PdfService.ConvertUrlToHtmlToPdfDocument(new UrlDTO(){Url = "https://www.google.com"});
 
                return File(pdf, "application/pdf");
            }
            catch (Exception e)
            {
                //Logger.Error(e, $"Error creating PDF from URL: {e.Message}");
                return StatusCode(500, $"Error creating PDF from URL: {e.Message}");
            }
        }
    }
}