#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
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
using System.IO;
using Bifrost.Execution;
using Bifrost.Extensions;
using Newtonsoft.Json;
using System.Collections;

namespace Bifrost.Serialization
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
            /*
			var serializer = CreateSerializer(options);
			using (var textReader = new StringReader(json))
			{
				using (var reader = new JsonTextReader(textReader))
				{
                    var instance = (T)CreateInstanceOf(typeof(T));
					serializer.Populate(reader, instance);
					return instance;
				}
			}*/
		}

		public object FromJson(Type type, string json, SerializationOptions options = null)
		{
			var serializer = CreateSerializer(options);
			using (var textReader = new StringReader(json))
			{
				using (var reader = new JsonTextReader(textReader))
				{
                    object instance;

                    if (type.IsValueType ||
                        type.HasInterface<IEnumerable>())
                        instance = serializer.Deserialize(reader, type);
                    else
                    {
                        instance = CreateInstanceOf(type);
                        serializer.Populate(reader, instance);
                    }
					return instance;
				}
			}
		}

		public void FromJson(object instance, string json, SerializationOptions options = null)
		{
			var serializer = CreateSerializer(options);
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
				var serializer = CreateSerializer(options);
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


        object CreateInstanceOf(Type type)
        {
            if (type.HasDefaultConstructor())
                return Activator.CreateInstance(type);
            else
                return _container.Get(type);
        }


        JsonSerializer CreateSerializer(SerializationOptions options)
		{
			var contractResolver = new SerializerContractResolver(_container, options);
			var serializer = new JsonSerializer
			                 	{
			                 		TypeNameHandling = TypeNameHandling.Auto,
									ContractResolver = contractResolver
			                 	};

            
			return serializer;
		}
	}
}