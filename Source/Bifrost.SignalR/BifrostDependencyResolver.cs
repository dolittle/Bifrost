using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Microsoft.AspNet.SignalR;

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
            if (!IsSignalRInternalType(serviceType) )
                return _container.Get(serviceType);

            return base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            if (!IsSignalRInternalType(serviceType) )
                return _container.GetAll(serviceType).Concat(base.GetServices(serviceType));

            return base.GetServices(serviceType);
        }


        bool IsSignalRInternalType(Type serviceType)
        {
            return serviceType.Namespace.StartsWith("SignalR");
        }

    }
}
