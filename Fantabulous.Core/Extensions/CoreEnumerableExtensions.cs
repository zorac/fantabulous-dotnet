using Fantabulous.Core.Entities;
using Fantabulous.Core.Models;
using Fantabulous.Core.Types;

namespace System.Collections.Generic
{
    public static class CoreEnumerableExtensions
    {
        /// <summary>
        /// Convert a sequence of TypeChildren into a dictionary of child IDs
        /// by type.
        /// </summary>
        /// <param name="data">
        /// A sequence to convert
        /// </param>
        /// <returns>
        /// A dictionary mapping types to sequences of child IDs, possibly empty
        /// </returns>
        public static TagsByType ToTagsByType(
            this IEnumerable<TypeChildren<TagType,Tag>> data)
        {
            var result = new TagsByType();

            foreach (var datum in data)
            {
                result.Add(datum.Type, datum.ChildIds);
            }

            return result;
        }

        /// <summary>
        /// Create a reder for a sequence of ParentChildren.
        /// </summary>
        /// <param name="data">
        /// A sequence
        /// </param>
        /// <returns>
        /// A reader for that sequence
        /// </returns>
        public static ParentChildrenReader<TParent,TChild> GetReader<TParent,TChild>(
            this IEnumerable<ParentChildren<TParent,TChild>> data)
            where TParent : HasId
            where TChild : HasId
        {
            return new ParentChildrenReader<TParent,TChild>(data);
        }

        /// <summary>
        /// Create a reder for a sequence of ParentTypeChildren.
        /// </summary>
        /// <param name="data">
        /// A sequence
        /// </param>
        /// <returns>
        /// A reader for that sequence
        /// </returns>
        public static ParentTypeChildrenReader<TParent,TType,TChild> GetReader<TParent,TType,TChild>(
            this IEnumerable<ParentTypeChildren<TParent,TType,TChild>> data)
            where TParent : HasId
            where TChild : HasId
        {
            return new ParentTypeChildrenReader<TParent,TType,TChild>(data);
        }
    }
}
