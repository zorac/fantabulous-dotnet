using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fantabulous.Api.Controllers;
using Fantabulous.User;

namespace Fantabulous.Api.User.Controllers
{
    [Route("api")]
    public class AuthenticationController : FantabulousController
    {
        private readonly IUserService Service;
        private readonly ILogger Logger;

        public AuthenticationController(
            IUserService userService,
            ILogger<AuthenticationController> logger)
        {
            Service = userService;
            Logger = logger;
        }

        // POST api/login
        [HttpPost("login")]
        public ActionResult Login(string username, string password)
        {
            IUser user = Service.Login(username, password);

            HttpContext.Session.Login(user);
            Logger.LogInformation("Logged in username {0}", username);

            return Json(user);
        }

        // DELETE api/login
        [HttpDelete("login")]
        public ActionResult Logout()
        {
            if (HttpContext.Session.IsLoggedIn())
            {
                Logger.LogInformation("Logged out username {0}",
                     HttpContext.Session.GetUserName());
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
