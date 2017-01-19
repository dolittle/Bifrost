/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;
using Castle.DynamicProxy;

namespace Bifrost.Content.Resources
{
    /// <summary>
    /// Represents an <see cref="IInterceptor"/> for intercepting properties in a class implementing <see cref="IHaveResources"/>
    /// </summary>
    [Singleton]
    public class ResourceInterceptor : IInterceptor
    {
        readonly IResourceResolver _resolver;

        /// <summary>
        /// Initializes a new instance of <see cref="ResourceInterceptor"/>
        /// </summary>
        /// <param name="resolver"></param>
        public ResourceInterceptor(IResourceResolver resolver)
        {
            _resolver = resolver;
        }

#pragma warning disable 1591 // Xml Comments
        public virtual void Intercept(IInvocation invocation)
        {
            var resourceName = string.Format("{0}.{1}", invocation.Method.DeclaringType.Name, invocation.Method.Name.Replace("get_", string.Empty));

            var value = _resolver.Resolve(resourceName);
            if( !string.IsNullOrEmpty(value) )
                invocation.ReturnValue = value;
            else
                invocation.Proceed();
        }
#pragma warning restore 1591 // Xml Comments

    }
}