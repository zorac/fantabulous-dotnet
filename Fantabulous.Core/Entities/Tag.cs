using System.Collections.Generic;

using Newtonsoft.Json;

using Fantabulous.Core.Types;

namespace Fantabulous.Core.Entities
{
    /// <summary>
    /// A tag.
    /// </summary>
    /// <inheritDoc/>
    public class Tag : HasName
    {
        /// <summary>
        /// The type of tag this is.
        /// </summary>
        public TagType Type { get; set; }

        /// <summary>
        /// The unique ID of the tag for which this is an alias. A value of 0
        /// indicates that this is a canonical tag.
        /// </summary>
        public long AliasFor { get; set; }

        /// <summary>
        /// The unique IDs of any parent tags.
        /// </summary>
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public IEnumerable<long> ParentIds { get; set; }

        /// <summary>
        /// The unique IDs of any fandom tags.
        /// </summary>
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public IEnumerable<long> FandomIds { get; set; }

        /// <summary>
        /// The unique IDs of any character tags.
        /// </summary>
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public IEnumerable<long> CharacterIds { get; set; }
    }
}
