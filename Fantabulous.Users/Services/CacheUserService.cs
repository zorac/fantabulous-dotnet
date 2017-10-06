using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Users.Services
{
    /// <summary>
    /// A user service providing an intermediate cache.
    /// </summary>
    public class CacheUserService : IUserService
    {
        private readonly IIdNameCache<User> Cache;
        private readonly IUserService Service;
        private readonly ILogger Logger;

        /// <summary>
        /// Create a new user cache service.
        /// </summary>
        /// <param name="cache">
        /// A cache where data will be stored
        /// </param>
        /// <param name="upstreamService">
        /// An upstream service to resolve cache misses
        /// </param>
        /// <param name="logger">
        /// A logger for this service
        /// </param>
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

        public Task<User> GetUserAsync(long id)
        {
            return Cache.GetAsync(id, Service.GetUserAsync);
        }

        public Task<User> GetUserAsync(string name)
        {
            return Cache.GetAsync(name, Service.GetUserAsync);
        }

        public Task<IEnumerable<User>> GetUsersAsync(IEnumerable<long> ids)
        {
            return Cache.GetAsync(ids, Service.GetUsersAsync);
        }

        public Task<string> GetUserJsonAsync(long id)
        {
            return Cache.GetJsonAsync(id, Service.GetUserAsync);
        }

        public Task<string> GetUserJsonAsync(string name)
        {
            return Cache.GetJsonAsync(name, Service.GetUserAsync);
        }

        public Task<IEnumerable<string>> GetUserJsonsAsync(IEnumerable<long> ids)
        {
            return Cache.GetJsonAsync(ids, Service.GetUsersAsync);
        }
    }
}
