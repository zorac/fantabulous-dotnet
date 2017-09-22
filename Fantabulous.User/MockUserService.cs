using Microsoft.Extensions.Logging;

namespace Fantabulous.User
{
    public class MockUserService : IUserService
    {
        private readonly ILogger Logger;

        public MockUserService(ILogger<MockUserService> logger)
        {
            Logger = logger;
        }

        public IUser GetUser(int id)
        {
            return new User(id) { Name = $"user{id}" };
        }

        public IUser GetUser(string name)
        {
            return NameHasId(name, out int id)
               ? new User(id) { Name = name } : null;
        }

        public string GetUserJson(int id)
        {
            return $"{{\"id\":{id},\"name\":\"user{id}\"}}";
        }

        public string GetUserJson(string name)
        {
            return NameHasId(name, out int id)
                ? $"{{\"id\":{id},\"name\":\"{name}\"}}" : null;
        }

        public IUser Login(string username, string password)
        {
            if (NameHasId(username, out int id))
            {
                Logger.LogInformation("Login succeeded for username {0}",
                    username);
                return new User(id) { Name = username };
            }
            else
            {
                Logger.LogError("Login failed: invalid username {0}", username);
                throw new AuthenticationException(
                    "Invalid username, must be 'user<id>'");
            }
        }

        private bool NameHasId(string name, out int id)
        {
            id = 0;
            return name.StartsWith("user")
                && int.TryParse(name.Substring(4), out id);
        }
    }
}
