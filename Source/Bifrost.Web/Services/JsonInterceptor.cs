/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Bifrost.Web.Services
{
    [Singleton]
    public class JsonInterceptor : IJsonInterceptor
    {
        private readonly IEnumerable<Type> _valueInterceptors;
        private readonly IContainer _container;

        public JsonInterceptor(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _container = container;
            _valueInterceptors = typeDiscoverer.FindMultiple(typeof(ICanInterceptValue<>));
        }

        public string Intercept(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return json;
            }

            var parsedJson = JsonConvert.DeserializeObject<dynamic>(json);

            ProcessElement(parsedJson);

            return JsonConvert.SerializeObject(parsedJson);
        }

        private void TraverseObject(JObject instance)
        {

            foreach (JProperty property in instance.Children().Where(t => t is JProperty).OfType<JProperty>())
            {
                ProcessElement(property.Value);
            }
        }

        private void TraverseArray(JArray array)
        {
            foreach (JToken item in array)
            {
                ProcessElement(item);
            }
        }

        private void ProcessElement(JToken element)
        {
            if (element is JObject)
            {
                TraverseObject(element as JObject);
            }
            else if (element is JArray)
            {
                TraverseArray(element as JArray);
            }
            else if (element is JValue)
            {
                InterceptValue(element as JValue);
            }
        }

        private void InterceptValue(JValue value)
        {
            if (value.Value == null)
            {
                return;
            }

            var valueInterceptorTypes = GetValueInterceptorsForType(value.Value.GetType());

            foreach (var valueInterceptorType in valueInterceptorTypes)
            {
                var valueInterceptor = _container.Get(valueInterceptorType);
                value.Value = valueInterceptorType.GetMethod("Intercept").Invoke(valueInterceptor, new[] { value.Value });
            }
        }

        private Type[] GetValueInterceptorsForType(Type valueType)
        {
            return _valueInterceptors.Where(t => t.GetInterfaces()
                                                   .First(i => i.GetGenericTypeDefinition() == typeof(ICanInterceptValue<>))
                                                   .GetGenericArguments()
                                                   .First() == valueType).ToArray();
        }


    }


}