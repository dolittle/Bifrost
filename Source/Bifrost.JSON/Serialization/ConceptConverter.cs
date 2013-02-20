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
using Bifrost.Concepts;
using Bifrost.Extensions;
using Newtonsoft.Json;

namespace Bifrost.JSON.Serialization
{
    public class ConceptConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsConcept();
        }

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

        public override void WriteJson(JsonWriter writer, object concept, JsonSerializer serializer)
        {
            var value = concept.GetType().GetProperty("Value").GetValue(concept, null);
            writer.WriteValue(value);
        }
    }
}
