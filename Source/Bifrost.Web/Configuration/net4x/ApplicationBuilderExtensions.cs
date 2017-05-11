/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
            builder.Map("/Bifrost/Application", a => a.Run(async (context) =>
            {
                var configuration = Configure.Instance.Container.Get<ConfigurationAsJavaScript>();
                context.Response.ContentType = "text/javascript";
                await context.Response.WriteAsync(configuration.AsString);
            }));
            return builder;
        }
    }
}