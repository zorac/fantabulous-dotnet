using Fantabulous.Core.Types;

namespace Fantabulous.Core.Entities
{
    /// <summary>
    /// A tag.
    /// </summary>
    /// <inheritDoc/>
    public class Tag : HasName
    {
        public TagType Type { get; set; }

        /// <summary>
        /// The unique ID of the tag for which this is an alias. A value of 0
        /// indicates that this is a canonical tag.
        /// </summary>
        /// <returns></returns>
        public long AliasFor { get; set; }
    }
}
