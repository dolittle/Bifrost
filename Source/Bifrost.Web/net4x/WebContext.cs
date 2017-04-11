/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Bifrost.Web
{
    public class WebContext : IWebContext
    {
        HttpContext _actualHttpContext;
        
        public WebContext (HttpContext actualHttpContext)
        {
            _actualHttpContext = actualHttpContext;
            Request = new Request(actualHttpContext.Request);
            Routes = RouteTable.Routes;
        }

        public void RewritePath (string path)
        {
            _actualHttpContext.RewritePath(path);
        }

        public IWebRequest Request { get; private set; }
        public RouteCollection Routes { get; private set; }


        public bool HasRouteForCurrentRequest
        {
            get 
            {
                var bundles = BundleTable.Bundles.GetRegisteredBundles();
                var currentRequestPath = _actualHttpContext.Server.MapPath(_actualHttpContext.Request.Path).ToLowerInvariant();
                if (bundles.Any(b => _actualHttpContext.Server.MapPath(b.Path).ToLowerInvariant() == currentRequestPath)) return true;
                return RouteTable.Routes.GetRouteData(new HttpContextWrapper(_actualHttpContext)) != null;
            }
        }
    }
}

