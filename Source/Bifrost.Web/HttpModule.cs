using System;
using System.Collections.Generic;
using System.Web;
using Bifrost.Web.Pipeline;

namespace Bifrost.Web
{
	public class HttpModule : IHttpModule
	{
		static List<IPipe>	_pipeline = new List<IPipe>();

		public static void AddPipe(IPipe pipe)
		{
			foreach( var existingPipe in _pipeline )
			{
				if( existingPipe.GetType () == pipe.GetType () )
					return;
			}
			_pipeline.Add (pipe);
		}
		
		
		
		public void Init (HttpApplication context)
		{
			context.AuthorizeRequest += AuthorizeRequest;
		}

		void AuthorizeRequest (object sender, EventArgs e)
		{
			var context = new WebContext(HttpContext.Current);
			foreach( var pipe in _pipeline )
				pipe.Before (context);
		}
		
		public void Dispose ()
		{
		}
	}
}

