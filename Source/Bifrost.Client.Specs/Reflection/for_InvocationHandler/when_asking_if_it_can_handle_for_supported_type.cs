using System.Reflection;
using Bifrost.Reflection;
using Castle.DynamicProxy;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Reflection.for_InvocationHandler
{
    public class when_asking_if_it_can_handle_for_supported_type
    {
        static InvocationHandler<Interface, Implementation> handler;
        static Mock<IInvocation> invocation_mock;
        static Mock<MethodInfo> method_mock;
        static bool result;

        Establish context = () =>
        {
            handler = new InvocationHandler<Interface, Implementation>(new Mock<Implementation>().Object);
            method_mock = new Mock<MethodInfo>();
            method_mock.SetupGet(m=>m.DeclaringType).Returns(typeof(Interface));
            invocation_mock = new Mock<IInvocation>();
            invocation_mock.SetupGet(i=>i.Method).Returns(method_mock.Object);
        };

        Because of = () => result = handler.CanHandle(invocation_mock.Object);

        It should_respond_with_being_able_to_handle = () => result.ShouldBeTrue();
    }
}
