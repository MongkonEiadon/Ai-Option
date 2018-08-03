using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iqoption.core.Converters.JsonConverters {
    public class UnixDateTimeJsonConverter : DateTimeConverterBase {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteRawValue(((DateTime) value).ToUnixTimeSecounds()
                .ToString()); //   ((DateTime)value - _epoch).TotalSeconds + "");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            if (reader.Value == null) {
                return null;
            }

            return reader.Value.FromUnixToDateTime();
        }
    }

    public class UnixSecondsDateTimeJsonConverter : DateTimeConverterBase {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteRawValue(((DateTime) value).ToUnixTimeSecounds()
                .ToString()); //   ((DateTime)value - _epoch).TotalSeconds + "");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            if (reader.Value == null) {
                return null;
            }

            return reader.Value.FromUnixSecondsToDateTime();
        }
    }

    public static class DateTimeExtensions {
        public static DateTime FromUnixToDateTime(this object This) {
            return DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(This)).DateTime.ToLocalTime();
        }

        public static DateTime FromUnixSecondsToDateTime(this object This) {
            return DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(This)).DateTime.ToLocalTime();
        }

        public static Int64 ToUnixTimeSecounds(this DateTime This) {
            return new DateTimeOffset(This).ToUnixTimeSeconds();
        }
    }
}