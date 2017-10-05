using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fantabulous.Core.Requests;
using Fantabulous.Core.Services;

namespace Fantabulous.Api.Controllers
{
    [Route("api/auth")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthService Service;
        private readonly ILogger Logger;

        public AuthenticationController(
            IAuthService authService,
            ILogger<AuthenticationController> logger)
        {
            Service = authService;
            Logger = logger;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await Service.LoginAsync(request);

            HttpContext.Session.Login(response.User, response.Pseud);
            Logger.LogInformation("Logged in username {0}", request.Username);

            return Json(response);
        }

        // POST api/auth/logout
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            if (await HttpContext.Session.IsLoggedInAsync())
            {
                Logger.LogInformation("Logged out username {0}",
                     await HttpContext.Session.GetUserNameAsync());
                HttpContext.Session.Logout();
                return BoolJson(true);
            }
            else
            {
                return BoolJson(false);
            }
        }
    }
}
