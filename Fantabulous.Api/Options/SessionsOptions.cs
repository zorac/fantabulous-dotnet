using System;

using Fantabulous.Redis;

namespace Fantabulous.Api.Options
{
    public class SessionsOptions
    {
        public RedisOptions Redis { get; set; }
        public string CookieName { get; set; } = "fantabulous.session";
        public TimeSpan Timeout { get; set; } = TimeSpan.FromHours(1);
    }
}
