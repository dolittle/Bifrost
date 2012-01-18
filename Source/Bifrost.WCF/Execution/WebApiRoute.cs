using System;
using System.ServiceModel.Activation;
using System.Web.Routing;

namespace Bifrost.WCF.Execution
{
    // Workaround for allowing mixing ASP.net MVC and ServiceRoutes 
    // From : http://codebetter.com/glennblock/2011/08/05/integrating-mvc-routes-and-web-api-routes-2/
    public class WebApiRoute : ServiceRoute
    {
        public WebApiRoute(string routePrefix, ServiceHostFactoryBase serviceHostFactory, Type serviceType)
            : base(routePrefix, serviceHostFactory, serviceType)
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
