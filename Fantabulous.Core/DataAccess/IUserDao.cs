using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Models;

namespace Fantabulous.Core.DataAccess
{
    public interface IUserDao
    {
        Task<User> ForIdAsync(long id);

        Task<IEnumerable<User>> ForIdsAsync(IEnumerable<long> ids);

        Task<User> ForNameAsync(string name);
    }
}
