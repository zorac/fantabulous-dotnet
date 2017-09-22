namespace Fantabulous.User
{
    public class User : IUser
    {
        public int Id { get; }

        public string Name { get; set; }

        public User(int id)
        {
            Id = id;
        }
    }
}
