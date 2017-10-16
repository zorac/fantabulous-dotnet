using System.Collections.Generic;
using System.Linq;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A reader for a sequence of ParentChildren objects. This assumes
    /// that the ordering of the elements in the input matches the order in
    /// which they will be retrieved.
    /// </summary>
    public class ParentChildrenReader<TParent,TChild>
        where TParent: HasId
        where TChild: HasId
    {
        private IEnumerator<ParentChildren<TParent,TChild>> Data;

        private bool More;

        /// <summary>
        /// Create a new reader.
        /// </summary>
        /// <param name="data">
        /// The data to read
        /// </param>
        public ParentChildrenReader(
            IEnumerable<ParentChildren<TParent,TChild>> data)
        {
            Data = data.GetEnumerator();
            More = Data.MoveNext();
        }

        /// <summary>
        /// Read the value for a given parent ID.
        /// </summary>
        /// <param name="parentId">
        /// A parent ID
        /// </param>
        /// <returns>
        /// A sequence of child IDs, possibly empty
        /// </returns>
        public IEnumerable<long> Get(long parentId)
        {
            if (!More || (Data.Current.ParentId != parentId))
                return Enumerable.Empty<long>();

            var result = Data.Current.ChildIds;

            More = Data.MoveNext();

            return result;
        }
    }
}
