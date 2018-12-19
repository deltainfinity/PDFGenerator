using System.Collections.Generic;

namespace PDFGenerator.DTOs
{
    /// <summary>
    /// Load settings used when converting a web page to PDF
    /// </summary>
    public class LoadSettingsDTO
    {
        /// <summary>
        /// Disallow local and piped files to access other local files. Default = false
        /// Null = default
        /// </summary>
        public bool? BlockLocalFileAccess { get; set; }

        /// <summary>
        /// Cookies used when requesting page. Default = empty
        /// Null = default
        /// </summary>
        public Dictionary<string, string> Cookies { get; set; }

        /// <summary>
        /// Custom headers used when requesting page. Default = empty
        /// Null = default
        /// </summary>
        public Dictionary<string, string> CustomHeaders { get; set; }

        /// <summary>
        /// The amount of time in milliseconds to wait after a page has done loading until it is actually printed. E.g. "1200".
        /// We will wait this amount of time or until, javascript calls window.print(). Default = 200
        /// Null = default
        /// </summary>
        public int? JSDelay { get; set; }

    }
}
