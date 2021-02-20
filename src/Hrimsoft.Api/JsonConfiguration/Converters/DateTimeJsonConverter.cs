using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hrimsoft.Api
{
    /// <summary>
    /// Converts date time to json cutting micro and nano seconds
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.GetDateTime();

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var stringValue = value.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            switch (value.Kind)
            {
                case DateTimeKind.Unspecified:
                    break;
                case DateTimeKind.Utc:
                    stringValue += 'Z';
                    break;
                case DateTimeKind.Local:
                    var offset = value - value.ToUniversalTime();
                    var sign   = offset.TotalSeconds > 0 ? '+' : '-';
                    stringValue =$"{stringValue}{sign}{offset.Hours:d2}:{offset.Minutes:d2}";
                    break;
            }
            writer.WriteStringValue(stringValue);
        }
    }
}