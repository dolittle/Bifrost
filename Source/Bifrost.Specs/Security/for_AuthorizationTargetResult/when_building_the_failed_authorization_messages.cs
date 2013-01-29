using System.Collections.Generic;
using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_AuthorizationTargetResult
{
    [Subject(typeof(AuthorizeTargetResult))]
    public class when_building_the_failed_authorization_messages
    {
        static Mock<AuthorizeSecurableResult> first_failed_authorization;
        static Mock<AuthorizeSecurableResult> second_failed_authorization;
        static string first_securable_first_description = "first_securable_first_description";
        static string first_securable_second_description = "first_securable_second_description";
        static string second_securable_description = "second_secrurable";
        static AuthorizeTargetResult result;
        static IEnumerable<string> failed_authorization_messages;
        static string target_description = "my target";

        Establish context = () =>
        {
            first_failed_authorization = new Mock<AuthorizeSecurableResult>(new Mock<ISecurable>().Object);
            first_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                      .Returns(new[] {first_securable_first_description, first_securable_second_description});
            second_failed_authorization = new Mock<AuthorizeSecurableResult>(new Mock<ISecurable>().Object);
            second_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                        .Returns(new[] { second_securable_description });

            result = new AuthorizeTargetResult(new SecurityTarget(target_description));
            result.ProcessAuthorizeSecurableResult(first_failed_authorization.Object);
            result.ProcessAuthorizeSecurableResult(second_failed_authorization.Object);

        };

        Because of = () => failed_authorization_messages = result.BuildFailedAuthorizationMessages();

        It have_three_failed_authorization_messages = () => failed_authorization_messages.Count().ShouldEqual(3);
        It should_have_the_target_in_each_message = () => failed_authorization_messages.All(m => m.StartsWith(target_description)).ShouldBeTrue();
        It should_have_one_message_with_the_first_securable_first_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_securable_first_description)).ShouldEqual(1);
        It should_have_one_message_with_the_first_securable_second_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_securable_second_description)).ShouldEqual(1);
        It should_have_one_message_with_the_second_securable_message = () => failed_authorization_messages.Count(m => m.EndsWith(second_securable_description)).ShouldEqual(1);
    }
}