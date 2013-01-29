using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityActor
{
    [Subject(typeof(SecurityActor))]
    public class when_authorizing_action_that_breaks_a_security_rule
    {
        static Mock<ISecurityRule> rule_that_is_not_broken_by_action;
        static Mock<ISecurityRule> rule_that_is_broken_by_action;
        static SecurityActor security_actor;
        static AuthorizeActorResult result;

        Establish context = () =>
            {
                rule_that_is_broken_by_action = new Mock<ISecurityRule>();
                rule_that_is_broken_by_action.Setup(r => r.IsAuthorized(Moq.It.IsAny<object>())).Returns(false);
                rule_that_is_not_broken_by_action = new Mock<ISecurityRule>();
                rule_that_is_not_broken_by_action.Setup(r => r.IsAuthorized(Moq.It.IsAny<object>())).Returns(true);

                security_actor = new SecurityActor(string.Empty);
                security_actor.AddRule(rule_that_is_not_broken_by_action.Object);
                security_actor.AddRule(rule_that_is_broken_by_action.Object);
            };

        Because of = () => result = security_actor.IsAuthorized(new object());

        It should_not_be_authorized = () => result.IsAuthorized.ShouldBeFalse();
        It should_have_the_rule_that_was_broken = () => result.BrokenRules.ShouldContainOnly(new [] { rule_that_is_broken_by_action.Object });
        It should_not_have_any_exceptions = () => result.RulesThatEncounteredAnErrorWhenEvaluating.Any().ShouldBeFalse();
        It should_have_a_reference_to_the_actor_authorizing = () => result.Actor.ShouldEqual(security_actor);
    }
}