/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Events;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;

namespace Bifrost.MongoDb.Events
{
	public class EventSourceVersionSerializer : IBsonSerializer
	{
		public Type ValueType
		{
			get
			{
				return typeof(EventSourceVersion);
			}
		}

		public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var versionAsDouble = context.Reader.ReadDouble();
			var version = EventSourceVersion.FromCombined(versionAsDouble);
			return version;
		}

		public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
		{
			var version = (EventSourceVersion)value;
			var versionAsDouble = version.Combine();
			context.Writer.WriteDouble(versionAsDouble);
		}
	}
}
