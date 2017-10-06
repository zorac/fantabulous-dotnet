using Fantabulous.Core.Entities;
using Fantabulous.Redis.Options;

namespace Fantabulous.Works.Options
{
    /// <summary>
    /// Configuration options for the chapter service.
    /// </summary>
    public class ChapterOptions
    {
        /// <summary>
        /// Set thes options to include a Redis chapter cache.
        /// </summary>
        public RedisCacheOptions<Chapter> Redis { get; set; }

        /// <summary>
        /// Set to true to include a SQL chapter service.
        /// </summary>
        public bool Sql { get; set; } = false;
    }
}
