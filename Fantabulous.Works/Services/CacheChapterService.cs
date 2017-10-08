using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Works.Services
{
    /// <summary>
    /// A chapter service providing an intermediate cache.
    /// </summary>
    /// <inhertitDoc/>
    public class CacheChapterService : IChapterService
    {
        private readonly IIdCache<Chapter> Cache;
        private readonly IChapterService Service;
        private readonly ILogger Logger;

        /// <summary>
        /// Create a new chapter cache service.
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
        public CacheChapterService(
            IIdCache<Chapter> cache,
            IChapterService upstreamService,
            ILogger<CacheChapterService> logger)
        {
            Cache = cache;
            Service = upstreamService;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public Task<Chapter> GetChapterAsync(long id)
        {
            return Cache.GetAsync(id, Service.GetChapterAsync);
        }

        public Task<IEnumerable<Chapter>> GetChaptersAsync(IEnumerable<long> ids)
        {
            return Cache.GetAsync(ids, Service.GetChaptersAsync);
        }

        public Task<string> GetChapterJsonAsync(long id)
        {
            return Cache.GetJsonAsync(id, Service.GetChapterAsync);
        }

        public Task<IEnumerable<string>> GetChapterJsonsAsync(IEnumerable<long> ids)
        {
            return Cache.GetJsonAsync(ids, Service.GetChaptersAsync);
        }
    }
}
