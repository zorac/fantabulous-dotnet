using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public async Task<ActionResult> Login(string username, string password)
        {
            var user = await Service.LoginAsync(username, password);

            HttpContext.Session.Login(user);
            Logger.LogInformation("Logged in username {0}", username);

            return Json(user);
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
