using System.Reflection;
using Bifrost.Commands;
using Castle.DynamicProxy;
using Machine.Specifications;
using Moq;

namespace Bifrost.Client.Specs.Commands.for_CommandForProxyInterceptor.given
{
    public class a_known_method : all_dependencies
    {
        protected static CommandForProxyInterceptor interceptor;
        protected static Mock<IInvocation> invocation_mock;
        protected static Mock<MethodInfo> method_mock;
        protected static Mock<IHoldCommandInstance> proxy_mock;
        protected static Mock<MyCommand> command_mock;
        protected static string method_name = "";

        Establish context = () =>
        {
            command_mock = new Mock<MyCommand>();

            interceptor = new CommandForProxyInterceptor(
                command_invocation_mock.Object,
                command_notify_data_error_info_mock.Object,
                command_process_handler_mock.Object
            );

            method_mock = new Mock<MethodInfo>();
            method_mock.Setup(m => m.Name).Returns(()=>method_name);

            invocation_mock = new Mock<IInvocation>();
            invocation_mock.SetupGet(i => i.Method).Returns(method_mock.Object);

            proxy_mock = new Mock<IHoldCommandInstance>();
            proxy_mock.Setup(p => p.CommandInstance).Returns(command_mock.Object);
            invocation_mock.SetupGet(i => i.Proxy).Returns(proxy_mock.Object);
        };
    }
}
