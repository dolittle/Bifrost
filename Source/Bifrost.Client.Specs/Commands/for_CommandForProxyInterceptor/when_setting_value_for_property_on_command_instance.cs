using Machine.Specifications;

namespace Bifrost.Client.Specs.Commands.for_CommandForProxyInterceptor
{
    public class when_setting_value_for_property_on_command_instance : given.a_known_method
    {
        const string value_to_set = "Something";

        Establish context = () =>
        {
            method_name = "set_Something";
            invocation_mock.SetupGet(i => i.Arguments).Returns(new[] { value_to_set });
        };

        Because of = () => interceptor.OnIntercept(invocation_mock.Object);

        It should_set_value_on_instance = () => command_mock.VerifySet(c => c.Something = value_to_set);
    }
}
