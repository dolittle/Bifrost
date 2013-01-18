using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    public class when_securing_namespace_from_type
    {
        static SecurityDescriptor security_descriptor;
        static ISecurable  securable;

        Establish context = () => security_descriptor = new SecurityDescriptor();

        Because of = () => securable = security_descriptor.ForNamespaceOf<when_securing_namespace_from_type>();

        It should_return_a_namespace_securable = () => securable.ShouldBeOfType<NamespaceSecurable>();
        It should_hold_the_name_of_the_namespace = () => ((NamespaceSecurable)securable).Namespace.ShouldEqual(typeof(when_securing_namespace_from_type).Namespace);
    }
}
