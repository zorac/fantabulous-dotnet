using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Services;

namespace Fantabulous.Users.Services
{
    public class MockUserService : IAuthService, IUserService, IPseudService
    {
        private readonly ILogger Logger;

        public MockUserService(ILogger<MockUserService> logger)
        {
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public Task<User> LoginAsync(string username, string password)
        {
            if (NameHasId(username, out int id))
            {
                Logger.LogInformation("Login succeeded for username {0}",
                    username);
                return Task.FromResult<User>(new User { Id = id, Name = username });
            }
            else
            {
                Logger.LogError("Login failed: invalid username {0}", username);
                throw new AuthenticationException(
                    "Invalid username, must be 'user<id>'");
            }
        }

        public Task<User> CreateUserAsync(string username, string password, string email)
        {
            if (NameHasId(username, out int id))
            {
                Logger.LogInformation("Created username {0}",
                    username);
                return Task.FromResult<User>(new User { Id = id, Name = username });
            }
            else
            {
                Logger.LogError("User create failed: invalid username {0}", username);
                throw new AuthenticationException(
                    "Invalid username, must be 'user<id>'");
            }
        }

        public Task ChangePasswordAsync(long id, string oldPassword, string newPassword)
        {
            return Task.CompletedTask;
        }

        public Task<User> GetUserAsync(long id)
        {
            return Task.FromResult(new User { Id = id, Name = $"user{id}" });
        }

        public Task<User> GetUserAsync(string name)
        {
            return Task.FromResult(NameHasId(name, out int id)
               ? new User { Id = id, Name = name } : null);
        }

        public Task<IEnumerable<User>> GetUsersAsync(IEnumerable<long> ids)
        {
            var users = new List<User>();

            foreach (var id in ids)
            {
                users.Add(new User { Id = id, Name = $"user{id}" });
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

        private bool NameHasId(string name, out int id)
        {
            id = 0;
            return name.StartsWith("user")
                && int.TryParse(name.Substring(4), out id);
        }

        public Task<Pseud> GetPseudAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Pseud> GetPseudAsync(long userId, string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pseud>> GetPseudsAsync(IEnumerable<long> ids)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPseudJsonAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPseudJsonAsync(long userId, string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetPseudsJsonAsync(IEnumerable<long> ids)
        {
            throw new NotImplementedException();
        }
    }
}
