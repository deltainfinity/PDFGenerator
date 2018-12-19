namespace PDFGenerator.DTOs
{
    /// <summary>
    /// Web settings
    /// </summary>
    public class WebSettingsDTO
    {
        /// <summary>
        /// What encoding should we guess content is using if they do not specify it properly. Default = "utf-8"
        /// Null = "utf-8"
        /// </summary>
        public string DefaultEncoding { get; set; }

        /// <summary>
        /// Should we enable javascript. Default = true
        /// Null = true
        /// </summary>
        public bool? EnableJavascript { get; set; }

    }
}
