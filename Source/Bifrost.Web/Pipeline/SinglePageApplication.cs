using System.IO;
using System.Web.Routing;

namespace Bifrost.Web.Pipeline
{
	public class SinglePageApplication : IPipe
	{
		public void Before (IWebContext webContext)
		{
			if( !HasExtension(webContext) &&
			    !HasRoute(webContext))
				webContext.RewritePath("/index.html");
		}

		public void After (IWebContext webContext)
		{
		}
		
		
		bool HasRoute(IWebContext webContext)
		{
			var path = StripLeadingSlashIfAny(webContext.Request.Path);
			foreach( var route in webContext.Routes ) 
			{
				if( route is Route )
				{
					var actualRoute = route as Route;
					var routePath = StripLeadingSlashIfAny(actualRoute.Url);
					if( routePath.StartsWith(path) ||
					    path.StartsWith(path))
						return true;
				}
			}
			return false;
		}
		
		
		bool HasExtension(IWebContext webContext)
		{
			var path = webContext.Request.Path;
			if( path.Length > 0 ) 
			{
				if( path.StartsWith("/") ) 
				{
					var extension = Path.GetExtension(path);					
					if( !string.IsNullOrEmpty(extension) ) 
						return true;
				}
			}
			return false;
		}
		
		string StripLeadingSlashIfAny(string path)
		{
			if( path.Length > 0 && path.StartsWith("/") ) 
				path = path.Substring(1);
			
			return path;
		}
	}
}

