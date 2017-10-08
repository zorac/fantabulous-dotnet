using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fantabulous.Core.Services;

namespace Fantabulous.Api.Controllers
{
    [Route("api/series")]
    public class SeriesController : BaseController
    {
        private readonly ISeriesService Service;
        private readonly ILogger Logger;

        public SeriesController(
            ISeriesService seriesService,
            ILogger<SeriesController> logger)
        {
            Service = seriesService;
            Logger = logger;
        }

        // GET api/series/id/123
        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            return PreJson(await Service.GetSeriesJsonAsync(id));
        }

        // GET api/series/ids/123,456,789
        [HttpGet("ids/{ids}")]
        public async Task<ActionResult> GetByIds(string ids)
        {
            return PreJson(await Service.GetSeriesJsonsAsync(ParseIds(ids)));
        }
    }
}
