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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bifrost.JSON.Serialization
{
    /// <summary>
    /// Represents a <see cref="ISerializer"/>
    /// </summary>
	public class Serializer : ISerializer
	{
		readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="Serializer"/>
        /// </summary>
        /// <param name="container">A <see cref="IContainer"/> used to create instances of types during serialization</param>
		public Serializer(IContainer container)
		{
			_container = container;
		}

#pragma warning disable 1591 // Xml Comments
        public T FromJson<T>(string json, SerializationOptions options = null)
		{
            return (T)FromJson(typeof(T), json, options);
		}

		public object FromJson(Type type, string json, SerializationOptions options = null)
		{
			var serializer = CreateSerializerForDeserialization(options);
			using (var textReader = new StringReader(json))
			{
				using (var reader = new JsonTextReader(textReader))
				{
                    object instance;
                    
                    if(type.IsConcept())
                    {
                        var genericArgumentType = type.BaseType.GetGenericArguments()[0];
                        var value = serializer.Deserialize(reader, genericArgumentType);
                        return ConceptFactory.CreateConceptInstance(type, value);
                    } 

                    if (type.IsValueType ||
                        type.HasInterface<IEnumerable>())
                        instance = serializer.Deserialize(reader, type);
                    else
                    {
                        instance = CreateInstanceOf(type, json);
                        serializer.Populate(reader, instance);
                    }
					return instance;
				}
			}
		}

		public void FromJson(object instance, string json, SerializationOptions options = null)
		{
			var serializer = CreateSerializerForDeserialization(options);
			using (var textReader = new StringReader(json))
			{
				using (var reader = new JsonTextReader(textReader))
				{
					serializer.Populate(reader, instance);
				}
			}
		}


		public string ToJson(object instance, SerializationOptions options = null)
		{
			using (var stringWriter = new StringWriter())
			{
				var serializer = CreateSerializerForSerialization(options);
				serializer.Serialize(stringWriter, instance);
				var serialized = stringWriter.ToString();
				return serialized;
			}
		}

		public IDictionary<string, object> GetKeyValuesFromJson(string json)
		{
			return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
		}
#pragma warning restore 1591 // Xml Comments


        object CreateInstanceOf(Type type, string json)
        {
            if (type.HasDefaultConstructor())
                return Activator.CreateInstance(type);
            else
            {
                if( DoesPropertiesMatchConstructor(type, json) ) 
                    return CreateInstanceByPropertiesMatchingConstructor(type, json);
                else
                    return _container.Get(type);
            }
        }


        bool DoesPropertiesMatchConstructor(Type type, string json)
        {
                var hash = JObject.Load(new JsonTextReader(new StringReader(json)));
                var constructor = type.GetNonDefaultConstructor();
                var parameters = constructor.GetParameters();
                var properties = hash.Properties();
                var matchingParameters = parameters.Where(cp => properties.Select(p=>p.Name.ToCamelCase()).Contains(cp.Name.ToCamelCase()));
                return matchingParameters.Count() == parameters.Length;
        }

        object CreateInstanceByPropertiesMatchingConstructor(Type type, string json)
        {
            var hash = JObject.Load(new JsonTextReader(new StringReader(json)));
            var properties = hash.Properties();

            var constructor = type.GetNonDefaultConstructor();

            var parameters = constructor.GetParameters();
            var parameterInstances = new List<object>();

            var toObjectMethod = typeof(JToken).GetMethod("ToObject", new Type[0]);

            foreach (var parameter in parameters)
            {
                var property = properties.Single(p => p.Name.ToCamelCase() == parameter.Name.ToCamelCase());
                var genericToObjectMethod = toObjectMethod.MakeGenericMethod(parameter.ParameterType);
                var parameterInstance = genericToObjectMethod.Invoke(property.Value, null);
                parameterInstances.Add(parameterInstance);
            }

            var instance = constructor.Invoke(parameterInstances.ToArray());
            return instance;
        }

        JsonSerializer CreateSerializerForDeserialization(SerializationOptions options)
        {
            return CreateSerializer(options, TypeNameHandling.Auto);
        }

        JsonSerializer CreateSerializerForSerialization(SerializationOptions options)
        {
            return CreateSerializer(options, 
                options == null ? TypeNameHandling.None :
                    options.IncludeTypeNames ?
                        TypeNameHandling.Auto : 
                        TypeNameHandling.None);
        }

        JsonSerializer CreateSerializer(SerializationOptions options, TypeNameHandling typeNameHandling)
		{
			var contractResolver = new SerializerContractResolver(_container, options);
            
			var serializer = new JsonSerializer
			                 	{
			                 		TypeNameHandling = typeNameHandling,
									ContractResolver = contractResolver,
			                 	};
            serializer.Converters.Add(new ConceptConverter());
            serializer.Converters.Add(new ConceptDictionaryConverter());

            
			return serializer;
		}
	}
}