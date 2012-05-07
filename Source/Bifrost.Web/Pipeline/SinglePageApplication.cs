using System.IO;

namespace Bifrost.Web.Pipeline
{
	public class SinglePageApplication : IPipe
	{
		public void Before (IWebContext webContext)
		{
			if( !HasExtension(webContext))
				webContext.RewritePath("/index.html");
			
		}

		public void After (IWebContext webContext)
		{
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
	}
}

