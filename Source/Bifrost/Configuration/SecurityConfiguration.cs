/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Reflection;
using Bifrost.Execution;
using Bifrost.Security;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents the configuration for security
    /// </summary>
    public class SecurityConfiguration : ISecurityConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public void Initialize(IContainer container)
        {
            var typeDiscoverer = container.Get<ITypeDiscoverer>();

            var resolverType = typeof(DefaultPrincipalResolver);
            var resolverTypes = typeDiscoverer.FindMultiple<ICanResolvePrincipal>().Where(t => t.GetTypeInfo().Assembly != typeof(SecurityConfiguration).GetTypeInfo().Assembly);
            if (resolverTypes.Count() > 1) throw new MultiplePrincipalResolversFound();
            if (resolverTypes.Count() == 1) resolverType = resolverTypes.First();

            container.Bind<ICanResolvePrincipal>(resolverType);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
