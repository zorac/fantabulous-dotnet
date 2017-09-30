using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Fantabulous.Core.Models;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Services;

namespace Fantabulous.Users
{
    public class MockUserService : IUserService, IAuthService
    {
        private readonly ILogger Logger;

        public MockUserService(ILogger<MockUserService> logger)
        {
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public Task<User> GetUserAsync(long id)
        {
            return Task.FromResult(new User(id, $"user{id}"));
        }

        public Task<User> GetUserAsync(string name)
        {
            return Task.FromResult(NameHasId(name, out int id)
               ? new User(id, name) : null);
        }

        public Task<IEnumerable<User>> GetUsersAsync(IEnumerable<long> ids)
        {
            var users = new List<User>();

            foreach (var id in ids)
            {
                users.Add(new User(id, $"user{id}"));
            }

            return Task.FromResult<IEnumerable<User>>(users);
        }

        public Task<string> GetUserJsonAsync(long id)
        {
            return Task.FromResult($"{{\"id\":{id},\"name\":\"user{id}\"}}");
        }

        public Task<string> GetUserJsonAsync(string name)
        {
            return Task.FromResult(NameHasId(name, out int id)
                ? $"{{\"id\":{id},\"name\":\"{name}\"}}" : null);
        }

        public Task<IEnumerable<string>> GetUsersJsonAsync(IEnumerable<long> ids)
        {
            var users = new List<string>();

            foreach (var id in ids)
            {
                users.Add($"{{\"id\":{id},\"name\":\"user{id}\"}}");
            }

            return Task.FromResult<IEnumerable<string>>(users);
        }

        public Task<User> CreateUserAsync(string username, string password)
        {
            if (NameHasId(username, out int id))
            {
                Logger.LogInformation("Created username {0}",
                    username);
                return Task.FromResult<User>(new User(id, username));
            }
            else
            {
                Logger.LogError("User create failed: invalid username {0}", username);
                throw new AuthenticationException(
                    "Invalid username, must be 'user<id>'");
            }
        }

        public Task<User> LoginAsync(string username, string password)
        {
            if (NameHasId(username, out int id))
            {
                Logger.LogInformation("Login succeeded for username {0}",
                    username);
                return Task.FromResult<User>(new User(id, username));
            }
            else
            {
                Logger.LogError("Login failed: invalid username {0}", username);
                throw new AuthenticationException(
                    "Invalid username, must be 'user<id>'");
            }
        }

        public Task ChangePasswordAsync(long id, string oldPassword, string newPassword)
        {
            return Task.CompletedTask;
        }

        private bool NameHasId(string name, out int id)
        {
            id = 0;
            return name.StartsWith("user")
                && int.TryParse(name.Substring(4), out id);
        }
    }
}
