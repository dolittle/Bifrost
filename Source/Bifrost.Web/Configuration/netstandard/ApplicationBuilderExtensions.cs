/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Microsoft.AspNetCore.Builder;
using Bifrost.Web.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Bifrost.Configuration;

namespace Bifrost.Web.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddBifrost(this IServiceCollection serviceCollection)
        {
            Configure.DiscoverAndConfigure();

        }

        public static IApplicationBuilder UseBifrost(this IApplicationBuilder builder)
        {
            builder.Map("/Bifrost/Application", a => a.Run(async (context) =>
            {
                var proxies = Configure.Instance.Container.Get<GeneratedProxies>();
                await context.Response.WriteAsync("Hello from App");
            }));
            return builder;
        }
}
}