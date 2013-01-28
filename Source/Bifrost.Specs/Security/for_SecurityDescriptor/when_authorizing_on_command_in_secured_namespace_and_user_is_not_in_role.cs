using System.Collections.Generic;
using System.Linq;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_authorizing_on_command_in_secured_namespace_and_user_is_not_in_role : given.a_configured_security_descriptor
    {
        static AuthorizationResult authorization_result;
        static IEnumerable<string> authorization_messages;

        Because of = () =>
            {
                authorization_result = security_descriptor.Authorize(command_that_has_namespace_rule);
                authorization_messages = authorization_result.GetFailedAuthorizationMessages();
            };

        It should_not_be_authorized = () => authorization_result.IsAuthorized.ShouldBeFalse();
        It should_indicate_that_the_user_is_not_in_the_required_role = () => authorization_messages.First().IndexOf(Testing.Fakes.Security.SecurityDescriptor.NAMESPACE_ROLE).ShouldBeGreaterThan(0);
        It should_indicate_the_secured_namespace  = () => authorization_messages.First().IndexOf(Testing.Fakes.Security.SecurityDescriptor.SECURED_NAMESPACE).ShouldBeGreaterThan(0);
    }
}