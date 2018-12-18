using DinkToPdf;
using PDFGenerator.DTOs;

namespace PDFGenerator.Services.Interfaces
{
    /// <summary>
    /// Interface for PDF creation services
    /// </summary>
    public interface IPdfService
    {
        /// <summary>
        /// Convert a web page at a specified URL to PDF
        /// </summary>
        /// <param name="url">URL to convert to PDFs</param>
        /// <returns>Byte array containing the PDF</returns>
        byte[] ConvertUrlToPdf(UrlDTO url);

        /// <summary>
        /// Convert an HTML string to a PDF
        /// </summary>
        /// <param name="document">The HTML document to convert along with the associated conversion settings</param>
        /// <returns></returns>
        byte[] ConvertHtmlToPdf(HTMLToPDFDTO document);

        /// <summary>
        /// Convert an HTML template to a dynamic HTML document and then convert that document to a PDF
        /// </summary>
        /// <param name="document">HTML template and values used to construct the HTML document along with the associated conversion settings</param>
        /// <returns></returns>
        byte[] ConvertTemplateToPdf(TemplateToPDFDTO document);
    }
}
