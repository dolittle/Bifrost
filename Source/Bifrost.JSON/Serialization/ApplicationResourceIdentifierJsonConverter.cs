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
            var identifierAsString = reader.ReadAsString();
            var identifier = _converter.FromString(identifierAsString);
            return identifier;
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
