using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace Bifrost.Web.Applications
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
