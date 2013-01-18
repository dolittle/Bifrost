using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    public class when_securing_type
    {
        static SecurityDescriptor security_descriptor;
        static ISecurable  securable;

        Establish context = () => security_descriptor = new SecurityDescriptor();

        Because of = () => securable = security_descriptor.For(typeof(when_securing_type_using_generics));

        It should_return_a_type_securable = () => securable.ShouldBeOfType<TypeSecurable>();
        It should_hold_the_type = () => ((TypeSecurable)securable).Type.ShouldEqual(typeof(when_securing_type_using_generics));
    }
}
