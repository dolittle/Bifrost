/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Web;
using Bifrost.Configuration;
using Bifrost.Web.Commands;

namespace Bifrost.Web.Security
{
    public class SecurityRouteHttpHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var proxies = Configure.Instance.Container.Get<CommandSecurityProxies>();
            context.Response.ContentType = "text/javascript";
            context.Response.Write(proxies.Generate());
        }
    }
}
