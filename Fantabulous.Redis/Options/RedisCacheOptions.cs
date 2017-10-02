using Fantabulous.Core.Entities;

namespace Fantabulous.Redis.Options
{
    /// <summary>
    /// Options for a Redis cache.
    /// </summary>
    public class RedisCacheOptions<T> : RedisOptions where T: HasId
    {
    }
}
