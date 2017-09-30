using Fantabulous.Core.Models;

namespace Fantabulous.Redis
{
    /// <summary>
    /// Options for a Redis cache.
    /// </summary>
    public class RedisCacheOptions<T> : RedisOptions where T: HasId
    {
    }
}
