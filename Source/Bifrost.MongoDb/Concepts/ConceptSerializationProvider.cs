using System;
using Bifrost.Concepts;
using MongoDB.Bson.Serialization;

namespace Bifrost.MongoDB.Concepts
{
    public class ConceptSerializationProvider : IBsonSerializationProvider
    {
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type.IsConcept())
                return new ConceptSerializer();

            return null;
        }
    }
}
