using System;
using Bifrost.Configuration;
using Bifrost.Entities;

namespace Bifrost.RavenDB.Embedded
{
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        public Type EntityContextType { get { return typeof(EntityContext<>); } }
        public IEntityContextConnection Connection { get; set; }
    }
}
