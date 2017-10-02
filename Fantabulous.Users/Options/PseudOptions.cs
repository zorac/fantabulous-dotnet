using Fantabulous.Core.Entities;
using Fantabulous.Redis.Options;

namespace Fantabulous.Users.Options
{
    /// <summary>
    /// Configuration options for the pseudonym service.
    /// </summary>
    public class PseudOptions
    {
        /// <summary>
        /// Set thes options to include a Redis pseudonym cache.
        /// </summary>
        public RedisCacheOptions<Pseud> Redis { get; set; }

        /// <summary>
        /// Set to true to include a SQL pseudonym service.
        /// </summary>
        public bool Sql { get; set; } = false;

        /// <summary>
        /// Set to true to include a Mock pseudonym service.
        /// </summary>
        public bool Mock { get; set; } = false;
    }
}
