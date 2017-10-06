using Fantabulous.Core.Entities;
using Fantabulous.Redis.Options;

namespace Fantabulous.Works.Options
{
    /// <summary>
    /// Configuration options for the work service.
    /// </summary>
    public class WorkOptions
    {
        /// <summary>
        /// Set thes options to include a Redis work cache.
        /// </summary>
        public RedisCacheOptions<Work> Redis { get; set; }

        /// <summary>
        /// Set to true to include a SQL work service.
        /// </summary>
        public bool Sql { get; set; } = false;
    }
}
