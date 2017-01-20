/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Execution;
using Bifrost.Extensions;
using Castle.DynamicProxy;

namespace Bifrost.Content.Resources
{
    /// <summary>
    /// Represents a <see cref="IBindingConvention"/> that resolves anything implementing <see cref="IHaveResources"/>
    /// </summary>
    public class ResourceConvention : BaseConvention
    {
        readonly ProxyGenerator _proxyGenerator;

        /// <summary>
        /// Initializes a new instance of <see cref="ResourceConvention"/>
        /// </summary>
        public ResourceConvention()
        {
            _proxyGenerator = new ProxyGenerator();
        }

#pragma warning disable 1591 // Xml Comments
        public override bool CanResolve(IContainer container, Type service)
        {
            var hasIStrings = service.HasInterface<IHaveResources>();
                
            return hasIStrings;
        }

        public override void Resolve(IContainer container, Type service)
        {
            var interceptor = container.Get<ResourceInterceptor>();
            var proxy = _proxyGenerator.CreateClassProxy(service, interceptor);
            container.Bind(service,proxy);
        }
#pragma warning restore 1591 // Xml Comments
    }
}