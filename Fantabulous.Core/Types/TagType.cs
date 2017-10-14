using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fantabulous.Core.Types
{
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum TagType
    {
        Root,
        Warning,
        Fandom,
        Character,
        Ship,
        Generic
    }
}
