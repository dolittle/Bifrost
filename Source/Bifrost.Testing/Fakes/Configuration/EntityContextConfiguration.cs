using System;
using Bifrost.Configuration;

namespace Bifrost.Testing.Fakes.Configuration
{
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        public Type EntityContextType { get; set; }

        public Bifrost.Entities.IEntityContextConnection Connection { get; set; }
    }
}
