using System.Threading.Tasks;

using Fantabulous.Core.Models;

namespace Fantabulous.Core.Services
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string username, string password);
        Task<bool> ChangePasswordAsync(long id, string oldPassword, string newPassword);
    }
}
