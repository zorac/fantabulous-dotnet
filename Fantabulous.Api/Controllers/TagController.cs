using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fantabulous.Core.Services;

namespace Fantabulous.Api.Controllers
{
    [Route("api/tag")]
    public class TagController : BaseController
    {
        private readonly ITagService Service;
        private readonly ILogger Logger;

        public TagController(
            ITagService tagService,
            ILogger<TagController> logger)
        {
            Service = tagService;
            Logger = logger;
        }

        // GET api/tag/id/123
        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            return PreJson(await Service.GetTagJsonAsync(id));
        }

        // GET api/tag/ids/123,456,789
        [HttpGet("ids/{ids}")]
        public async Task<ActionResult> GetByIds(string ids)
        {
            return PreJson(await Service.GetTagJsonsAsync(ParseIds(ids)));
        }
    }
}
