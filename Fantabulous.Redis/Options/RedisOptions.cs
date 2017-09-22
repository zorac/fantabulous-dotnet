using System;

namespace Fantabulous.Redis.Options
{
    public class RedisOptions
    {
        public string Hostname { get; set; }
        public int Port { get; set; } = 6379;
        public string Password { get; set; }
    }
}
