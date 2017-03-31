/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Microsoft.AspNetCore.Builder;

namespace Bifrost.Web.Configuration
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseBifrost(this IApplicationBuilder builder)
        {
            app.Map("/Bifrost/Application", a => a.Run(async (context) => await context.Response.WriteAsync("Hello from App")));

            return builder;
        }
    }
}