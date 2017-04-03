/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Bifrost.Configuration;
using Bifrost.Web.Assets;
using Bifrost.Web.Commands;
using Bifrost.Web.Proxies;
using Bifrost.Web.Read;
using Bifrost.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Bifrost.Web.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddBifrost(this IServiceCollection serviceCollection)
        {
            Configure.DiscoverAndConfigure();
        }

        public static IApplicationBuilder UseBifrost(this IApplicationBuilder builder, IHostingEnvironment env)
        {
            builder.Map("/Bifrost/Application", a => a.Run(Application));
            builder.Map("/Bifrost/Proxies", a => a.Run(Proxies));
            builder.Map("/Bifrost/Security", a => a.Run(SecurityProxies));
            builder.Map("/Bifrost/AssetsManager", a => a.Run(AssetsManager));

            var routeBuilder = new RouteBuilder(builder);
            routeBuilder.MapService<CommandCoordinatorService>("Bifrost/CommandCoordinator");
            routeBuilder.MapService<CommandSecurityService>("Bifrost/CommandSecurity");
            routeBuilder.MapService<QueryService>("Bifrost/Query");
            routeBuilder.MapService<ReadModelService>("Bifrost/ReadModel");

            var routes = routeBuilder.Build();
            builder.UseRouter(routes);

            return builder;
        }

        static async Task Application(HttpContext context)
        {
            var configuration = Configure.Instance.Container.Get<ConfigurationAsJavaScript>();
            context.Response.ContentType = "text/javascript";
            await context.Response.WriteAsync(configuration.AsString);
        }


        static async Task Proxies(HttpContext context)
        {
            var proxies = Configure.Instance.Container.Get<GeneratedProxies>();
            context.Response.ContentType = "text/javascript";
            await context.Response.WriteAsync(proxies.All);
        }

        static async Task SecurityProxies(HttpContext context)
        {
            var proxies = Configure.Instance.Container.Get<CommandSecurityProxies>();
            context.Response.ContentType = "text/javascript";
            await context.Response.WriteAsync(proxies.Generate());
        }

        static async Task AssetsManager(HttpContext context)
        {
            var assetsManager = Configure.Instance.Container.Get<IAssetsManager>();
            context.Response.ContentType = "text/javascript";

            IEnumerable<string> assets = new string[0];
            if (context.Request.Query.ContainsKey("extension"))
            {
                var extension = context.Request.Query["extension"];
                assets = assetsManager.GetFilesForExtension(extension);
                if (context.Request.Query.ContainsKey("structure"))
                    assets = assetsManager.GetStructureForExtension(extension);
            }
            var serialized = JsonConvert.SerializeObject(assets);
            await context.Response.WriteAsync(serialized);
        }
    }
}