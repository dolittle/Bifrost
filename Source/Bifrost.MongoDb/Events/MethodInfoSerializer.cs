/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;

namespace Bifrost.MongoDb.Events
{
	public class MethodInfoSerializer : IBsonSerializer
	{
		public Type ValueType
		{
			get
			{
				return typeof(MethodInfo);
			}
		}

		public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var bsonReader = context.Reader;
			bsonReader.ReadStartDocument();

			bsonReader.ReadName();
			var typeName = bsonReader.ReadString();
			bsonReader.ReadName();
			var methodSignature = bsonReader.ReadString();

			bsonReader.ReadEndDocument();

			var type = Type.GetType(typeName);
			if (type != null)
			{
				var method = type.GetMethods().Where(m => GetMethodSignature(m) == methodSignature).SingleOrDefault();
				return method;
			}

			return null;
		}

		public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
		{
			var method = (MethodInfo)value;
			var bsonWriter = context.Writer;

			bsonWriter.WriteStartDocument();
			bsonWriter.WriteName("Type");
			bsonWriter.WriteString(method.DeclaringType.AssemblyQualifiedName);
			bsonWriter.WriteName("Method");
			bsonWriter.WriteString(GetMethodSignature(method));

			bsonWriter.WriteEndDocument();
		}

		string GetMethodSignature(MethodInfo method)
		{
			var builder = new StringBuilder();
			builder.Append(method.Name);
			builder.Append("(");

			foreach (var parameter in method.GetParameters())
				builder.AppendFormat("{0} {1}", parameter.ParameterType.Name, parameter.Name);

			builder.Append(")");
			return builder.ToString();
		}


		
	}
}
