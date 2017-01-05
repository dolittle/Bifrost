/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using System.Web.Routing;
using Bifrost.Web.Applications;

namespace System.Web.Routing
{
    public static class RouteExtensions
    {
        public static void AddApplicationFromAssembly(this RouteCollection routes, string url, Assembly assembly)
        {
            routes.Add(new ApplicationRoute(url, assembly));
        }
    }
}
