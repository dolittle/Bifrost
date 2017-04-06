/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Extensions;
using Newtonsoft.Json;

namespace Bifrost.JSON.Concepts
{
    /// <summary>
    /// Represents a <see cref="JsonConverter"/> that can serialize and deserialize a <see cref="IDictionary{TKey, TValue}">dictionary</see> of <see cref="ConceptAs{T}"/>
    /// </summary>
    public class ConceptDictionaryConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            if (objectType.HasInterface(typeof(IDictionary<,>)) && objectType.GetTypeInfo().IsGenericType ) 
            {
                var keyType = objectType.GetTypeInfo().GetGenericArguments()[0].GetTypeInfo().BaseType;
                if (keyType.GetTypeInfo().IsGenericType)
                {
                    var genericArgumentType = keyType.GetTypeInfo().GetGenericArguments()[0];
                    var isConcept = typeof(ConceptAs<>).MakeGenericType(genericArgumentType).GetTypeInfo().IsAssignableFrom(keyType);
                    return isConcept;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var keyType = objectType.GetTypeInfo().GetGenericArguments()[0];
            var keyValueType = keyType.GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments()[0];
            var valueType = objectType.GetTypeInfo().GetGenericArguments()[1];
            var intermediateDictionaryType = typeof(Dictionary<,>).MakeGenericType(typeof(string), valueType);
            var intermediateDictionary = (IDictionary)Activator.CreateInstance(intermediateDictionaryType);
            serializer.Populate(reader, intermediateDictionary);

            var valueProperty = keyType.GetTypeInfo().GetProperty("Value");
            var finalDictionary = (IDictionary)Activator.CreateInstance(objectType);
            foreach (DictionaryEntry pair in intermediateDictionary)
            {
                object value;
                if (keyValueType == typeof(Guid))
                    value = Guid.Parse(pair.Key.ToString());
                else
                    value = Convert.ChangeType(pair.Key, keyValueType, null);

                var key = Activator.CreateInstance(keyType);
                valueProperty.SetValue(key, value, null);
                finalDictionary.Add(key, pair.Value);
            }
            return finalDictionary;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dictionary = value as IDictionary;

            var objectType = value.GetType();
            var keyType = objectType.GetTypeInfo().GetGenericArguments()[0];
            var keyValueType = keyType.GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments()[0];
            var valueType = objectType.GetTypeInfo().GetGenericArguments()[1];
            var intermediateDictionaryType = typeof(Dictionary<,>).MakeGenericType(typeof(string), valueType);
            var intermediateDictionary = (IDictionary)Activator.CreateInstance(intermediateDictionaryType);
            var valueProperty = keyType.GetTypeInfo().GetProperty("Value");

            foreach (DictionaryEntry pair in dictionary)
            {
                var keyValue = valueProperty.GetValue(pair.Key, null).ToString();
                intermediateDictionary[keyValue] = pair.Value;
            }

            writer.WriteValue(intermediateDictionary);
        }
    }
}
