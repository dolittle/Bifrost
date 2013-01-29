using System.Collections.Generic;
using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_AuthorizationResult
{
    [Subject(typeof(AuthorizationResult))]
    public class when_building_the_failed_authorization_messages
    {
        static Mock<AuthorizeDescriptorResult> first_failed_authorization;
        static Mock<AuthorizeDescriptorResult> second_failed_authorization;
        static string first_descriptor_first_description = "first_descriptor_first_description";
        static string first_descriptor_second_description = "first_descriptor_second_description";
        static string second_descriptor_description = "second_descriptor";
        static AuthorizationResult result;
        static IEnumerable<string> failed_authorization_messages;

        Establish context = () =>
        {
            first_failed_authorization = new Mock<AuthorizeDescriptorResult>();
            first_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                      .Returns(new[] {first_descriptor_first_description, first_descriptor_second_description});
            second_failed_authorization = new Mock<AuthorizeDescriptorResult>();
            second_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                        .Returns(new[] { second_descriptor_description });

            result = new AuthorizationResult();
            result.ProcessAuthorizeDescriptorResult(first_failed_authorization.Object);
            result.ProcessAuthorizeDescriptorResult(second_failed_authorization.Object);

        };

        Because of = () => failed_authorization_messages = result.BuildFailedAuthorizationMessages();

        It have_three_failed_authorization_messages = () => failed_authorization_messages.Count().ShouldEqual(3);
        It should_have_one_message_with_the_first_descriptor_first_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_descriptor_first_description)).ShouldEqual(1);
        It should_have_one_message_with_the_first_descriptor_second_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_descriptor_second_description)).ShouldEqual(1);
        It should_have_one_message_with_the_second_descriptor_message = () => failed_authorization_messages.Count(m => m.EndsWith(second_descriptor_description)).ShouldEqual(1);
    }
}