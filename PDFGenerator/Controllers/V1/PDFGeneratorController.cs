using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PDFGenerator.Controllers.Base;
using PDFGenerator.DTOs;
using PDFGenerator.Services.Interfaces;
using Serilog;

namespace PDFGenerator.Controllers.V1
{
    /// <summary>
    /// Controller for performing all PDF generation functions
    /// </summary>
    [ApiVersion("1.0")]
    public class PDFGeneratorController : BaseApiController
    {
        private readonly ILogger Logger = Log.ForContext<PDFGeneratorController>();
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
        /// <param name="page">The URL of the page</param>
        /// <returns>A PDF of the HTML page located at the URL</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("PDFFromURL")]
        public ActionResult CreatePdfFromUrl([FromBody]UrlDTO page)
        {
            try
            {
                var pdf = PdfService.ConvertUrlToHtmlToPdfDocument(page);
 
                return File(pdf, "application/pdf");
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Error creating PDF from URL: {e.Message}");
                return StatusCode(500, $"Error creating PDF from URL: {e.Message}");
            }
        }

        /// <summary>
        /// Load test endpoint to be used by Azure DevOps to test maximum concurrent users
        /// </summary>
        /// <returns>A PDF of the google.com web page</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("LoadTest")]
        public ActionResult CreatePdfFromUrlLoadTest()
        {
            try
            {
                var pdf = PdfService.ConvertUrlToHtmlToPdfDocument(new UrlDTO(){Url = "https://www.google.com"});
 
                return File(pdf, "application/pdf");
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Error creating PDF from google.com URL in load test endpoint: {e.Message}");
                return StatusCode(500, $"Error creating PDF from google.com URL in load test endpoint: {e.Message}");
            }
        }
    }
}