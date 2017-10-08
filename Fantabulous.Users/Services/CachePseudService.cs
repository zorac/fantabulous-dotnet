using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Users.Services
{
    /// <summary>
    /// A pseudonym service providing an intermediate cache.
    /// </summary>
    /// <inheritDoc/>
    public class CachePseudService : IPseudService
    {
        private readonly IIdCache<Pseud> Cache;
        private readonly IPseudService Service;
        private readonly ILogger Logger;

        /// <summary>
        /// Create a new pseudonym cache service.
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
        public CachePseudService(
            IIdCache<Pseud> cache,
            IPseudService upstreamService,
            ILogger<CachePseudService> logger)
        {
            Cache = cache;
            Service = upstreamService;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public Task<Pseud> GetPseudAsync(long id)
        {
            return Cache.GetAsync(id, Service.GetPseudAsync);
        }

        public Task<Pseud> GetPseudAsync(long userId, string name)
        {
            return Service.GetPseudAsync(userId, name); // TODO cache?
        }

        public Task<IEnumerable<Pseud>> GetPseudsAsync(IEnumerable<long> ids)
        {
            return Cache.GetAsync(ids, Service.GetPseudsAsync);
        }

        public Task<string> GetPseudJsonAsync(long id)
        {
            return Cache.GetJsonAsync(id, Service.GetPseudAsync);
        }

        public Task<string> GetPseudJsonAsync(long userId, string name)
        {
            return Service.GetPseudJsonAsync(userId, name); // TODO cache?
        }

        public Task<IEnumerable<string>> GetPseudJsonsAsync(IEnumerable<long> ids)
        {
            return Cache.GetJsonAsync(ids, Service.GetPseudsAsync);
        }
    }
}
