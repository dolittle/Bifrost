using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_authorizing_on_command_in_secured_namespace_and_user_is_not_in_role : given.a_configured_security_descriptor
    {
        static AuthorizeDescriptorResult authorize_descriptor_result;
        static IEnumerable<string> authorization_messages;

        Establish context = () =>
        {
            GenericPrincipal.ClaimsPrincipalSelector = () =>  new GenericPrincipal(new GenericIdentity(""), new string[0]);
        };

        Because of = () =>
            {
                authorize_descriptor_result = security_descriptor.Authorize(command_that_has_namespace_rule);
                authorization_messages = authorize_descriptor_result.BuildFailedAuthorizationMessages();
            };

        It should_not_be_authorized = () => authorize_descriptor_result.IsAuthorized.ShouldBeFalse();
        It should_indicate_that_the_user_is_not_in_the_required_role = () => authorization_messages.First().IndexOf(Testing.Fakes.Security.SecurityDescriptor.NAMESPACE_ROLE).ShouldBeGreaterThan(0);
        It should_indicate_the_secured_namespace  = () => authorization_messages.First().IndexOf(Testing.Fakes.Security.SecurityDescriptor.SECURED_NAMESPACE).ShouldBeGreaterThan(0);
    }
}