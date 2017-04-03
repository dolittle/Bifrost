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
    /// <summary>
    /// Represents a <see cref="JsonConverter"/> that can serialize and deserialize <see cref="IApplicationResourceIdentifier"/>
    /// </summary>
    public class ApplicationResourceIdentifierJsonConverter : JsonConverter
    {
        IApplicationResourceIdentifierConverter _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationResourceIdentifierJsonConverter"/>
        /// </summary>
        /// <param name="converter"></param>
        public ApplicationResourceIdentifierJsonConverter(IApplicationResourceIdentifierConverter converter)
        {
            _converter = converter;
        }

        /// <inheritdoc/>
        public override bool CanRead { get { return true; } }

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IApplicationResourceIdentifier).GetTypeInfo().IsAssignableFrom(objectType);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var identifierAsString = reader.ReadAsString();
            var identifier = _converter.FromString(identifierAsString);
            return identifier;
        }

        /// <inheritdoc/>
        public override bool CanWrite { get { return true; } }

        /// <inheritdoc/>
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
