using Machine.Specifications;
using Bifrost.Web.Pipeline;
using Moq;

namespace Bifrost.Web.Specs.Pipeline.for_SinglePageApplication.given
{
	public class a_single_page_application_and_a_web_context
	{
		protected static Mock<IWebContext> web_context_mock;
		protected static Mock<IWebRequest> web_request_mock;
		protected static SinglePageApplication	single_page_application;
		
		Establish	context = () => {
			web_context_mock = new Mock<IWebContext>();
			web_request_mock = new Mock<IWebRequest>();
			web_context_mock.SetupGet (w => w.Request).Returns(web_request_mock.Object);
			single_page_application = new SinglePageApplication();	
		};
	}
}

