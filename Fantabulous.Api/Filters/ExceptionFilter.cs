using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using Fantabulous.Core.Exceptions;

namespace Fantabulous.Api.Filters
{
    /// <summary>
    /// A filter which handles uncaught exceptions.
    /// </summary>
    /// <inheritDoc/>
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger Logger;

        /// <summary>
        /// Create a new filter.
        /// </summary>
        /// <param name="logger">
        /// A logger for this filter
        /// </param>
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            Logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var status = 500;
            var message = exception.Message;
            Dictionary<string,string> details = null;

            if (exception is AuthorizationException)
            {
                status = 403;
            }
            else if (exception is ValidationException)
            {
                status = 400;
                details = (exception as ValidationException).Errors;
            }
            else if (exception is AuthenticationException)
            {
                status = 400;
            }
            else
            {
                Logger.LogError(exception, "Unexpected exeption");
                message = null; // Avoid information leak
            }

            var response = new
            {
                Message = message,
                Details = details
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = status
            };
        }
    }
}
