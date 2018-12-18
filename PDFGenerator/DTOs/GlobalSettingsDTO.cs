using System.ComponentModel.DataAnnotations;

namespace PDFGenerator.DTOs
{
    /// <summary>
    /// Global PDF conversion options
    /// </summary>
    public class GlobalSettingsDTO
    {
        /// <summary>
        /// The number of copies to print. Default = 1 copy.
        /// Null will be handled as default.
        /// </summary>
        public int? Copies { get; set; }

        /// <summary>
        /// The title of the PDF document. Default = "".
        /// Null will be handled as default.
        /// </summary>
        public string DocumentTitle { get; set; }

        /// <summary>
        /// Bottom margin. Null = 0.
        /// </summary>
        public double? MarginBottom { get; set; }

        /// <summary>
        /// Left margin. Null = 0.
        /// </summary>
        public double? MarginLeft { get; set; }

        /// <summary>
        /// Right margin. Null = 0.
        /// </summary>
        public double? MarginRight { get; set; }

        /// <summary>
        /// Top margin. Null = 0.
        /// </summary>
        public double? MarginTop { get; set; }

        /// <summary>
        /// Is the document orientation portrait or landscape?
        /// True = Portrait, False = Landscape
        /// </summary>
        [Required]
        public bool PortraitOrientation { get; set; }
    }
}
