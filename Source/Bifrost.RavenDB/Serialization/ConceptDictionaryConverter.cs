#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using Bifrost.Concepts;
using Bifrost.Extensions;
using System.Collections;
using Raven.Imports.Newtonsoft.Json;

namespace Bifrost.RavenDB.Serialization
{
    public class ConceptDictionaryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType.HasInterface(typeof(IDictionary<,>)) && objectType.IsGenericType ) 
            {
                var keyType = objectType.GetGenericArguments()[0].BaseType;
                if (keyType.IsGenericType)
                {
                    var genericArgumentType = keyType.GetGenericArguments()[0];
                    if (genericArgumentType.HasInterface(typeof(IEquatable<>)))
                    {
                        var isConcept = typeof(ConceptAs<>).MakeGenericType(genericArgumentType).IsAssignableFrom(keyType);
                        return isConcept;
                    }
                }
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var keyType = objectType.GetGenericArguments()[0];
            var keyValueType = keyType.BaseType.GetGenericArguments()[0];
            var valueType = objectType.GetGenericArguments()[1];
            var intermediateDictionaryType = typeof(Dictionary<,>).MakeGenericType(typeof(string), valueType);
            var intermediateDictionary = (IDictionary)Activator.CreateInstance(intermediateDictionaryType);
            serializer.Populate(reader, intermediateDictionary);

            var valueProperty = keyType.GetProperty("Value");
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

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dictionary = value as IDictionary;

            var objectType = value.GetType();
            var keyType = objectType.GetGenericArguments()[0];
            var keyValueType = keyType.BaseType.GetGenericArguments()[0];
            var valueType = objectType.GetGenericArguments()[1];
            var intermediateDictionaryType = typeof(Dictionary<,>).MakeGenericType(typeof(string), valueType);
            var intermediateDictionary = (IDictionary)Activator.CreateInstance(intermediateDictionaryType);
            var valueProperty = keyType.GetProperty("Value");

            foreach (DictionaryEntry pair in dictionary)
            {
                var keyValue = valueProperty.GetValue(pair.Key, null).ToString();
                intermediateDictionary[keyValue] = pair.Value;
            }

            writer.WriteValue(intermediateDictionary);
        }
    }
}
