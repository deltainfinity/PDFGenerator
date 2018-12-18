using System.ComponentModel.DataAnnotations;

namespace PDFGenerator.DTOs
{
    /// <summary>
    /// Holds the URL of an HTML page to convert to a PDF
    /// </summary>
    public class UrlDTO
    {
        /// <summary>
        /// The URL of the HTML page
        /// </summary>
        [Required]
        public string Url { get; set; }
    }
}
