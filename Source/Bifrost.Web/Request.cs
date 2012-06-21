using System.Web;

namespace Bifrost.Web
{
	public class Request : IWebRequest
	{
		HttpRequest	_actualRequest;
		
		public Request(HttpRequest actualRequest)
		{
			_actualRequest = actualRequest;
		}
		
		public string Path { get { return _actualRequest.Path; } }
	}
}

