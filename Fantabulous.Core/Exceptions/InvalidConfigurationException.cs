using System;

namespace Fantabulous.Core.Exceptions
{
    /// <summary>
    /// An exception thrown at startup due to an invalid configuration.
    /// </summary>
    /// <inheritDoc/>
    public class InvalidConfigurationException : Exception
    {
        /// <summary>
        /// Create a new invalid configuration exception.
        /// </summary>
        /// <param name="message">
        /// A message describing the error
        /// </param>
        /// <param name="innerException">
        /// An optional underlying exception
        /// </param>
        public InvalidConfigurationException(
            string message,
            Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
