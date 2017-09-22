using System;
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
    [Route("api/user")]
    public class UserController : FantabulousController
    {
        private readonly IUserService Service;
        private readonly ILogger Logger;

        public UserController(
            IUserService userService,
            ILogger<AuthenticationController> logger)
        {
            Service = userService;
            Logger = logger;
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return PreJson(Service.GetUserJson(id));
        }

        // POST api/user
        [HttpPost]
        public ActionResult Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
