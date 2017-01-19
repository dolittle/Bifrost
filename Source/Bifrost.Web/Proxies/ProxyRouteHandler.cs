/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Web.Routing;
using System.Web;

namespace Bifrost.Web.Proxies
{
    public class ProxyRouteHandler : IRouteHandler
    {
        IHttpHandler _httpHandler;

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (_httpHandler == null)
                _httpHandler = new ProxyRouteHttpHandler();

            return _httpHandler;
        }
    }
}
