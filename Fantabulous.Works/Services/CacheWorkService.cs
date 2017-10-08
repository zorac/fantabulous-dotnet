using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Works.Services
{
    /// <summary>
    /// A work service providing an intermediate cache.
    /// </summary>
    /// <inheritDoc/>
    public class CacheWorkService : IWorkService
    {
        private readonly IIdCache<Work> Cache;
        private readonly IWorkService Service;
        private readonly ILogger Logger;

        /// <summary>
        /// Create a new work cache service.
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
        public CacheWorkService(
            IIdCache<Work> cache,
            IWorkService upstreamService,
            ILogger<CacheWorkService> logger)
        {
            Cache = cache;
            Service = upstreamService;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public Task<Work> GetWorkAsync(long id)
        {
            return Cache.GetAsync(id, Service.GetWorkAsync);
        }

        public Task<IEnumerable<Work>> GetWorksAsync(IEnumerable<long> ids)
        {
            return Cache.GetAsync(ids, Service.GetWorksAsync);
        }

        public Task<string> GetWorkJsonAsync(long id)
        {
            return Cache.GetJsonAsync(id, Service.GetWorkAsync);
        }

        public Task<IEnumerable<string>> GetWorkJsonsAsync(IEnumerable<long> ids)
        {
            return Cache.GetJsonAsync(ids, Service.GetWorksAsync);
        }
    }
}
