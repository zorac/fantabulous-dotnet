using Fantabulous.Core.Models;
using Fantabulous.Redis;

namespace Fantabulous.Users
{
    public class UserOptions
    {
        public RedisCacheOptions<User> Redis { get; set; }
    }
}
