using System;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Resources.for_ResourceInterceptor
{
    public class when_intercepting_property_with_string_and_resolver_does_not_have_value : given.a_resource_interceptor
    {
        const string expected = "Something";
        static FakeInvocation    invocation;

        Establish context = () =>
        {
            invocation = new FakeInvocation();
            Action action = () => { int i = 0; i++; };
            invocation.Method = action.Method;
            invocation.ReturnValueToSetOnProceed = expected;
        };

        Because of = () => interceptor.Intercept(invocation);

        It should_have_value_from_property_as_return_value = () => invocation.ReturnValue.ShouldEqual(expected);
    }
}
