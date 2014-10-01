using System.Text;
using Bifrost.Web.Proxies;

namespace Bifrost.Web.Hubs
{
    public class HubProxies : IProxyGenerator
    {
        public string Generate()
        {
            var result = new StringBuilder();

            return result.ToString();
        }
    }
}
