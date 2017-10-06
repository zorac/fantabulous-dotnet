using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Tags.Services
{
    /// <summary>
    /// A tag service providing an intermediate cache.
    /// </summary>
    public class CacheTagService : ITagService
    {
        private readonly IIdCache<Tag> Cache;
        private readonly ITagService Service;
        private readonly ILogger Logger;

        /// <summary>
        /// Create a new tag cache service.
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
        public CacheTagService(
            IIdCache<Tag> cache,
            ITagService upstreamService,
            ILogger<CacheTagService> logger)
        {
            Cache = cache;
            Service = upstreamService;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public Task<Tag> GetTagAsync(long id)
        {
            return Cache.GetAsync(id, Service.GetTagAsync);
        }

        public Task<IEnumerable<Tag>> GetTagsAsync(IEnumerable<long> ids)
        {
            return Cache.GetAsync(ids, Service.GetTagsAsync);
        }

        public Task<string> GetTagJsonAsync(long id)
        {
            return Cache.GetJsonAsync(id, Service.GetTagAsync);
        }

        public Task<IEnumerable<string>> GetTagJsonsAsync(IEnumerable<long> ids)
        {
            return Cache.GetJsonAsync(ids, Service.GetTagsAsync);
        }
    }
}
