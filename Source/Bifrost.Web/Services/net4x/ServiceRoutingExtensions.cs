/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Web.Routing;
using Bifrost.Configuration;

namespace Bifrost.Web.Services
{
    public static class ServiceRoutingExtensions
    {
        const string PostFix = "Service";

        public static void AddService<T>(this RouteCollection routes, string url = null, bool removePostFix = true)
        {
            routes.AddService(typeof(T), url, removePostFix);
        }

        public static void AddService(this RouteCollection routes, Type service, string url = null, bool removePostFix = true)
        {
            url = url ?? (removePostFix ? RemovePostFix(service) : service.Name);

            //Configure.Instance.Container.Get<IRegisteredServices>().Register(service, url);
            routes.Add(new RestServiceRoute(service, url));
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