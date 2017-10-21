using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fantabulous.Core.Types
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class TagsByType
    {
        private static long[] Empty = new long[0];

        public IEnumerable<long> Warning { get; set; } = Empty;
        public IEnumerable<long> Fandom { get; set; } = Empty;
        public IEnumerable<long> Character { get; set; } = Empty;
        public IEnumerable<long> Ship { get; set; } = Empty;
        public IEnumerable<long> Generic { get; set; } = Empty;

        public IEnumerable<long> Get(TagType key)
        {
            switch (key)
            {
                case TagType.Warning:
                    return Warning ?? Empty;
                case TagType.Fandom:
                    return Fandom ?? Empty;
                case TagType.Character:
                    return Character ?? Empty;
                case TagType.Ship:
                    return Ship ?? Empty;
                case TagType.Generic:
                    return Generic ?? Empty;
                default:
                    throw new KeyNotFoundException();
            }
        }

        public void Add(TagType key, IEnumerable<long> value)
        {
            switch (key)
            {
                case TagType.Warning:
                    Warning = value;
                    break;
                case TagType.Fandom:
                    Fandom = value;
                    break;
                case TagType.Character:
                    Character = value;
                    break;
                case TagType.Ship:
                    Ship = value;
                    break;
                case TagType.Generic:
                    Generic = value;
                    break;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}
