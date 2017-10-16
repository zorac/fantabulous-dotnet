using System;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A type and sequence of child entity IDs.
    /// </summary>
    public class TypeChildren<TType,TChild>
        : Children<TChild>
        where TChild: HasId
    {
        /// <summary>
        /// The type.
        /// </summary>
        public TType Type { get; set; }
    }
}
