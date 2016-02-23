/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Text;
using Bifrost.Execution;
using Bifrost.Web.Commands;
using Bifrost.Web.Configuration;
using Bifrost.Web.Hubs;
using Bifrost.Web.Read;
using Bifrost.Web.Services;

namespace Bifrost.Web.Proxies
{
    [Singleton]
    public class GeneratedProxies
    {
        public string All { get; }

        public GeneratedProxies(
            CommandProxies commandProxies,
            QueryProxies queryProxies,
            ReadModelProxies readModelProxies,
            ServiceProxies serviceProxies,
            NamespaceConfigurationProxies namespaceConfigurationProxies,
            HubProxies hubProxies,
            ITypeDiscoverer typeDiscoverer,
            IContainer container)
        {
            var builder = new StringBuilder();
            builder.Append(commandProxies.Generate());
            builder.Append(readModelProxies.Generate());
            builder.Append(queryProxies.Generate());
            builder.Append(serviceProxies.Generate());
            builder.Append(namespaceConfigurationProxies.Generate());
            builder.Append(hubProxies.Generate());

            var generatorTypes = typeDiscoverer.FindMultiple<IProxyGenerator>().Where(t => !t.Namespace.StartsWith("Bifrost"));
            foreach (var generatorType in generatorTypes)
            {
                var generator = container.Get(generatorType) as IProxyGenerator;
                builder.Append(generator.Generate());
            }

            All = builder.ToString();
        }
    }
}
