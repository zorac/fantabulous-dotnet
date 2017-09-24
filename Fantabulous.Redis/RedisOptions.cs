namespace Fantabulous.Redis
{
    public class RedisOptions
    {
        public string Hostname { get; set; }
        public int Port { get; set; } = 6379;
        public int Database { get; set; } = 0;
        public string Password { get; set; }
    }
}
