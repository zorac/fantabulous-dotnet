using System;
using System.Threading.Tasks;

using Fantabulous.Core.Models;
using Fantabulous.Core.Services;

namespace Microsoft.AspNetCore.Http
{
    public static class FantabulousSessionSessionExtensions
    {
        private const string USER_ID = "user_id";
        private const string USER_NAME = "user_name";

        public static void Login(this ISession session, User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            session.SetInt32(USER_ID, (int)user.Id);
            session.SetString(USER_NAME, user.Name);
        }

        public static async Task<bool> IsLoggedInAsync(this ISession session)
        {
            await session.LoadAsync();

            return session.GetInt32(USER_ID) != null;
        }

        public static void Logout(this ISession session)
        {
            session.Remove(USER_ID);
            session.Remove(USER_NAME);
        }

        public static async Task<long> GetUserIdAsync(this ISession session)
        {
            await session.LoadAsync();

            return (long)session.GetInt32(USER_ID);
        }

        public static async Task<string> GetUserNameAsync(this ISession session)
        {
            await session.LoadAsync();

            return session.GetString(USER_NAME);
        }

        public static async Task<User> GetUserAsync(
            this ISession session,
            IUserService service)
        {
            await session.LoadAsync();

            var id = session.GetInt32(USER_ID);

            if (id == null) return null;

            return await service.GetUserAsync((long)id);
        }
    }
}
