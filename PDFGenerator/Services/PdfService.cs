using DinkToPdf;
using DinkToPdf.Contracts;
using PDFGenerator.DTOs;
using PDFGenerator.Services.Interfaces;

namespace PDFGenerator.Services
{
    /// <summary>
    /// Class for performing HTML to PDF conversions
    /// </summary>
    public class PdfService : IPdfService
    {
        private IConverter Converter { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="converter"></param>
        public PdfService(IConverter converter)
        {
            Converter = converter;
        }

        ///<inheritdoc />
        public byte[] ConvertUrlToHtmlToPdfDocument(UrlDTO url)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Vant4ge PDF Generator"
            };
 
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                Page = url.Url,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Vant4gePoint" }
            };

            return CreatePdf(globalSettings, objectSettings);
        }

        /// <summary>
        /// Create the PDF from global and object settings
        /// </summary>
        /// <param name="globalSettings">Global Settings</param>
        /// <param name="objectSettings">Object Settings</param>
        /// <returns></returns>
        private byte[] CreatePdf(GlobalSettings globalSettings, ObjectSettings objectSettings)
        {
            var document = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = {objectSettings}
            };

            var pdf = Converter.Convert(document);
            return pdf;
        }
    }
}
