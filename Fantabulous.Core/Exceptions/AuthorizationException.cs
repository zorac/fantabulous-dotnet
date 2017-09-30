using System;

namespace Fantabulous.Core.Exceptions
{
    /// <summary>
    /// An exception thrown when a user attempts to access a resource they are
    /// not authorized for.
    /// </summary>
    /// <inheritDoc/>
    public class AuthorizationException : Exception
    {
        /// <summary>
        /// Create a new authorization exception.
        /// </summary>
        /// <param name="message">
        /// A message describing the error
        /// </param>
        /// <param name="innerException">
        /// An optional underlying exception
        /// </param>
        public AuthorizationException(
            string message,
            Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
