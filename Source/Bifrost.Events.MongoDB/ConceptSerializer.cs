/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Concepts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Bifrost.Events.MongoDB
{
    /// <summary>
    /// Represents an implementation of <see cref="IBsonSerializer"/> that knows how to 
    /// serialize and deserialize <see cref="ConceptAs{T}"/>
    /// </summary>
    public class ConceptSerializer : IBsonSerializer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConceptSerializer"/>
        /// </summary>
        /// <param name="conceptType"></param>
        public ConceptSerializer(Type conceptType)
        {
            if (!conceptType.IsConcept())
                throw new ArgumentException("Type is not a concept.", nameof(conceptType));

            ValueType = conceptType;
        }

        /// <summary>
        /// Gets the value type the serializer supports - our <see cref="ConceptAs{T}">concept</see>
        /// </summary>
        public Type ValueType { get; }


        /// <inheritdoc/>
        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            var actualType = args.NominalType;

            object value = null;

            var valueType = actualType.GetConceptValueType();
            if (valueType == typeof(Guid))
            {
                var binaryData = bsonReader.ReadBinaryData();
                value = binaryData.ToGuid();
            }
            else if (valueType == typeof(double))
                value = bsonReader.ReadDouble();
            else if (valueType == typeof(float))
                value = (float)bsonReader.ReadDouble();
            else if (valueType == typeof(Int32))
                value = bsonReader.ReadInt32();
            else if (valueType == typeof(Int64))
                value = bsonReader.ReadInt64();
            else if (valueType == typeof(bool))
                value = bsonReader.ReadBoolean();
            else if (valueType == typeof(string))
                value = bsonReader.ReadString();
            else if (valueType == typeof(decimal))
                value = decimal.Parse(bsonReader.ReadString());

            var concept = ConceptFactory.CreateConceptInstance(actualType, value);
            return concept;
        }

        /// <inheritdoc/>
        public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        {
            id = null;
            idGenerator = null;
            idNominalType = null;
            return false;
        }

        /// <inheritdoc/>
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var underlyingValue = value?.GetType().GetTypeInfo().GetProperty("Value").GetValue(value, null);
            var nominalType = args.NominalType;
            var underlyingValueType = nominalType.GetConceptValueType();

            var bsonWriter = context.Writer;

			if (underlyingValueType == typeof(Guid))
			{
				var guid = (Guid) (underlyingValue ?? default(Guid));
				var guidAsBytes = guid.ToByteArray();
				bsonWriter.WriteBinaryData(new BsonBinaryData(guidAsBytes, BsonBinarySubType.UuidLegacy, GuidRepresentation.CSharpLegacy));
			}
            else if (underlyingValueType == typeof(double))
                bsonWriter.WriteDouble((double) (underlyingValue ?? default(double)));
            else if (underlyingValueType == typeof(float))
                bsonWriter.WriteDouble((double) (underlyingValue ?? default(double)));
            else if (underlyingValueType == typeof(Int32))
                bsonWriter.WriteInt32((Int32) (underlyingValue ?? default(Int32)));
            else if (underlyingValueType == typeof(Int64))
                bsonWriter.WriteInt64((Int64) (underlyingValue ?? default(Int64)));
            else if (underlyingValueType == typeof(bool))
                bsonWriter.WriteBoolean((bool) (underlyingValue ?? default(bool)));
            else if (underlyingValueType == typeof(string))
                bsonWriter.WriteString((string) (underlyingValue ?? string.Empty));
            else if (underlyingValueType == typeof(decimal))
                bsonWriter.WriteString(underlyingValue?.ToString() ?? default(decimal).ToString());
        }
    }
}
