namespace Fantabulous.User
{
    public interface IUserService
    {
        IUser Login(string username, string password);
        IUser GetUser(int id);
        IUser GetUser(string name);
        string GetUserJson(int id);
        string GetUserJson(string name);
    }
}
