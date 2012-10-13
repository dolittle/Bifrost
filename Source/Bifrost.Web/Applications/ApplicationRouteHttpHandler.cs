using System.Reflection;
using System.Web;

namespace Bifrost.Web.Applications
{
    public class ApplicationRouteHttpHandler : IHttpHandler
    {
        string _url;
        Assembly _assembly;

        public ApplicationRouteHttpHandler(string url, Assembly assembly)
        {
            _url = url;
            _assembly = assembly;
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
        }
    }
}
