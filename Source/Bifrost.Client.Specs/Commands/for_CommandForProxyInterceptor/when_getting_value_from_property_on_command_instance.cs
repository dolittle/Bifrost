using Machine.Specifications;

namespace Bifrost.Client.Specs.Commands.for_CommandForProxyInterceptor
{
    public class when_getting_value_from_property_on_command_instance : given.a_known_method
    {
        const string return_value = "Something";

        Establish context = () =>
        {
            method_name = "get_Something";
            command_mock.SetupGet(c => c.Something).Returns(return_value);
        };

        Because of = () => interceptor.OnIntercept(invocation_mock.Object);

        It should_return_the_value_from_the_property = () => invocation_mock.VerifySet(i => i.ReturnValue = return_value);
    }
}
