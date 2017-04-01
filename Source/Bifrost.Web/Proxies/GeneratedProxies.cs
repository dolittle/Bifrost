/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Text;
using Bifrost.Execution;
using Bifrost.Web.Commands;
using Bifrost.Web.Configuration;
#if(NET461)
using Bifrost.Web.Hubs;
#endif
using Bifrost.Web.Read;
using Bifrost.Web.Services;

namespace Bifrost.Web.Proxies
{
    [Singleton]
    public class GeneratedProxies
    {
        public GeneratedProxies(
            CommandProxies commandProxies,
            CommandSecurityProxies commandSecurityProxies,
            QueryProxies queryProxies,
            ReadModelProxies readModelProxies,
            ServiceProxies serviceProxies,
            NamespaceConfigurationProxies namespaceConfigurationProxies,
#if(NET461)
            HubProxies hubProxies,
#endif
            ITypeDiscoverer typeDiscoverer,
            IContainer container)
        {
            var builder = new StringBuilder();
            builder.Append(commandProxies.Generate());
            builder.Append(commandSecurityProxies.Generate());
            builder.Append(readModelProxies.Generate());
            builder.Append(queryProxies.Generate());
            builder.Append(serviceProxies.Generate());
            builder.Append(namespaceConfigurationProxies.Generate());
#if(NET461)
            builder.Append(hubProxies.Generate());
#endif

            var generatorTypes = typeDiscoverer.FindMultiple<IProxyGenerator>().Where(t => !t.Namespace.StartsWith("Bifrost"));
            foreach (var generatorType in generatorTypes)
            {
                var generator = container.Get(generatorType) as IProxyGenerator;
                builder.Append(generator.Generate());
            }

            All = builder.ToString();
        }

        public string All { get; private set; }
    }
}
