/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;
using Raven.Imports.Newtonsoft.Json;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace Bifrost.RavenDB
{
    public class MethodInfoConverter : JsonConverter
    {
        public override bool CanRead { get { return true; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(MethodInfo).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var jsonObject = JObject.Load(reader);
                var className = jsonObject.Property("ClassName").Value.ToString();
                var name = jsonObject.Property("Name").Value.ToString();
                var assemblyName = jsonObject.Property("AssemblyName").Value.ToString();
                var signature = jsonObject.Property("Signature").Value.ToString();

                var start = signature.IndexOf('(') + 1;
                var end = signature.IndexOf(')');
                var parametersString = signature.Substring(start, end - start);
                var parameterTypeStrings = parametersString.Split(',');

                if (string.IsNullOrEmpty(className) ||
                    string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(assemblyName) ||
                    parameterTypeStrings == null)
                    throw new ArgumentException("Json object is not representing a MethodInfo");

                var typeName = string.Format("{0}, {1}", className, assemblyName);
                var type = Type.GetType(typeName);
                var method = type.GetMethods().Where(m => m.Name.Equals(name) && DoesSignatureMatch(m, parameterTypeStrings)).SingleOrDefault();
                return method;
            }
            catch {
                return null;
            }
        }

        bool DoesSignatureMatch(MethodInfo m, string[] parameterTypeStrings)
        {
            var parameters = m.GetParameters().Select(p => p.ParameterType.FullName).ToArray();
            if (parameters.Length == parameterTypeStrings.Length)
                for (var parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
                    if (parameterTypeStrings[parameterIndex] != parameters[parameterIndex])
                        return false;

            return true;
        }

        public override bool CanWrite { get { return false; } }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
