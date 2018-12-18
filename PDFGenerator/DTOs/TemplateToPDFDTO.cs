using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PDFGenerator.DTOs
{
    /// <summary>
    /// Holds value information to construct an HTML document using the template stored in the parent class HTMLContent field
    /// </summary>
    public class TemplateToPDFDTO : HTMLToPDFDTO
    {
        /// <summary>
        /// Key, value pairs to replace template placeholders
        /// </summary>
        [Required]
        public Dictionary<string, string> Values { get; set; }
    }
}
