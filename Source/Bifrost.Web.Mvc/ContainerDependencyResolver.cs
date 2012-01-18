using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bifrost.Execution;

namespace Bifrost.Web.Mvc
{
    public class ContainerDependencyResolver : IDependencyResolver
    {
        readonly IContainer _container;

        public ContainerDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.Get(serviceType, true);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAll(serviceType);
        }
    }
}
