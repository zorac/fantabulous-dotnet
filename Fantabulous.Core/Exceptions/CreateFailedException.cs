using System;

namespace Fantabulous.Core.Exceptions
{
    /// <summary>
    /// An exception thrown due to a failure to create an entity.
    /// </summary>
    /// <inheritDoc/>
    public class CreateFailedException : Exception
    {
        /// <summary>
        /// Create a new exception.
        /// </summary>
        /// <param name="message">
        /// A message describing the error
        /// </param>
        /// <param name="innerException">
        /// An optional underlying exception
        /// </param>
        public CreateFailedException(
            string message,
            Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
