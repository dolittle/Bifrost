using System.Security.Principal;
using System.Threading;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(SecurityDescriptor))]
    public class when_authorizing_on_command_in_secured_namespace_and_user_is_in_role : given.a_configured_security_descriptor
    {
        static AuthorizationResult can_authorize;

        Establish context = () =>
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(""), new[] { Testing.Fakes.Security.SecurityDescriptor.NAMESPACE_ROLE });
            };

        Because of = () => can_authorize = security_descriptor.Authorize(command_that_has_namespace_rule);

        It should_be_authorized = () => can_authorize.IsAuthorized.ShouldBeTrue();
    }
}