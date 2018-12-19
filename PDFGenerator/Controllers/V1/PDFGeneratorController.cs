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
        /// Create a PDF document from an HTML string and a set of properties
        /// </summary>
        /// <param name="document">The document containing the HTML and the properties</param>
        /// <returns>byte[] containing the generated PDF</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("PDFFromHTML")]
        public ActionResult<byte[]> CreatePdfFromHtml([FromBody] HTMLToPDFDTO document)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pdf = PdfService.ConvertHtmlToPdf(document);

                return Ok(pdf);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Error creating PDF from HTML: {e.Message}");
                return StatusCode(500, $"Error creating PDF from HTML: {e.Message}");
            }
        }

        /// <summary>
        /// Create a PDF from a URL
        /// </summary>
        /// <param name="page">The URL of the page</param>
        /// <returns>byte[] containing a PDF of the HTML page located at the URL</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("PDFFromURL")]
        public ActionResult<byte[]> CreatePdfFromUrl([FromBody]UrlDTO page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var pdf = PdfService.ConvertUrlToPdf(page);

                return Ok(pdf);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Error creating PDF from URL: {e.Message}");
                return StatusCode(500, $"Error creating PDF from URL: {e.Message}");
            }
        }

        /// <summary>
        /// Create a PDF from an HTML template and a set of values
        /// </summary>
        /// <param name="document">HTML template and dictionary of replacement values along with PDF properties</param>
        /// <returns>byte[] containing PDF document</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("PDFFromTemplate")]
        public ActionResult<byte[]> CreatePdfFromTemplate([FromBody] TemplateToPDFDTO document)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pdf = PdfService.ConvertTemplateToPdf(document);

                return Ok(pdf);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Error creating PDF from HTML template and values: {e.Message}");
                return StatusCode(500, $"Error creating PDF from HTML template and values: {e.Message}");
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
        public ActionResult<byte[]> CreatePdfFromUrlLoadTest()
        {
            try
            {
                var pdf = PdfService.ConvertUrlToPdf(new UrlDTO(){Url = "https://www.google.com"});
 
                return Ok(pdf);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Error creating PDF from google.com URL in load test endpoint: {e.Message}");
                return StatusCode(500, $"Error creating PDF from google.com URL in load test endpoint: {e.Message}");
            }
        }
    }
}