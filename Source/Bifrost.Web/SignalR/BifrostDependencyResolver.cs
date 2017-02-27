/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Microsoft.AspNet.SignalR;

namespace Bifrost.Web.SignalR
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
            var service = base.GetService(serviceType);
            if (service == null)
                try { service = _container.Get(serviceType); }
                catch { }

            return service;
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container.HasBindingFor(serviceType))
                return _container.GetAll(serviceType).Concat(base.GetServices(serviceType));

            return base.GetServices(serviceType);
        }

        
        bool IsSignalRInternalType(Type serviceType)
        {
            return serviceType.Namespace.StartsWith("Microsoft") || serviceType.Namespace.StartsWith("System") || serviceType.Namespace.StartsWith("Owin");
        }

    }
}
