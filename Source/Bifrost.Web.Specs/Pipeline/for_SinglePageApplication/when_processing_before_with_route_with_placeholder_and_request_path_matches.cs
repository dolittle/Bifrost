using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using System.Web.Routing;

namespace Bifrost.Web.Specs.Pipeline.for_SinglePageApplication
{
    public class when_processing_before_with_route_with_placeholder_and_request_path_matches : given.a_single_page_application_and_a_web_context_with_default_routes
    {
        Establish context = () =>
        {
            route_collection.Add(new Route("something/{*pathInfo}", null));
            web_request_mock.SetupGet(c => c.Path).Returns("/something/going/on");
        };

        Because of = () => single_page_application.Before(web_context_mock.Object);

        It should_not_redirect_anywhere = () => web_context_mock.Verify(w => w.RewritePath(Moq.It.IsAny<string>()), Times.Never());
    }
}
