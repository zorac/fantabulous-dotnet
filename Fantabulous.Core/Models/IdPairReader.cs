using System.Collections.Generic;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A reader for ID pairs, which assumes that the keys are strictly in the
    /// order in which they will be read.
    /// </summary>
    public class IdPairReader<TParent,TChild>
        where TParent: HasId
        where TChild: HasId
    {
        private readonly IEnumerator<IdPair<TParent,TChild>> Enumerator;

        private bool HavePairs;

        /// <summary>
        /// Create a new ID pair reader.
        /// </summary>
        /// <param name="pairs">
        /// The pairs which are to be read
        /// </param>
        public IdPairReader(IEnumerable<IdPair<TParent,TChild>> pairs)
        {
            Enumerator = pairs.GetEnumerator();
            HavePairs = Enumerator.MoveNext();
        }

        /// <summary>
        /// Fetch an array of child entity IDs for a given parent entity ID. It
        /// is assumed the child IDs for all preceeding parent IDs have already
        /// been read.
        /// </summary>
        /// <param name="id">
        /// A parent entity ID
        /// </param>
        /// <returns>
        /// An array of child entity IDs, empty if none found
        /// </returns>
        public long[] GetChildIdsForParentId(long id)
        {
            if (!HavePairs || (Enumerator.Current.ParentId != id))
                return new long[0];

            var ids = new List<long>();

            do
            {
                ids.Add(Enumerator.Current.ChildId);
                HavePairs = Enumerator.MoveNext();
            }
            while (HavePairs && (Enumerator.Current.ParentId == id));

            return ids.ToArray();
        }
    }
}
