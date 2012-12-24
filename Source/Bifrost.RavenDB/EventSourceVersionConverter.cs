using System;
using Bifrost.Events;
using Newtonsoft.Json;

namespace Bifrost.RavenDB
{
    public class EventSourceVersionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(EventSourceVersion).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return EventSourceVersion.FromCombined((double)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((EventSourceVersion)value).Combine());
        }
    }
}
