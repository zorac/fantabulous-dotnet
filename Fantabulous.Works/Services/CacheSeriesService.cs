using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Works.Services
{
    /// <summary>
    /// A series service providing an intermediate cache.
    /// </summary>
    /// <inheritDoc/>
    public class CacheSeriesService : ISeriesService
    {
        private readonly IIdCache<Series> Cache;
        private readonly ISeriesService Service;
        private readonly ILogger Logger;

        /// <summary>
        /// Create a new series cache service.
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
        public CacheSeriesService(
            IIdCache<Series> cache,
            ISeriesService upstreamService,
            ILogger<CacheSeriesService> logger)
        {
            Cache = cache;
            Service = upstreamService;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public Task<Series> GetSeriesAsync(long id)
        {
            return Cache.GetAsync(id, Service.GetSeriesAsync);
        }

        public Task<IEnumerable<Series>> GetSeriesAsync(IEnumerable<long> ids)
        {
            return Cache.GetAsync(ids, Service.GetSeriesAsync);
        }

        public Task<string> GetSeriesJsonAsync(long id)
        {
            return Cache.GetJsonAsync(id, Service.GetSeriesAsync);
        }

        public Task<IEnumerable<string>> GetSeriesJsonsAsync(IEnumerable<long> ids)
        {
            return Cache.GetJsonAsync(ids, Service.GetSeriesAsync);
        }
    }
}
