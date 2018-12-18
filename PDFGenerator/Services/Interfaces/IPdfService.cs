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
        /// Convert HTML string to PDF
        /// </summary>
        /// <param name="url">URL to convert to PDFs</param>
        /// <returns>Byte array containing the PDF</returns>
        byte[] ConvertUrlToHtmlToPdfDocument(UrlDTO url);
    }
}
