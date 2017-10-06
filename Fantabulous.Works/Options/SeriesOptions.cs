using Fantabulous.Core.Entities;
using Fantabulous.Redis.Options;

namespace Fantabulous.Works.Options
{
    /// <summary>
    /// Configuration options for the series service.
    /// </summary>
    public class SeriesOptions
    {
        /// <summary>
        /// Set thes options to include a Redis series cache.
        /// </summary>
        public RedisCacheOptions<Series> Redis { get; set; }

        /// <summary>
        /// Set to true to include a SQL series service.
        /// </summary>
        public bool Sql { get; set; } = false;
    }
}
