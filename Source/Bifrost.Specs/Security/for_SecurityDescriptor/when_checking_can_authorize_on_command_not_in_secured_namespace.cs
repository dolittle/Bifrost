using Bifrost.Commands;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_checking_can_authorize_on_command_not_in_secured_namespace : given.a_configured_security_descriptor
    {
        static bool can_authorize;

        Because of = () => can_authorize = security_descriptor.CanAuthorize<HandleCommand>(command_that_is_not_applicable);

        It should_be_able_to_authorize = () => can_authorize.ShouldBeFalse();
    }
}