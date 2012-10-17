using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace Bifrost.Web.Applications
{
    public class AssetsRouteHandler : IRouteHandler
    {
        string _url;
        IHttpHandler _httpHandler;

        public AssetsRouteHandler(string url)
        {
            _url = url;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (_httpHandler == null)
                _httpHandler = new AssetsRouteHttpHandler(_url);

            return _httpHandler;
        }
    }
}
