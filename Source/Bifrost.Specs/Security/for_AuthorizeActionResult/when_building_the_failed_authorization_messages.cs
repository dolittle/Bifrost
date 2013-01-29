using System.Collections.Generic;
using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_AuthorizeActionResult
{
    [Subject(typeof(AuthorizeActionResult))]
    public class when_building_the_failed_authorization_messages
    {
        static Mock<AuthorizeTargetResult> first_failed_authorization;
        static Mock<AuthorizeTargetResult> second_failed_authorization;
        static string first_target_first_description = "first_target_first_description";
        static string first_target_second_description = "first_target_second_description";
        static string second_target_description = "second_target";
        static AuthorizeActionResult result;
        static IEnumerable<string> failed_authorization_messages;
        static string action_description = "my action";

        Establish context = () =>
        {
            first_failed_authorization = new Mock<AuthorizeTargetResult>(new Mock<ISecurityTarget>().Object);
            first_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                      .Returns(new[] {first_target_first_description, first_target_second_description});
            first_failed_authorization.SetupGet(a => a.IsAuthorized).Returns(false);
            second_failed_authorization = new Mock<AuthorizeTargetResult>(new Mock<ISecurityTarget>().Object);
            second_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                        .Returns(new[] { second_target_description });
            second_failed_authorization.SetupGet(a => a.IsAuthorized).Returns(false);

            var securityAction = new Mock<ISecurityAction>();
            securityAction.Setup(s => s.ActionType).Returns(action_description);
            result = new AuthorizeActionResult(securityAction.Object);
            result.ProcessAuthorizeTargetResult(first_failed_authorization.Object);
            result.ProcessAuthorizeTargetResult(second_failed_authorization.Object);

        };

        Because of = () => failed_authorization_messages = result.BuildFailedAuthorizationMessages();

        It have_three_failed_authorization_messages = () => failed_authorization_messages.Count().ShouldEqual(3);
        It should_have_the_action_in_each_message = () => failed_authorization_messages.All(m => m.StartsWith(action_description)).ShouldBeTrue();
        It should_have_one_message_with_the_first_target_first_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_target_first_description)).ShouldEqual(1);
        It should_have_one_message_with_the_first_target_second_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_target_second_description)).ShouldEqual(1);
        It should_have_one_message_with_the_second_target_message = () => failed_authorization_messages.Count(m => m.EndsWith(second_target_description)).ShouldEqual(1);
    }
}