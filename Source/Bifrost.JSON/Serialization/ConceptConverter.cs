using System;
using Bifrost.Concepts;
using Bifrost.Extensions;
using Newtonsoft.Json;

namespace Bifrost.JSON.Serialization
{
    public class ConceptConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if( objectType.BaseType.IsGenericType ) 
            {
                var genericArgumentType = objectType.BaseType.GetGenericArguments()[0];
                if( genericArgumentType.HasInterface(typeof(IEquatable<>)) ) 
                {
                    var isConcept = typeof(ConceptAs<>).MakeGenericType(genericArgumentType).IsAssignableFrom(objectType);
                    return isConcept;
                }
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var instance = Activator.CreateInstance(objectType);
            var genericArgumentType = objectType.BaseType.GetGenericArguments()[0];
            var value = reader.Value;
            if (genericArgumentType == typeof(Guid))
                value = Guid.Parse(reader.Value.ToString());

            objectType.GetProperty("Value").SetValue(instance, value, null);
            return instance;
        }

        public override void WriteJson(JsonWriter writer, object concept, JsonSerializer serializer)
        {
            var value = concept.GetType().GetProperty("Value").GetValue(concept, null);
            writer.WriteValue(value);
        }
    }
}
