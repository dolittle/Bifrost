using System;
using Bifrost.Concepts;
using Bifrost.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace Bifrost.MongoDB.Concepts
{
    public class ConceptSerializer : IBsonSerializer
    {
        public object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            object value = null;
            var valueType = actualType.GetConceptValueType();
            if (valueType == typeof(Guid))
            {
                var guidBytes = new byte[16];
                BsonBinarySubType subType;
                bsonReader.ReadBinaryData(out guidBytes, out subType);
                value = new Guid(guidBytes);
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
            
            var concept = ConceptFactory.CreateConceptInstance(actualType, value);
            return concept;
        }

        public object Deserialize(BsonReader bsonReader, Type nominalType, IBsonSerializationOptions options)
        {
            return null;
        }

        public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        {
            id = null;
            idGenerator = null;
            idNominalType = null;
            return false;
        }

        public void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            var underlyingValue = value.GetType().GetProperty("Value").GetValue(value, null);
            var underlyingValueType = underlyingValue.GetType();
            if (underlyingValueType == typeof(Guid))
            {
                var guid = (Guid)underlyingValue;
                var guidAsBytes = guid.ToByteArray();
                bsonWriter.WriteBinaryData(guidAsBytes, BsonBinarySubType.UuidLegacy, GuidRepresentation.CSharpLegacy);
            }
            else if (underlyingValueType == typeof(double))
                bsonWriter.WriteDouble((double)underlyingValue);
            else if (underlyingValueType == typeof(float))
                bsonWriter.WriteDouble((double)underlyingValue);
            else if (underlyingValueType == typeof(Int32))
                bsonWriter.WriteInt32((Int32)underlyingValue);
            else if (underlyingValueType == typeof(Int64))
                bsonWriter.WriteInt64((Int64)underlyingValue);
            else if (underlyingValueType == typeof(bool))
                bsonWriter.WriteBoolean((bool)underlyingValue);
            else if (underlyingValueType == typeof(string))
                bsonWriter.WriteString((string)underlyingValue);
        }

        public void SetDocumentId(object document, object id)
        {
        }
    }
}
