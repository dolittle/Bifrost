/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Microsoft.AspNetCore.Routing;

namespace Bifrost.Web.Services
{
    public static class ServiceRoutingExtensions
    {
        const string PostFix = "Service";

        public static void MapService<T>(this RouteBuilder builder, string url = null, bool removePostFix = true)
        {
            builder.MapService(typeof(T), url, removePostFix);
        }

        public static void MapService(this RouteBuilder builder, Type service, string url = null, bool removePostFix = true)
        {
            url = url ?? (removePostFix ? RemovePostFix(service) : service.Name);

            var handler = new RestServiceRouteHttpHandler(service, $"{url}/{{*pathInfo}}");
            builder.MapRoute($"{url}/{{*pathInfo}}", handler.ProcessRequest);
        }

        static string RemovePostFix(this Type serviceType)
        {
            var name = serviceType.Name;
            if (name.EndsWith(PostFix))
                name = name.Substring(0, name.Length - PostFix.Length);

            return name;
        }
    }
}
