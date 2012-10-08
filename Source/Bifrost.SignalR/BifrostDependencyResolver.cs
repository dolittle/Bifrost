using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using SignalR;

namespace Bifrost.SignalR
{
    public class BifrostDependencyResolver : DefaultDependencyResolver
    {
        IContainer _container;
        public BifrostDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            try
            {
                return _container.Get(serviceType);
            }
            catch
            {
                return base.GetService(serviceType);
            }
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.GetAll(serviceType).Concat(base.GetServices(serviceType));
            }
            catch
            {
                return base.GetServices(serviceType);
            }
        }
    }
}
