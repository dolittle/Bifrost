using System;
using System.Web;

namespace Bifrost.Web
{
	public class WebContext : IWebContext
	{
		HttpContext _actualHttpContext;
		
		public WebContext (HttpContext actualHttpContext)
		{
			_actualHttpContext = actualHttpContext;
			Request = new Request(actualHttpContext.Request);
		}

		public void RewritePath (string path)
		{
			_actualHttpContext.RewritePath(path);
		}

		public IWebRequest Request { get; private set; }
	}
}

