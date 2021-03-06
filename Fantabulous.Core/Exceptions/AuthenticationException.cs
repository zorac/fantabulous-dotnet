using System;

namespace Fantabulous.Core.Exceptions
{
    /// <summary>
    /// An exception thrown due to a failure to authenticate a user.
    /// </summary>
    /// <inheritDoc/>
    public class AuthenticationException : Exception
    {
        /// <summary>
        /// Create a new authentication exception.
        /// </summary>
        /// <param name="message">
        /// A message describing the error
        /// </param>
        /// <param name="innerException">
        /// An optional underlying exception
        /// </param>
        public AuthenticationException(
            string message,
            Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
