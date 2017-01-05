/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Events;
using Raven.Imports.Newtonsoft.Json;

namespace Bifrost.RavenDB.Events
{
    public class EventSourceVersionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(EventSourceVersion).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return EventSourceVersion.FromCombined((double)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((EventSourceVersion)value).Combine());
        }
    }
}
