using Bifrost.Commands;
using Bifrost.Reflection;
using Castle.DynamicProxy;
using Machine.Specifications;
using Moq;

namespace Bifrost.Client.Specs.Commands.for_CommandForProxies.given
{
    public class all_dependencies 
    {
        protected static Mock<IProxying> proxying_mock;
        protected static Mock<IProxyBuilder> proxy_builder_mock;
        protected static Mock<ICommandForProxyInterceptor> interceptor_mock;

        Establish context = () =>
        {
            proxying_mock = new Mock<IProxying>();
            proxy_builder_mock = new Mock<IProxyBuilder>();
            interceptor_mock = new Mock<ICommandForProxyInterceptor>();
        };
    }
}
