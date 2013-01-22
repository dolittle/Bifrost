using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_Securable
{
    [Subject(typeof(SecurityTarget))]
    public class when_authorizing_target_that_is_not_authorized_by_a_securable
    {
        static SecurityTarget target;
        static Mock<ISecurable> securable_that_is_authorized;
        static Mock<ISecurable> securable_that_is_not_authorized;
        static AuthorizeSecurableResult securable_authorized;
        static AuthorizeSecurableResult securable_does_not_authorize;
        static AuthorizeTargetResult result;

        Establish context = () =>
            {
                var actor_not_authorised = new AuthorizeActorResult(null);
                actor_not_authorised.AddBrokenRule(new Mock<ISecurityRule>().Object);

                securable_that_is_authorized = new Mock<ISecurable>();
                securable_that_is_not_authorized = new Mock<ISecurable>();

                securable_authorized = new AuthorizeSecurableResult(securable_that_is_authorized.Object);
                securable_does_not_authorize = new AuthorizeSecurableResult(securable_that_is_not_authorized.Object);
                securable_does_not_authorize.AddAuthorizeActorResult(actor_not_authorised);
                securable_that_is_authorized.Setup(a => a.Authorize(Moq.It.IsAny<object>())).Returns(securable_authorized);
                securable_that_is_not_authorized.Setup(a => a.Authorize(Moq.It.IsAny<object>())).Returns(securable_does_not_authorize);
                target = new SecurityTarget();
                target.AddSecurable(securable_that_is_authorized.Object);
                target.AddSecurable(securable_that_is_not_authorized.Object);
            };

        Because of = () => result = target.Authorize(new object());

        It should_not_be_authorized = () => result.IsAuthorized.ShouldBeFalse();
        It should_hold_the_results_of_each_actor_authorization = () =>
            {
                result.AuthorizeSecurableResults.Count().ShouldEqual(2);
                result.AuthorizeSecurableResults.Count(r => r == securable_authorized).ShouldEqual(1);
                result.AuthorizeSecurableResults.Count(r => r == securable_does_not_authorize).ShouldEqual(1);
            };

        It should_have_a_reference_to_the_target = () => result.Target.ShouldEqual(target);
    }
}