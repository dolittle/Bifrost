﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Web;
using System.Web.Routing;

namespace Bifrost.Web.Services
{
    public class RestServiceRouteHandler : IRouteHandler
    {
        Type _type;
        string _url;
        IHttpHandler _httpHandler;

        public RestServiceRouteHandler(Type type, string url)
        {
            _type = type;
            _url = url;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (_httpHandler == null)
                _httpHandler = new RestServiceRouteHttpHandler(_type, _url);

            return _httpHandler;
        }
    }
}
