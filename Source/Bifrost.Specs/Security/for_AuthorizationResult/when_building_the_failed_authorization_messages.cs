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
        static Mock<AuthorizeActionResult> first_failed_authorization;
        static Mock<AuthorizeActionResult> second_failed_authorization;
        static string first_action_first_description = "first_action_first_description";
        static string first_action_second_description = "first_action_second_description";
        static string second_action_description = "second_action";
        static AuthorizationResult result;
        static IEnumerable<string> failed_authorization_messages;

        Establish context = () =>
        {
            first_failed_authorization = new Mock<AuthorizeActionResult>(new Mock<ISecurityAction>().Object);
            first_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                      .Returns(new[] {first_action_first_description, first_action_second_description});
            second_failed_authorization = new Mock<AuthorizeActionResult>(new Mock<ISecurityAction>().Object);
            second_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                        .Returns(new[] { second_action_description });

            result = new AuthorizationResult();
            result.ProcessAuthorizeActionResult(first_failed_authorization.Object);
            result.ProcessAuthorizeActionResult(second_failed_authorization.Object);

        };

        Because of = () => failed_authorization_messages = result.GetFailedAuthorizationMessages();

        It have_three_failed_authorization_messages = () => failed_authorization_messages.Count().ShouldEqual(3);
        It should_have_one_message_with_the_first_action_first_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_action_first_description)).ShouldEqual(1);
        It should_have_one_message_with_the_first_action_second_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_action_second_description)).ShouldEqual(1);
        It should_have_one_message_with_the_second_action_message = () => failed_authorization_messages.Count(m => m.EndsWith(second_action_description)).ShouldEqual(1);
    }
}