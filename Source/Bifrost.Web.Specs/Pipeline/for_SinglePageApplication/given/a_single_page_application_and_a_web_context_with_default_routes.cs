using Machine.Specifications;
using Bifrost.Web.Pipeline;
using Moq;
using System.Web.Routing;

namespace Bifrost.Web.Specs.Pipeline.for_SinglePageApplication.given
{
	public class a_single_page_application_and_a_web_context_with_default_routes
	{
		protected static Mock<IWebContext> web_context_mock;
		protected static Mock<IWebRequest> web_request_mock;
		protected static SinglePageApplication	single_page_application;
        protected static RouteCollection route_collection;
		
		Establish	context = () => {
            route_collection = new RouteCollection();
            route_collection.Add(new Route("", null));
			web_context_mock = new Mock<IWebContext>();
			web_request_mock = new Mock<IWebRequest>();
			web_context_mock.SetupGet (w => w.Request).Returns(web_request_mock.Object);
            web_context_mock.SetupGet(w => w.Routes).Returns(route_collection);
			single_page_application = new SinglePageApplication();	
		};
	}
}

