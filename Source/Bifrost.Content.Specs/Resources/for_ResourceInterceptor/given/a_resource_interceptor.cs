using Bifrost.Content.Resources;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Resources.for_ResourceInterceptor.given
{
    public class a_resource_interceptor
    {
        protected static Mock<IResourceResolver> resource_resolver_mock;
        protected static ResourceInterceptor interceptor;

        Establish context = () =>
        {
            resource_resolver_mock = new Mock<IResourceResolver>();
            interceptor = new ResourceInterceptor(resource_resolver_mock.Object);
        };
    }
}
