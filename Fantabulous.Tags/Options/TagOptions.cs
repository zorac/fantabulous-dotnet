using Fantabulous.Core.Entities;
using Fantabulous.Redis.Options;

namespace Fantabulous.Tags.Options
{
    /// <summary>
    /// Configuration options for the tag service.
    /// </summary>
    public class TagOptions
    {
        /// <summary>
        /// Set thes options to include a Redis tag cache.
        /// </summary>
        public RedisCacheOptions<Tag> Redis { get; set; }

        /// <summary>
        /// Set to true to include a SQL tag service.
        /// </summary>
        public bool Sql { get; set; } = false;
    }
}
