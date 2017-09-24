namespace Fantabulous.Core.Models
{
    public abstract class HasId
    {
        public long Id { get; }

        public HasId(long id)
        {
            Id = id;
        }
    }
}
