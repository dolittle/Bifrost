using System.Globalization;
using System.Reflection;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Reflection.for_InvocationHandler
{
    public class when_handling_getting_property_value : given.an_invocation
    {
        Establish context = () => 
        {
            method_name = "get_Something";

            return_value = "Hello world";
        };

        Because of = () => handler.Handle(invocation_mock.Object);

        It should_invoke_the_method_on_the_implementation = () => method_mock.Verify(m => m.Invoke(implementation_mock.Object, Moq.It.IsAny<BindingFlags>(), Moq.It.IsAny<Binder>(), arguments, Moq.It.IsAny<CultureInfo>()), Moq.Times.Once());
        It should_pass_along_the_return_value_to_the_invocation = () => invocation_mock.VerifySet(i => i.ReturnValue = return_value);
    }
}
