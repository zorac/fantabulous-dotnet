using System.Collections.Generic;

namespace Fantabulous.Core.Entities
{
    /// <summary>
    /// A series of works.
    /// </summary>
    /// <inheritDoc/>
    public class Series : HasName
    {
        /// <summary>
        /// The IDs of the works in this series, in order.
        /// </summary>
        public IEnumerable<long> WorkIds { get; set; }
    }
}
