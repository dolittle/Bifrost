/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Bifrost.Applications;
using Bifrost.Concepts;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.JSON.Concepts;
using Bifrost.JSON.Events;
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
        IContainer _container;
        IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;

        ConcurrentDictionary<ISerializationOptions, JsonSerializer> _cacheAutoTypeName;
        ConcurrentDictionary<ISerializationOptions, JsonSerializer> _cacheNoneTypeName;

        /// <summary>
        /// Initializes a new instance of <see cref="Serializer"/>
        /// </summary>
        /// <param name="container">A <see cref="IContainer"/> used to create instances of types during serialization</param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter"/> for converting string representations of <see cref="IApplicationResourceIdentifier"/></param>
        public Serializer(IContainer container, IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter)
        {
            _container = container;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _cacheAutoTypeName = new ConcurrentDictionary<ISerializationOptions, JsonSerializer>();
            _cacheNoneTypeName = new ConcurrentDictionary<ISerializationOptions, JsonSerializer>();
        }

#pragma warning disable 1591 // Xml Comments
        public T FromJson<T>(string json, ISerializationOptions options = null)
        {
            return (T)FromJson(typeof(T), json, options);
        }

        public object FromJson(Type type, string json, ISerializationOptions options = null)
        {
            var serializer = CreateSerializerForDeserialization(options);
            using (var textReader = new StringReader(json))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    object instance;
                    
                    if(type.IsConcept())
                    {
                        var genericArgumentType = type.GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments()[0];
                        var value = serializer.Deserialize(reader, genericArgumentType);
                        return ConceptFactory.CreateConceptInstance(type, value);
                    } 

                    if (type.GetTypeInfo().IsValueType ||
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

        public void FromJson(object instance, string json, ISerializationOptions options = null)
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

        public string ToJson(object instance, ISerializationOptions options = null)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = CreateSerializerForSerialization(options);
                serializer.Serialize(stringWriter, instance);
                var serialized = stringWriter.ToString();
                return serialized;
            }
        }

        public Stream ToJsonStream(object instance, ISerializationOptions options = null)
        {
            var serialized = ToJson(instance, options);

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(serialized);
            writer.Flush();
            stream.Position = 0;

            return stream;
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

            var toObjectMethod = typeof(JToken).GetTypeInfo().GetMethod("ToObject", new Type[0]);

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

        JsonSerializer CreateSerializerForDeserialization(ISerializationOptions options)
        {
            return RetrieveSerializer(options ?? SerializationOptions.Default, true);
        }

        JsonSerializer CreateSerializerForSerialization(ISerializationOptions options)
        {
            if (options == null)
            {
                options = SerializationOptions.Default;
            }

            return RetrieveSerializer(options, options.Flags.HasFlag(SerializationOptionsFlags.IncludeTypeNames));
        }

        JsonSerializer RetrieveSerializer(ISerializationOptions options, bool includeTypeNames)
        {
            if (includeTypeNames)
            {
                return _cacheAutoTypeName.GetOrAdd(options, _ => CreateSerializer(options, TypeNameHandling.Auto));
            }
            else
            {
                return _cacheNoneTypeName.GetOrAdd(options, _ => CreateSerializer(options, TypeNameHandling.None));
            }
        }

        JsonSerializer CreateSerializer(ISerializationOptions options, TypeNameHandling typeNameHandling)
        {
            var contractResolver = new SerializerContractResolver(_container, options);

            var serializer = new JsonSerializer
                                 {
                                     TypeNameHandling = typeNameHandling,
                                     ContractResolver = contractResolver,
                                 };
            serializer.Converters.Add(new ApplicationResourceIdentifierJsonConverter(_applicationResourceIdentifierConverter));
            serializer.Converters.Add(new ExceptionConverter());
            serializer.Converters.Add(new ConceptConverter());
            serializer.Converters.Add(new ConceptDictionaryConverter());
            serializer.Converters.Add(new EventSourceVersionConverter());
            
            return serializer;
        }
    }
}