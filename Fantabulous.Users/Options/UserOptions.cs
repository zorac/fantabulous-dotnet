using Fantabulous.Core.Entities;
using Fantabulous.Redis.Options;

namespace Fantabulous.Users.Options
{
    /// <summary>
    /// Configuration options for the user service.
    /// </summary>
    public class UserOptions
    {
        /// <summary>
        /// Set thes options to include a Redis user cache.
        /// </summary>
        public RedisCacheOptions<User> Redis { get; set; }

        /// <summary>
        /// Set to true to include a SQL user service.
        /// </summary>
        public bool Sql { get; set; } = false;

        /// <summary>
        /// Set to true to include a Mock user service.
        /// </summary>
        public bool Mock { get; set; } = false;
    }
}
