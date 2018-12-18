namespace PDFGenerator.DTOs
{
    /// <summary>
    /// Document settings for PDF conversion
    /// Maps to ObjectSettings in DinkToPdf library
    /// </summary>
    public class DocumentSettingsDTO
    {
        /// <summary>
        /// Header settings
        /// </summary>
        public HeaderSettingsDTO HeaderSettings { get; set; }

        /// <summary>
        /// Footer settings
        /// </summary>
        public FooterSettingsDTO FooterSettings { get; set; }

        /// <summary>
        /// Should we count the pages of this document, in the counter used for TOC, headers and footers. Default = false
        /// Null = false.
        /// If you are using any of the page count replacements in HeaderSettings or FooterSettings this value should be true
        /// </summary>
        public bool? PagesCount { get; set; }

        /// <summary>
        /// Web settings
        /// </summary>
        public WebSettingsDTO WebSettings { get; set; }
    }
}
