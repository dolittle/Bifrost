using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Configuration;

namespace Bifrost.Fakes.Configuration
{
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        public Type EntityContextType { get; set; }

        public Bifrost.Entities.IEntityContextConnection Connection { get; set; }
    }
}
