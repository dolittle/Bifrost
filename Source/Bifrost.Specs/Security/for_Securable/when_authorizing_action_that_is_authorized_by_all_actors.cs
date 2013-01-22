using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_Securable
{
    [Subject(typeof(Securable))]
    public class when_authorizing_action_that_is_authorized_by_all_actors
    {
        static Securable securable;
        static Mock<ISecurityActor> actor_that_is_authorized;
        static Mock<ISecurityActor> another_actor_that_is_authorized;
        static AuthorizeActorResult actor_authorized;
        static AuthorizeActorResult another_actor_authorized;
        static AuthorizeSecurableResult result;

        Establish context = () =>
            {
                actor_that_is_authorized = new Mock<ISecurityActor>();
                another_actor_that_is_authorized = new Mock<ISecurityActor>();

                actor_authorized = new AuthorizeActorResult(actor_that_is_authorized.Object);
                another_actor_authorized = new AuthorizeActorResult(another_actor_that_is_authorized.Object);
                actor_that_is_authorized.Setup(a => a.IsAuthorized(Moq.It.IsAny<object>())).Returns(actor_authorized);
                another_actor_that_is_authorized.Setup(a => a.IsAuthorized(Moq.It.IsAny<object>())).Returns(another_actor_authorized);
                securable = new Securable();
                securable.AddActor(actor_that_is_authorized.Object);
                securable.AddActor(another_actor_that_is_authorized.Object);
            };

        Because of = () => result = securable.Authorize(new object());

        It should_be_authorized = () => result.IsAuthorized.ShouldBeTrue();
        It should_hold_the_results_of_each_actor_authorization = () =>
            {
                result.AuthorizeActorResults.Count().ShouldEqual(2);
                result.AuthorizeActorResults.Count(r => r == actor_authorized).ShouldEqual(1);
                result.AuthorizeActorResults.Count(r => r == another_actor_authorized).ShouldEqual(1);
            };
        It should_have_a_reference_to_the_securable_authorizing = () => result.Securable.ShouldEqual(securable);
    }
}