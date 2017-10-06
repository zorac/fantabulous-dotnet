using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fantabulous.Core.Services;

namespace Fantabulous.Api.Controllers
{
    [Route("api/work")]
    public class WorkController : BaseController
    {
        private readonly IWorkService Service;
        private readonly ILogger Logger;

        public WorkController(
            IWorkService workService,
            ILogger<WorkController> logger)
        {
            Service = workService;
            Logger = logger;
        }

        // GET api/work/id/123
        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            return PreJson(await Service.GetWorkJsonAsync(id));
        }

        // GET api/work/ids/123,456,789
        [HttpGet("ids/{ids}")]
        public async Task<ActionResult> GetByIds(string ids)
        {
            return PreJson(await Service.GetWorkJsonsAsync(ParseIds(ids)));
        }
    }
}
