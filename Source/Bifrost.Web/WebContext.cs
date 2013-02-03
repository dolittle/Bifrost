using System;
using System.Web;
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
                return RouteTable.Routes.GetRouteData(new HttpContextWrapper(_actualHttpContext)) != null;
            }
        }
    }
}

