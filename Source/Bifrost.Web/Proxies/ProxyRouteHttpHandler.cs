/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Web;
using Bifrost.Configuration;

namespace Bifrost.Web.Proxies
{
    public class ProxyRouteHttpHandler : IHttpHandler
    {
        GeneratedProxies _proxies;

        public ProxyRouteHttpHandler()
        {
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            if (_proxies == null) _proxies = Configure.Instance.Container.Get<GeneratedProxies>();
            context.Response.ContentType = "text/javascript";
            context.Response.Write(_proxies.All);
        }
    }
}
