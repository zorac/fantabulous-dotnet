using System.Collections.Generic;
using System.Linq;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A sequence of child entity IDs.
    /// </summary>
    public class Children<TChild>
        where TChild: HasId
    {
        /// <summary>
        /// The child entity IDs.
        /// </summary>
        public IEnumerable<long> ChildIds { get; set; }

        /// <summary>
        /// A comma-separated string of child entity IDs.
        /// </summary>
        public string ChildIdString {
            get {
                return string.Join(",", ChildIds);
            }
            set {
                ChildIds = value.Split(',').Select(long.Parse);
            }
        }
    }
}
