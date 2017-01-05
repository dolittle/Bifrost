/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Web.Routing;

namespace Bifrost.Web.Configuration
{
    public class ConfigurationRoute : Route
    {
        const string ConfigurationUrl = "Bifrost/Application";

        public ConfigurationRoute()
            : base(ConfigurationUrl, new ConfigurationRouteHandler())
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
