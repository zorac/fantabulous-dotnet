using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Users
{
    public class CacheUserService : IUserService
    {
        private readonly IIdNameCache<User> Cache;
        private readonly IUserService Service;
        private readonly ILogger Logger;

        public CacheUserService(
            IIdNameCache<User> cache,
            IUserService upstreamService,
            ILogger<CacheUserService> logger)
        {
            Cache = cache;
            Service = upstreamService;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public async Task<User> GetUserAsync(long id)
        {
            var user = await Cache.GetAsync(id);

            if (user == null)
            {
                user = await Service.GetUserAsync(id);
                if (user != null) Cache.SetInBackground(user);
            }

            return user;
        }

        public async Task<User> GetUserAsync(string name)
        {
            var user = await Cache.GetAsync(name);

            if (user == null)
            {
                user = await Service.GetUserAsync(name);
                if (user != null) Cache.SetInBackground(user);
            }

            return user;
        }

        public Task<IEnumerable<User>> GetUsersAsync(IEnumerable<long> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserJsonAsync(long id)
        {
            var json = await Cache.GetJsonAsync(id);

            if (json == null)
            {
                var user = await Service.GetUserAsync(id);

                if (user != null) json = Cache.SetInBackground(user);
            }

            return json;
        }

        public async Task<string> GetUserJsonAsync(string name)
        {
            var json = await Cache.GetJsonAsync(name);

            if (json == null)
            {
                var user = await Service.GetUserAsync(name);

                if (user != null) json = Cache.SetInBackground(user);
            }

            return json;
        }

        public Task<IEnumerable<string>> GetUsersJsonAsync(IEnumerable<long> ids)
        {
            throw new NotImplementedException();
        }
    }
}
