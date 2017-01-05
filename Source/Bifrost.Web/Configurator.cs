/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Web.Routing;
using Bifrost.Commands;
using Bifrost.Configuration;
using Bifrost.JSON.Concepts;
using Bifrost.JSON.Serialization;
using Bifrost.Web.SignalR;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Owin;

namespace Bifrost.Web
{
    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            configure.CallContext.WithCallContextTypeOf<WebCallContext>();
            ConfigureSignalR(configure);
        }

        void ConfigureSignalR(IConfigure configure)
        {
            var resolver = new BifrostDependencyResolver(configure.Container);

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new FilteredCamelCasePropertyNamesContractResolver(),
                Converters = { new ConceptConverter(), new ConceptDictionaryConverter() }
            };
            var jsonSerializer = JsonSerializer.Create(serializerSettings);
            resolver.Register(typeof(JsonSerializer), () => jsonSerializer);

            GlobalHost.DependencyResolver = resolver;

            var hubConfiguration = new HubConfiguration { Resolver = resolver };

            RouteTable.Routes.MapOwinPath("/signalr", a => a.RunSignalR(hubConfiguration));
            var route = RouteTable.Routes.Last();
            RouteTable.Routes.Remove(route);
            RouteTable.Routes.Insert(0, route);
        }
    }
}
