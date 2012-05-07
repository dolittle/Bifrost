using System;
using Bifrost.Web;
using Bifrost.Web.Pipeline;

namespace Bifrost.Configuration
{
	public static class ConfigurationExctensions
	{
		public static IConfigure AsSinglePageApplication(this IConfigure configure)
		{
			HttpModule.AddPipe(new SinglePageApplication());
			return configure;
		}
	}
}

