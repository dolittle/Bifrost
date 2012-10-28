using System.Web.Routing;
namespace Bifrost.Web
{
	public interface IWebContext
	{
		IWebRequest Request { get; }
		RouteCollection Routes { get; }
		void RewritePath(string path);
	}
}

