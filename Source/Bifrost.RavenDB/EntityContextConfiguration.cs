using System;
using Bifrost.Configuration;
using Bifrost.Entities;

namespace Bifrost.RavenDB
{
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        public EntityContextConfiguration()
        {
            EventsKeyGeneratorType = typeof(SequentialKeyGenerator);
        }

        public string Url { get; set; }
        public Type EventsKeyGeneratorType { get; set; }
        public Type EntityContextType { get { return typeof(EntityContext<>); } }
        public IEntityContextConnection Connection { get; set; }
    }
}
