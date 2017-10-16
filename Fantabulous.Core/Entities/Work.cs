using System.Collections.Generic;

using Fantabulous.Core.Types;

namespace Fantabulous.Core.Entities
{
    /// <summary>
    /// A fanwork.
    /// </summary>
    /// <inheritDoc/>
    public class Work : HasName
    {
        /// <summary>
        /// The IDs of the pseuds which are creators of this work, in display
        /// order.
        /// </summary>
        public IEnumerable<long> PseudIds { get; set; }

        /// <summary>
        /// The IDs of the tags attached to this work, in display order
        /// </summary>
        public IDictionary<TagType,IEnumerable<long>> TagIds { get; set; }

        /// <summary>
        /// The IDs of the series this work belongs to.
        /// </summary>
        public IEnumerable<long> SeriesIds { get; set; }

        /// <summary>
        /// The ID of the chpaters in this series, in order.
        /// </summary>
        public IEnumerable<long> ChapterIds { get; set; }
    }
}
