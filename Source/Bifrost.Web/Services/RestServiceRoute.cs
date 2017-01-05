/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Web.Routing;

namespace Bifrost.Web.Services
{
    public class RestServiceRoute : Route
    {
        const string UnmatchedPathSegment = "{*pathInfo}";

        public RestServiceRoute(Type type, string url)
            : base(string.Format("{0}/{1}", url, UnmatchedPathSegment), new RestServiceRouteHandler(type,url))
        {
        }


        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
