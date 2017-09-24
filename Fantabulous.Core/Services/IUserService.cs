using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Models;

namespace Fantabulous.Core.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(long id);
        Task<User> GetUserAsync(string name);
        Task<IEnumerable<User>> GetUsersAsync(IEnumerable<long> ids);
        Task<string> GetUserJsonAsync(long id);
        Task<string> GetUserJsonAsync(string name);
        Task<IEnumerable<string>> GetUsersJsonAsync(IEnumerable<long> ids);
    }
}
