namespace Fantabulous.Core.Models
{
    public abstract class HasName : HasId
    {
        public string Name { get; }

        public HasName(long id, string name) : base(id)
        {
            Name = name;
        }
    }
}
