using System;
using Bifrost.Web;
using Bifrost.Web.Pipeline;

namespace Bifrost.Configuration
{
	public static class WebConfigurationExtensions
	{
		public static IConfigure AsSinglePageApplication(this IConfigure configuration)
		{
			HttpModule.AddPipe(new SinglePageApplication());
			return configuration;
		}
	}
}

