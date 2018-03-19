using System;
using System.Collections.Generic;
using System.Text;
using iqoption.core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iqoption.core.Converters.JsonConverters
{
    public class UnixDateTimeJsonConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((DateTime)value - _epoch).TotalSeconds + "");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            return reader.Value.FromUnixToDateTime();
        }


      
    }

    
}
