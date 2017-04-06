/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Concepts;
using Newtonsoft.Json;

namespace Bifrost.JSON.Concepts
{
    /// <summary>
    /// Implements a <see cref="JsonConverter"/> that deals with serializing and deserializing of <see cref="ConceptAs{T}"/>
    /// </summary>
    public class ConceptConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsConcept();
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //var instance = Activator.CreateInstance(objectType);
            //var genericArgumentType = objectType.BaseType.GetGenericArguments()[0];
            //var value = reader.Value;
            //if (genericArgumentType == typeof(Guid))
            //    value = Guid.Parse(reader.Value.ToString());

            //objectType.GetProperty("Value").SetValue(instance, value, null);
            //return instance;

            return ConceptFactory.CreateConceptInstance(objectType, reader.Value);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object concept, JsonSerializer serializer)
        {
            var value = concept.GetType().GetTypeInfo().GetProperty("Value").GetValue(concept, null);
            writer.WriteValue(value);
        }
    }
}
