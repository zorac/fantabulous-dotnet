using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Types;

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
        public IEnumerable<TypeChildren<TType,TChild>> Get(long parentId)
        {
            return new PtcEnumerable(this, parentId);
        }

        private class PtcEnumerable
            : IEnumerable<TypeChildren<TType,TChild>>
            , IEnumerator<TypeChildren<TType,TChild>>
        {
            private ParentTypeChildrenReader<TParent,TType,TChild> Reader;
            private long ParentId;
            private bool Moved;

            object IEnumerator.Current => Current;
            public TypeChildren<TType,TChild> Current { get; set; }

            internal PtcEnumerable(
                ParentTypeChildrenReader<TParent,TType,TChild> reader,
                long parentId)
            {
                Reader = reader;
                ParentId = parentId;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<TypeChildren<TType,TChild>> GetEnumerator()
            {
                return this;
            }

            public bool MoveNext()
            {
                if (!Reader.More
                        || (Reader.Data.Current.ParentId != ParentId)) {
                    return false;
                } else if (!Moved || ((Reader.More = Reader.Data.MoveNext())
                        && (Reader.Data.Current.ParentId == ParentId))) {
                    Moved = true;
                    Current = Reader.Data.Current;
                    return true;
                } else {
                    Current = null;
                    return false;
                }
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                // Nothing to dispose of
            }
        }
    }
}
