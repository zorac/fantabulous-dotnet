using System;

using Fantabulous.Redis;

namespace Fantabulous.Api.Options
{
    /// <summary>
    /// Options used to configure sessions.
    /// </summary>
    public class SessionsOptions
    {
        /// <summary>
        /// Options of the Redis instance where sessions will be stored.
        /// </summary>
        public RedisOptions Redis { get; set; }

        /// <summary>
        /// Set to specify that a memory cache should be used to store sessions.
        /// </summary>
        public bool Memory = false;

        /// <summary>
        /// The name of the cookie containing the session ID (defaults to
        /// "fantabulous.session").
        /// </summary>
        public string CookieName { get; set; } = "fantabulous.session";

        /// <summary>
        /// The lifetime of a session (defaults to one hour).
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromHours(1);
    }
}
