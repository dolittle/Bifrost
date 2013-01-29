using System.Security.Principal;
using System.Threading;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_authorizing_on_command_type_and_namespace_and_user_is_in_roles : given.a_configured_security_descriptor
    {
        static AuthorizeDescriptorResult authorize_descriptor_result;

        Establish context = () =>
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(""), new[]
                    {
                        Testing.Fakes.Security.SecurityDescriptor.NAMESPACE_ROLE,
                        Testing.Fakes.Security.SecurityDescriptor.SIMPLE_COMMAND_ROLE
                    });
            };

        Because of = () => authorize_descriptor_result = security_descriptor.Authorize(command_that_has_namespace_and_type_rule);

        It should_be_authorized = () => authorize_descriptor_result.IsAuthorized.ShouldBeTrue();
    }
}