using System;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Services;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// Extensions to access data from a session.
    /// </summary>
    public static class ApiSessionExtensions
    {
        private const string USER_ID = "user_id";
        private const string USER_NAME = "user_name";

        /// <summary>
        /// Add a user as logged in to this session.
        /// </summary>
        /// <param name="user">
        /// A user
        /// </param>
        public static void Login(this ISession session, User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            session.SetInt32(USER_ID, (int)user.Id);
            session.SetString(USER_NAME, user.Name);
        }

        /// <summary>
        /// Check if this session is currently logged in.
        /// </summary>
        /// <returns>
        /// True iff the session is logged in.
        /// </returns>
        public static async Task<bool> IsLoggedInAsync(this ISession session)
        {
            await session.LoadAsync();

            return session.GetInt32(USER_ID) != null;
        }

        /// <summary>
        /// Log out any user from the current session.
        /// </summary>
        public static void Logout(this ISession session)
        {
            session.Remove(USER_ID);
            session.Remove(USER_NAME);
        }

        /// <summary>
        /// Fetch the unique ID of the logged-in user.
        /// </summary>
        /// <returns>
        /// The user ID, or null if not logged in
        /// </returns>
        public static async Task<long> GetUserIdAsync(this ISession session)
        {
            await session.LoadAsync();

            return (long)session.GetInt32(USER_ID);
        }

        /// <summary>
        /// Fetch the username of the logged-in user.
        /// </summary>
        /// <returns>
        /// The username, or null if not logged in
        /// </returns>
        public static async Task<string> GetUserNameAsync(this ISession session)
        {
            await session.LoadAsync();

            return session.GetString(USER_NAME);
        }

        /// <summary>
        /// Fetch the logged-in user.
        /// </summary>
        /// <returns>
        /// The user, or null if not logged in
        /// </returns>
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
