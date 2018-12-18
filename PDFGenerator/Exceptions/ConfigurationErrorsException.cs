using System;

namespace PDFGenerator.Exceptions
{
    /// <summary>
    /// Class to encapsulate configuration errors during startup
    /// </summary>
    public class ConfigurationErrorsException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfigurationErrorsException()
        { }

        /// <summary>
        /// Constructor with an error message parameter
        /// </summary>
        /// <param name="message">Error message</param>
        public ConfigurationErrorsException(string message) : base(message)
        { }

        /// <summary>
        /// Constructor with an error message and inner exception
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="innerException">Inner Exception</param>
        public ConfigurationErrorsException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
