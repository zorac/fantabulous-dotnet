using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fantabulous.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        private const string JSON = "application/json";

        protected ContentResult BoolJson(bool result)
        {
            return Content(result ? "true" : "false", JSON);
        }

        protected ContentResult PreJson(string json)
        {
            return Content(json, JSON);
        }

        protected ContentResult PreJson(IEnumerable<string> jsons)
        {
            return Content("[" + String.Join(",", jsons) + "]", JSON);
        }
    }
}
