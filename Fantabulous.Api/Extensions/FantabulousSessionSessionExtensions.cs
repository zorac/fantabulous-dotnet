using System;

using Fantabulous.User;

namespace Microsoft.AspNetCore.Http
{
    public static class FantabulousSessionSessionExtensions
    {
        private const string USER_ID = "user_id";
        private const string USER_NAME = "user_name";

        public static void Login(this ISession session, IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            session.SetInt32(USER_ID, user.Id);
            session.SetString(USER_NAME, user.Name);
        }

        public static bool IsLoggedIn(this ISession session)
        {
             return session.GetInt32(USER_ID) != null;
        }

        public static void Logout(this ISession session)
        {
            session.Remove(USER_ID);
            session.Remove(USER_NAME);
        }

        public static int GetUserId(this ISession session)
        {
             return (int)session.GetInt32(USER_ID);
        }

        public static string GetUserName(this ISession session)
        {
             return session.GetString(USER_NAME);
        }

        public static IUser GetUser(this ISession session, IUserService service)
        {
             return service.GetUser((int)session.GetInt32(USER_ID));
        }
    }
}
