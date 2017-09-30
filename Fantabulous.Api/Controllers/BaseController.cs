using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fantabulous.Core.Exceptions;

namespace Fantabulous.Api.Controllers
{
    /// <summary>
    /// Adds some useful methods to controllers.
    /// </summary>
    /// <inheritDoc/>
    public abstract class BaseController : Controller
    {
        private const string JSON = "application/json";

        /// <summary>
        /// Create a boolean JSON result.
        /// </summary>
        /// <param name="result">
        /// The result
        /// </param>
        /// <returns>
        /// A JSON result
        /// </returns>
        protected ContentResult BoolJson(bool result)
        {
            return Content(result ? "true" : "false", JSON);
        }

        /// <summary>
        /// Create a result using pre-formatted JSON.
        /// </summary>
        /// <param name="json">
        /// The JSON to return
        /// </param>
        /// <returns>
        /// A JSON result
        /// </returns>
        protected ContentResult PreJson(string json)
        {
            return Content(json, JSON);
        }

        /// <summary>
        /// Create a result using pre-formatted JSON.
        /// </summary>
        /// <param name="jsons">
        /// A sequence of JSON to return
        /// </param>
        /// <returns>
        /// A JSON result
        /// </returns>
        protected ContentResult PreJson(IEnumerable<string> jsons)
        {
            return Content("[" + String.Join(",", jsons) + "]", JSON);
        }

        /// <summary>
        /// Parse a comma-separated list of IDs.
        /// </summary>
        /// <param name="ids">
        /// Some comma-separated IDs, eg "123,456,789"
        /// </param>
        /// <returns>
        /// A sequence of numeric IDs
        /// </returns>
        protected IEnumerable<long> ParseIds(string ids)
        {
            try
            {
                var idStrings = ids.Split(",");
                var idLongs = new long[idStrings.Length];

                for (int i = 0; i < idStrings.Length; i++)
                {
                    idLongs[i] = long.Parse(idStrings[i]);
                }

                return idLongs;
            }
            catch (Exception e)
            {
                throw new ValidationException("ids", "Failed to parse ids", e);
            }
        }
    }
}
