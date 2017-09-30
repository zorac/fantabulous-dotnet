using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fantabulous.Api.Controllers;
using Fantabulous.Core.Services;

namespace Fantabulous.Api.Users.Controllers
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserService Service;
        private readonly ILogger Logger;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger)
        {
            Service = userService;
            Logger = logger;
        }

        // GET api/user/id/123
        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            return PreJson(await Service.GetUserJsonAsync(id));
        }

        // GET api/user/name/example
        [HttpGet("name/{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
            return PreJson(await Service.GetUserJsonAsync(name));
        }

        // GET api/user/ids/123,456,789
        [HttpGet("ids/{ids}")]
        public async Task<ActionResult> GetByIds(string ids)
        {
            return PreJson(await Service.GetUsersJsonAsync(ParseIds(ids)));
        }
    }
}
