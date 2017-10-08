using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fantabulous.Core.Services;

namespace Fantabulous.Api.Controllers
{
    [Route("api/pseud")]
    public class PseudController : BaseController
    {
        private readonly IPseudService Service;
        private readonly ILogger Logger;

        public PseudController(
            IPseudService pseudService,
            ILogger<PseudController> logger)
        {
            Service = pseudService;
            Logger = logger;
        }

        // GET api/pseud/id/123
        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            return PreJson(await Service.GetPseudJsonAsync(id));
        }

        // GET api/pseud/name/123/example
        [HttpGet("name/{userId}/{name}")]
        public async Task<ActionResult> GetByName(long userId, string name)
        {
            return PreJson(await Service.GetPseudJsonAsync(userId, name));
        }

        // GET api/pseud/ids/123,456,789
        [HttpGet("ids/{ids}")]
        public async Task<ActionResult> GetByIds(string ids)
        {
            return PreJson(await Service.GetPseudJsonsAsync(ParseIds(ids)));
        }
    }
}
