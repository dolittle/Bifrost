using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Commands.for_CommandForProxyInterceptor
{
    public class when_getting_instance : given.a_known_method
    {
        Establish context = () => method_name = "get_Instance";

        Because of = () => interceptor.OnIntercept(invocation_mock.Object);

        It should_return_the_command_instance = () => invocation_mock.VerifySet(i => i.ReturnValue = command_mock.Object);
    }
}
