using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hrimsoft.Api
{
    /// <summary>
    /// Creates common json settings for serialization and deserialization processes
    /// </summary>
    public interface IHrimsoftJsonSerializeOptionsFactory
    {
        /// <summary>
        /// Creates common json settings for serialization and deserialization processes
        /// </summary>
        /// <returns>Returns json settings</returns>
        JsonSerializerOptions Get();
    }

    /// <summary>
    /// Factory that creates json settings for serialization and deserialization processes
    /// </summary>
    public class HrimsoftJsonSerializeOptionsFactory : IHrimsoftJsonSerializeOptionsFactory
    {
        /// <summary>
        /// Creates common json settings for serialization and deserialization processes
        /// </summary>
        /// <returns>Returns json settings</returns>
        public JsonSerializerOptions Get()
        {
            var options = new JsonSerializerOptions()
                          {
                              DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                              PropertyNamingPolicy   = new SnakeCaseNamingPolicy()
                          };
            options.Converters.Add(new DateTimeJsonConverter());
            options.Converters.Add(new JsonStringEnumConverter(new SnakeCaseNamingPolicy()));
            return options;
        }
    }
}