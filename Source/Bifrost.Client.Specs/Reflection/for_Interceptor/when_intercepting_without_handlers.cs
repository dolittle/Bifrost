using Castle.DynamicProxy;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Reflection.for_Interceptor
{
    public class when_intercepting_without_handlers
    {
        static Mock<IInvocation>    invocation_mock;
        static InterceptorWithoutHandlers interceptor;

        Establish context = () => 
        {
            invocation_mock = new Mock<IInvocation>();
            interceptor = new InterceptorWithoutHandlers();
        };

        Because of = () => interceptor.Intercept(invocation_mock.Object);

        It should_call_the_on_intercept_with_the_invocation = () => interceptor.invocation_passed_to_intercept.ShouldEqual(invocation_mock.Object);
    }
}
