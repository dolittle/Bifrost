/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Extensions;
using Bifrost.Serialization;

namespace Bifrost.Web.Services
{
    public class RestServiceMethodInvoker : IRestServiceMethodInvoker
    {
        private readonly ISerializer _serializer;
        private readonly IJsonInterceptor _jsonInterceptor;

        public RestServiceMethodInvoker(ISerializer serializer, IJsonInterceptor jsonInterceptor)
        {
            _serializer = serializer;
            _jsonInterceptor = jsonInterceptor;
        }

        public string Invoke(string baseUrl, object instance, Uri uri, NameValueCollection inputParameters)
        {
            FilterInputParameters(inputParameters);

            var type = instance.GetType();
            var methodName = GetMethodNameFromUri(baseUrl, uri);
            ThrowIfMethodNameNotSpecified(methodName, instance, uri);
            ThrowIfMethodMissing(methodName, instance, uri);

            var method = type.GetMethod(methodName);
            ThrowIfParameterCountMismatches(method, type, uri, inputParameters);
            ThrowIfParameterMissing(method, type, uri, inputParameters);

            var values = GetParameterValues(inputParameters, method);
            var result = method.Invoke(instance, values);

            var serializedResult = _serializer.ToJson(result, SerializationOptions.CamelCase);

            serializedResult = _jsonInterceptor.Intercept(serializedResult);

            return serializedResult;
        }

        void FilterInputParameters(NameValueCollection inputParameters)
        {
            inputParameters.Remove("_");
            inputParameters.Remove("_q");
            inputParameters.Remove("_rm");
            inputParameters.Remove("_cmd");
        }

        object[] GetParameterValues(NameValueCollection inputParameters, MethodInfo method)
        {
            var values = new List<object>();
            var parameters = method.GetParameters();
            foreach (var parameter in parameters)
            {
                var parameterAsString = inputParameters[parameter.Name];
                values.Add(HandleValue(parameter, parameterAsString));
            }
            return values.ToArray();
        }

        string Unescape(string value)
        {
            if (value.StartsWith("\"")) value = value.Substring(1);
            if (value.EndsWith("\"")) value = value.Substring(0, value.Length - 1);

            return value;
        }



        object HandleValue(ParameterInfo parameter, string input)
        {            
            if (parameter.ParameterType == typeof(string))
                return input;

            input = Unescape(input);

            if (parameter.ParameterType.GetTypeInfo().IsValueType)
                return TypeDescriptor.GetConverter(parameter.ParameterType).ConvertFromInvariantString(input);

            if(parameter.ParameterType.IsConcept())
            {
                var genericArgumentType = parameter.ParameterType.GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments()[0];
                var value = input.ParseTo(genericArgumentType);
                return ConceptFactory.CreateConceptInstance(parameter.ParameterType, value);
            }

            input = _jsonInterceptor.Intercept(input);
            return _serializer.FromJson(parameter.ParameterType, input);
        }

        string GetMethodNameFromUri(string baseUrl, Uri uri)
        {
            var path = uri.AbsolutePath;
            if (path.StartsWith("/"))
                path = path.Substring(1);

            var segments = path.Split('/');
            if (segments.Length > 1)
                return segments[segments.Length-1];

            return string.Empty;
        }

        void ThrowIfParameterMissing(MethodInfo methodInfo, Type type, Uri uri, NameValueCollection inputParameters)
        {
            var parameters = methodInfo.GetParameters();
            foreach (var parameter in parameters)
                if (!inputParameters.AllKeys.Contains(parameter.Name))
                    throw new MissingParameterException(parameter.Name, type.Name, uri);
        }

        void ThrowIfParameterCountMismatches(MethodInfo methodInfo, Type type, Uri uri, NameValueCollection inputParameters)
        {
            var parameters = methodInfo.GetParameters();
            if( inputParameters.Count != parameters.Length )
                throw new ParameterCountMismatchException(uri, type.Name, inputParameters.Count, parameters.Length);
        }

        void ThrowIfMethodNameNotSpecified(string methodName, object instance, Uri uri)
        {
            if (string.IsNullOrEmpty(methodName))
                throw new MethodNotSpecifiedException(instance.GetType(), uri);
        }

        void ThrowIfMethodMissing(string methodName, object instance, Uri uri)
        {
            var method = instance.GetType().GetMethod(methodName);
            if (method == null)
                throw new MissingMethodException(string.Format("Missing method '{0}' for Uri '{1}'", methodName, uri));
        }
    }
}
