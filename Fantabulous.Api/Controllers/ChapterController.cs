using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fantabulous.Core.Services;

namespace Fantabulous.Api.Controllers
{
    [Route("api/chapter")]
    public class ChapterController : BaseController
    {
        private readonly IChapterService Service;
        private readonly ILogger Logger;

        public ChapterController(
            IChapterService chapterService,
            ILogger<ChapterController> logger)
        {
            Service = chapterService;
            Logger = logger;
        }

        // GET api/chapter/id/123
        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            return PreJson(await Service.GetChapterJsonAsync(id));
        }

        // GET api/chapter/ids/123,456,789
        [HttpGet("ids/{ids}")]
        public async Task<ActionResult> GetByIds(string ids)
        {
            return PreJson(await Service.GetChapterJsonsAsync(ParseIds(ids)));
        }
    }
}
