using Bifrost.Entities;
using Bifrost.Execution;

namespace Bifrost.Testing.Fakes.Entities
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
