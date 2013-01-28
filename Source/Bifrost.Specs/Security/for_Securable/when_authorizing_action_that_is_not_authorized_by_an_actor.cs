using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_Securable
{
    [Subject(typeof(Securable))]
    public class when_authorizing_action_that_is_not_authorized_by_an_actor
    {
        static Securable securable;
        static Mock<ISecurityActor> actor_that_is_authorized;
        static Mock<ISecurityActor> actor_that_is_not_authorized;
        static AuthorizeActorResult actor_authorized;
        static AuthorizeActorResult actor_does_not_authorize;
        static AuthorizeSecurableResult result;

        Establish context = () =>
            {
                actor_that_is_authorized = new Mock<ISecurityActor>();
                actor_that_is_not_authorized = new Mock<ISecurityActor>();

                actor_authorized = new AuthorizeActorResult(actor_that_is_authorized.Object);
                actor_does_not_authorize = new AuthorizeActorResult(actor_that_is_not_authorized.Object);
                actor_does_not_authorize.AddBrokenRule(new Mock<ISecurityRule>().Object);
                actor_that_is_authorized.Setup(a => a.IsAuthorized(Moq.It.IsAny<object>())).Returns(actor_authorized);
                actor_that_is_not_authorized.Setup(a => a.IsAuthorized(Moq.It.IsAny<object>())).Returns(actor_does_not_authorize);
                securable = new Securable(string.Empty);
                securable.AddActor(actor_that_is_authorized.Object);
                securable.AddActor(actor_that_is_not_authorized.Object);
            };

        Because of = () => result = securable.Authorize(new object());

        It should_not_be_authorized = () => result.IsAuthorized.ShouldBeFalse();
        It should_hold_the_results_of_each_failed_actor_authorization = () =>
            {
                result.AuthorizationFailures.Count().ShouldEqual(1);
                result.AuthorizationFailures.Count(r => r == actor_does_not_authorize).ShouldEqual(1);
            };
        It should_have_a_reference_to_the_securable_authorizing = () => result.Securable.ShouldEqual(securable);
    }
}