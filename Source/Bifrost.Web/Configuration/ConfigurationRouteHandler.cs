/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Web;
using System.Web.Routing;

namespace Bifrost.Web.Configuration
{
    public class ConfigurationRouteHandler : IRouteHandler
    {
        IHttpHandler _httpHandler;

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return _httpHandler ?? (_httpHandler = new ConfigurationRouteHttpHandler());
        }
    }
}
