using Bifrost.Reflection;
using Castle.DynamicProxy;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Reflection.for_Interceptor
{
    public class when_intercepting_with_one_handler_that_can_handle
    {
        static Mock<IInvocation> invocation_mock;
        static Mock<ICanHandleInvocations> invocation_handler_mock;
        static InterceptorWithHandler interceptor;

        Establish context = () =>
        {
            invocation_mock = new Mock<IInvocation>();
            invocation_handler_mock = new Mock<ICanHandleInvocations>();
            invocation_handler_mock.Setup(i => i.CanHandle(invocation_mock.Object)).Returns(true);

            interceptor = new InterceptorWithHandler(invocation_handler_mock.Object);
        };

        Because of = () => interceptor.Intercept(invocation_mock.Object);

        It should_call_the_handler = () => invocation_handler_mock.Verify(i => i.Handle(invocation_mock.Object), Moq.Times.Once());
    }
}
