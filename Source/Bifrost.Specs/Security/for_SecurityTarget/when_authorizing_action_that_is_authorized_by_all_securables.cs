using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_Securable
{
    [Subject(typeof(Securable))]
    public class when_authorizing_action_that_is_authorized_by_all_securables
    {
        static SecurityTarget target;
        static Mock<ISecurable> securable_that_is_authorized;
        static Mock<ISecurable> another_securable_that_is_authorized;
        static AuthorizeSecurableResult securable_authorized;
        static AuthorizeSecurableResult another_securable_authorized;
        static AuthorizeTargetResult result;

        Establish context = () =>
            {
                securable_that_is_authorized = new Mock<ISecurable>();
                another_securable_that_is_authorized = new Mock<ISecurable>();

                securable_authorized = new AuthorizeSecurableResult(securable_that_is_authorized.Object);
                another_securable_authorized = new AuthorizeSecurableResult(another_securable_that_is_authorized.Object);
                securable_that_is_authorized.Setup(a => a.Authorize(Moq.It.IsAny<object>())).Returns(securable_authorized);
                another_securable_that_is_authorized.Setup(a => a.Authorize(Moq.It.IsAny<object>())).Returns(another_securable_authorized);
                target = new SecurityTarget();
                target.AddSecurable(securable_that_is_authorized.Object);
                target.AddSecurable(another_securable_that_is_authorized.Object);
            };

        Because of = () => result = target.Authorize(new object());

        It should_be_authorized = () => result.IsAuthorized.ShouldBeTrue();
        It should_hold_the_results_of_each_actor_authorization = () =>
            {
                result.AuthorizeSecurableResults.Count().ShouldEqual(2);
                result.AuthorizeSecurableResults.Count(r => r == securable_authorized).ShouldEqual(1);
                result.AuthorizeSecurableResults.Count(r => r == another_securable_authorized).ShouldEqual(1);
            };

        It should_have_a_reference_to_the_target = () => result.Target.ShouldEqual(target);
    }
}