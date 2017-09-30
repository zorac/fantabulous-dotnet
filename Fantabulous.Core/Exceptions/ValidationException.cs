using System;
using System.Collections.Generic;

namespace Fantabulous.Core.Exceptions
{
    /// <summary>
    /// An exception thrown due to issues with validating user input.
    /// </summary>
    /// <inheritDoc/>
    public class ValidationException : Exception
    {
        private const string MESSAGE = "Validation failed";

        /// <summary>
        /// A dictonary mapping field names to error messages.
        /// </summary>
        public Dictionary<string,string> Errors { get; }

        /// <summary>
        /// Create a validation exception with a dictionaty of errors.
        /// </summary>
        /// <param name="errors">
        /// Some errors
        /// </param>
        /// <param name="innerException">
        /// An optional underlying exception
        /// </param>
        public ValidationException(
            Dictionary<string,string> errors,
            Exception innerException = null)
            : base(MESSAGE, innerException)
        {
            Errors = errors;
        }

        /// <summary>
        /// Create a validation exception with a single error message.
        /// </summary>
        /// <param name="field">
        /// The affected field
        /// </param>
        /// <param name="error">
        /// An error message
        /// </param>
        /// <param name="innerException">
        /// An optional underlying exception
        /// </param>
        public ValidationException(
            string field,
            string error,
            Exception innerException = null)
            : base(MESSAGE, innerException)
        {
            Errors = new Dictionary<string,string>();
            Errors.Add(field, error);
        }
    }
}
