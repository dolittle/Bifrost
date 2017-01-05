/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace Bifrost.Web.Assets
{
    public class AssetManagerRouteHandler : IRouteHandler
    {
        string _url;
        IHttpHandler _httpHandler;

        public AssetManagerRouteHandler(string url)
        {
            _url = url;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (_httpHandler == null)
                _httpHandler = new AssetManagerRouteHttpHandler(_url);

            return _httpHandler;
        }
    }
}
