/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Applications;
using Newtonsoft.Json;

namespace Bifrost.JSON.Serialization
{
    public class ApplicationResourceIdentifierJsonConverter : JsonConverter
    {
        IApplicationResourceIdentifierConverter _converter;

        public ApplicationResourceIdentifierJsonConverter(IApplicationResourceIdentifierConverter converter)
        {
            _converter = converter;
        }

        public override bool CanRead { get { return true; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IApplicationResourceIdentifier).GetTypeInfo().IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;

            /*
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
                var method = type.GetTypeInfo().GetMethods().Where(m => m.Name.Equals(name) && DoesSignatureMatch(m, parameterTypeStrings)).SingleOrDefault();
                return method;
            }
            catch {
                return null;
            }*/
        }

        public override bool CanWrite { get { return true; } }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var identifier = value as IApplicationResourceIdentifier;
            if( identifier != null )
            {
                var identifierAsString = _converter.AsString(identifier);
                writer.WriteValue(identifierAsString);
            }
        }
    }
}
