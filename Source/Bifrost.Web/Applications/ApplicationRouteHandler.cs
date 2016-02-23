/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace Bifrost.Web.Applications
{
    public class ApplicationRouteHandler : IRouteHandler
    {
        readonly string _url;
        readonly Assembly _assembly;
        IHttpHandler _httpHandler;

        public ApplicationRouteHandler(string url, Assembly assembly)
        {
            _url = url;
            _assembly = assembly;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return _httpHandler ?? (_httpHandler = new ApplicationRouteHttpHandler(_url, _assembly));
        }
    }
}
