using System;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Resources.for_ResourceInterceptor
{
    public class when_intercepting_property_with_string_and_resolver_has_a_value : given.a_resource_interceptor
    {
        const string property_value = "Something";
        const string expected = "Something else";
        static FakeInvocation invocation;

        Establish context = () =>
        {
            invocation = new FakeInvocation();
            Action action = () => { int i = 0; i++; };
            invocation.Method = action.Method;
            invocation.ReturnValueToSetOnProceed = property_value;
            resource_resolver_mock.Setup(r => r.Resolve(Moq.It.IsAny<string>())).Returns(expected);
        };

        Because of = () => interceptor.Intercept(invocation);

        It should_have_value_from_resolver_as_return_value = () => invocation.ReturnValue.ShouldEqual(expected);
    }
}
