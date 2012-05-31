using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Specs.Pipeline.for_SinglePageApplication
{
	public class when_processing_before_with_a_request_with_extension : given.a_single_page_application_and_a_web_context
	{
		Establish context = () => web_request_mock.SetupGet(c=>c.Path).Returns("/something/going/on.html");
		
		Because of = () => single_page_application.Before(web_context_mock.Object);
		
		It should_not_redirect_anywhere = () => web_context_mock.Verify(w=>w.RewritePath(Moq.It.IsAny<string>()), Times.Never () );
	}
}

