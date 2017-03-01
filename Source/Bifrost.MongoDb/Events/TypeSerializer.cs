/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;

namespace Bifrost.MongoDb.Events
{
	public class TypeSerializer : IBsonSerializer
	{
		public Type ValueType
		{
			get
			{
				return typeof(Type);
			}
		}
		public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var bsonReader = context.Reader;
			var typeName = bsonReader.ReadString();
			var type = Type.GetType(typeName);
			return type;
		}

		public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
		{
			var type = (Type)value;
			var bsonWriter = context.Writer;
			bsonWriter.WriteString(type.AssemblyQualifiedName);
		}
	}
}
