using System;

namespace Bifrost.Web
{
	public interface IPipe
	{
		void Before(IWebContext webContext);
		void After(IWebContext webContext);
	}
}

