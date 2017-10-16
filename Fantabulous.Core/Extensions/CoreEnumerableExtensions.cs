using Fantabulous.Core.Entities;
using Fantabulous.Core.Models;

namespace System.Collections.Generic
{
    public static class CoreEnumerableExtensions
    {
        /// <summary>
        /// Convert a sequence of TypeChildren into a Dictionary of child IDs
        /// by type.
        /// </summary>
        /// <param name="data">
        /// A sequence to convert
        /// </param>
        /// <returns>
        /// A dictionary mapping types to sequences of child IDs, possibly empty
        /// </returns>
        public static IDictionary<TType,IEnumerable<long>> ToDictionary<TType,TChild>(
            this IEnumerable<TypeChildren<TType,TChild>> data)
            where TChild : HasId
        {
            var result = new Dictionary<TType,IEnumerable<long>>();

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
