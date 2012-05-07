namespace Bifrost.Web
{
	public interface IWebContext
	{
		IWebRequest Request { get; }
		void RewritePath(string path);
	}
}

