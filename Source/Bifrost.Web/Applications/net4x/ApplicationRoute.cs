/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using System.Web.Routing;

namespace Bifrost.Web.Applications
{
    public class ApplicationRoute : Route
    {
        const string UnmatchedPathSegment = "{*pathInfo}";

        public ApplicationRoute(string url, Assembly assembly)
            : base(string.Format("{0}/{1}", url, UnmatchedPathSegment), new ApplicationRouteHandler(url, assembly))
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
