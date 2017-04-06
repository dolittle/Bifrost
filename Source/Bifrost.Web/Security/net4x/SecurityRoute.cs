/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Web.Routing;

namespace Bifrost.Web.Security
{
    public class SecurityRoute : Route
    {
        const string _url = "Bifrost/Security";

        public SecurityRoute()
            : base(_url, new SecurityRouteHandler())
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
