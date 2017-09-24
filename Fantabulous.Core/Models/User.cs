namespace Fantabulous.Core.Models
{
    public class User : HasName
    {
        public User(long id, string name) : base(id, name)
        {
        }
    }
}
