using System.Collections.Generic;
using System.Linq;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A reader for a sequence of ParentTypeChildren objects. This assumes
    /// that the ordering of the elements in the input matches the order in
    /// which they will be retrieved.
    /// </summary>
    public class ParentTypeChildrenReader<TParent,TType,TChild>
        where TParent: HasId
        where TChild: HasId
    {
        private IEnumerator<ParentTypeChildren<TParent,TType,TChild>> Data;

        private bool More;

        /// <summary>
        /// Create a new reader.
        /// </summary>
        /// <param name="data">
        /// The data to read
        /// </param>
        public ParentTypeChildrenReader(
            IEnumerable<ParentTypeChildren<TParent,TType,TChild>> data)
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
        /// A dictionary mapping types to sequences of child IDs, possibly empty
        /// </returns>
        public IDictionary<TType,IEnumerable<long>> Get(long parentId)
        {
            var result = new Dictionary<TType,IEnumerable<long>>();

            while (More && (Data.Current.ParentId == parentId))
            {
                result.Add(Data.Current.Type, Data.Current.ChildIds);
                More = Data.MoveNext();
            }

            return result;
        }
    }
}
