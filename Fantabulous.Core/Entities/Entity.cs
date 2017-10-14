using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fantabulous.Core.Entities
{
    /// <summary>
    /// Abstract root class for an entity.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public abstract class Entity
    {
    }
}
