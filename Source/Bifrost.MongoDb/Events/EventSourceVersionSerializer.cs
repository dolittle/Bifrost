using System;
using Bifrost.Events;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace Bifrost.MongoDB.Events
{
    public class EventSourceVersionSerializer : IBsonSerializer
    {
        public object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            var versionAsDouble = bsonReader.ReadDouble();
            var version = EventSourceVersion.FromCombined(versionAsDouble);
            return version;
        }

        public void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            var version = (EventSourceVersion)value;
            var versionAsDouble = version.Combine();
            bsonWriter.WriteDouble(versionAsDouble);
        }


        public object Deserialize(BsonReader bsonReader, Type nominalType, IBsonSerializationOptions options)
        {
            throw new NotImplementedException();
        }

        public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        {
            throw new NotImplementedException();
        }

        public void SetDocumentId(object document, object id)
        {
            throw new NotImplementedException();
        }
    }
}
