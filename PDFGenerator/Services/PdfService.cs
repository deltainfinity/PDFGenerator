using System.Text;
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
        public byte[] ConvertUrlToPdf(UrlDTO url)
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

        ///<inheritdoc />
        public byte[] ConvertHtmlToPdf(HTMLToPDFDTO document)
        {
            var globalSettings = CreateGlobalSettings(document);

            var objectSettings = CreateObjectSettings(document);

            var pdf = CreatePdf(globalSettings, objectSettings);

            return pdf;
        }
        
        ///<inheritdoc />
        public byte[] ConvertTemplateToPdf(TemplateToPDFDTO document)
        {
            CreateHtmlFromTemplate(document);

            var globalSettings = CreateGlobalSettings(document);

            var objectSettings = CreateObjectSettings(document);

            var pdf = CreatePdf(globalSettings, objectSettings);

            return pdf;
        }

        /// <summary>
        /// Create the PDF from global and object settings
        /// </summary>
        /// <param name="globalSettings">Global Settings</param>
        /// <param name="objectSettings">Object Settings</param>
        /// <returns>PDF document</returns>
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

        /// <summary>
        /// Create the global settings
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GlobalSettings CreateGlobalSettings(HTMLToPDFDTO document)
        {
            var bottom = document.GlobalSettings.MarginBottom ?? 0.0;
            var left = document.GlobalSettings.MarginLeft ?? 0.0;
            var right = document.GlobalSettings.MarginRight ?? 0.0;
            var top = document.GlobalSettings.MarginTop ?? 0.0;

            var globalSettings = new GlobalSettings()
            {
                ColorMode = ColorMode.Color,
                Copies = document.GlobalSettings.Copies ?? 1,
                DocumentTitle = document.GlobalSettings.DocumentTitle ?? "",
                Margins = new MarginSettings(top, right, bottom, left),
                Orientation =
                    document.GlobalSettings.PortraitOrientation ? Orientation.Portrait : Orientation.Landscape,
                PaperSize = PaperKind.A4
            };

            return globalSettings;
        }

        /// <summary>
        /// Create the object settings
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Object settings</returns>
        private ObjectSettings CreateObjectSettings(HTMLToPDFDTO document)
        {
            var web = document.DocumentSettings.WebSettings ?? new WebSettingsDTO()
                          {DefaultEncoding = "utf-8", EnableJavascript = true};

            var header = document.DocumentSettings.HeaderSettings ?? new HeaderSettingsDTO()
            {
                Center = "", 
                FontName = "Arial", 
                FontSize = 12, 
                Left = "", 
                Line = false, 
                Right = "",
                HtmlUrl = "",
                Spacing = 0.0
            };

            if (header.HtmlUrl.Length > 0)
            {
                header.Center = "";
                header.Left = "";
                header.Right = "";
            }

            var footer = document.DocumentSettings.FooterSettings ?? new FooterSettingsDTO()
            {
                Center = "",
                FontName = "Arial",
                FontSize = 12,
                Left = "",
                Line = false,
                Right = "",
                HtmlUrl = "",
                Spacing = 0.0
            };

            if (footer.HtmlUrl.Length > 0)
            {
                footer.Center = "";
                footer.Left = "";
                footer.Right = "";
            }

            var objectSettings = new ObjectSettings()
            {
                PagesCount = document.DocumentSettings.PagesCount ?? false,
                HtmlContent = document.HtmlContent,
                WebSettings = {DefaultEncoding = web.DefaultEncoding, EnableJavascript = web.EnableJavascript},
                HeaderSettings = {Center = header.Center ?? "", FontName = header.FontName ?? "Arial", FontSize = header.FontSize ?? 12, Left = header.Left ?? "", Line = header.Line ?? false, Right = header.Right ?? "", Spacing = header.Spacing ?? 0.0, HtmUrl = header.HtmlUrl ?? ""},
                FooterSettings = {Center = footer.Center ?? "", FontName = footer.FontName ?? "Arial", FontSize = footer.FontSize ?? 12, Left = footer.Left ?? "", Line = footer.Line ?? false, Right = footer.Right ?? "", Spacing = footer.Spacing ?? 0.0, HtmUrl = header.HtmlUrl ?? ""}
            };

            return objectSettings;
        }

        /// <summary>
        /// Replace all of the template entries with the actual values to create the dynamic HTML document
        /// </summary>
        /// <param name="document"></param>
        private void CreateHtmlFromTemplate(TemplateToPDFDTO document)
        {
            var html = new StringBuilder(document.HtmlContent);

            foreach (var item in document.Values)
            {
                var placeholder = "{{" + item.Key + "}}";
                html.Replace(placeholder, item.Value);
            }

            document.HtmlContent = html.ToString();
        }
    }
}
