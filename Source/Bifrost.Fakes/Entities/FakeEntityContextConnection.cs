using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Entities;
using Bifrost.Execution;

namespace Bifrost.Fakes.Entities
{
    public class EntityContextConnection : IEntityContextConnection
    {

        IContainer _container;

        public void Initialize(IContainer container)
        {
            _container = container;
        }
    }
}
