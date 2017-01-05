/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Concepts;
using Bifrost.Extensions;
using Raven.Imports.Newtonsoft.Json;

namespace Bifrost.RavenDB.Serialization
{
    public class ConceptConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsConcept();
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
