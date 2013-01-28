using System.Collections.Generic;
using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_AuthorizeSecurableResult
{
    [Subject(typeof(AuthorizeSecurableResult))]
    public class when_building_the_failed_authorization_messages
    {
        static Mock<AuthorizeActorResult> first_failed_authorization;
        static Mock<AuthorizeActorResult> second_failed_authorization;
        static string first_actor_first_description = "first_actor_first_description";
        static string first_actor_second_description = "first_actor_second_description";
        static string second_actor_description = "second_actor";
        static AuthorizeSecurableResult result;
        static IEnumerable<string> failed_authorization_messages;
        static string securable_description = "my securable";

        Establish context = () =>
        {
            first_failed_authorization = new Mock<AuthorizeActorResult>(new Mock<ISecurityActor>().Object);
            first_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                      .Returns(new[] {first_actor_first_description, first_actor_second_description});
            second_failed_authorization = new Mock<AuthorizeActorResult>(new Mock<ISecurityActor>().Object);
            second_failed_authorization.Setup(a => a.BuildFailedAuthorizationMessages())
                                        .Returns(new[] { second_actor_description });

            result = new AuthorizeSecurableResult(new Securable(securable_description));
            result.ProcessAuthorizeActorResult(first_failed_authorization.Object);
            result.ProcessAuthorizeActorResult(second_failed_authorization.Object);

        };

        Because of = () => failed_authorization_messages = result.BuildFailedAuthorizationMessages();

        It have_three_failed_authorization_messages = () => failed_authorization_messages.Count().ShouldEqual(3);
        It should_have_the_securable_in_each_message = () => failed_authorization_messages.All(m => m.StartsWith(securable_description)).ShouldBeTrue();
        It should_have_one_message_with_the_first_actor_first_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_actor_first_description)).ShouldEqual(1);
        It should_have_one_message_with_the_first_actor_second_message = () => failed_authorization_messages.Count(m => m.EndsWith(first_actor_second_description)).ShouldEqual(1);
        It should_have_one_message_with_the_second_actor_message = () => failed_authorization_messages.Count(m => m.EndsWith(second_actor_description)).ShouldEqual(1);
    }
}