using Fantabulous.Core.Models;

namespace Fantabulous.Redis
{
    public class RedisCacheOptions<T> : RedisOptions where T: HasId
    {
    }
}
