using Bifrost.Commands;
using Machine.Specifications;
using SecurityDescriptor = Bifrost.Security.SecurityDescriptor;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(SecurityDescriptor))]
    public class when_checking_can_authorize_on_command_in_secured_namespace : given.a_configured_security_descriptor
    {
        static bool can_authorize;

        Because of = () => can_authorize = security_descriptor.CanAuthorize<HandleCommand>(command_that_has_namespace_rule);

        It should_be_able_to_authorize = () => can_authorize.ShouldBeTrue();
    }
}