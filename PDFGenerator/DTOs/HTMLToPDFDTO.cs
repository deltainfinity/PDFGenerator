using System.ComponentModel.DataAnnotations;

namespace PDFGenerator.DTOs
{
    /// <summary>
    /// Holds data to convert an HTML document to a PDF
    /// </summary>
    public class HTMLToPDFDTO
    {
        /// <summary>
        /// Global PDF conversion settings
        /// </summary>
        [Required]
        public GlobalSettingsDTO GlobalSettings { get; set; }

        /// <summary>
        /// PDF document settings
        /// </summary>
        [Required]
        public DocumentSettingsDTO DocumentSettings { get; set; }

        /// <summary>
        /// The HTML to convert to PDF
        /// </summary>
        [Required]
        public string HtmlContent { get; set; }

    }
}
