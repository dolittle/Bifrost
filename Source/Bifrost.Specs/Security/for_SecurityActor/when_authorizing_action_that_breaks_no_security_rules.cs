using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityActor
{
    [Subject(typeof(SecurityActor))]
    public class when_authorizing_action_that_breaks_no_security_rules
    {
        static Mock<ISecurityRule> another_rule_that_is_not_broken_by_action;
        static Mock<ISecurityRule> rule_that_is_not_broken_by_action;
        static SecurityActor security_actor;
        static AuthorizeActorResult result;

        Establish context = () =>
            {
                rule_that_is_not_broken_by_action = new Mock<ISecurityRule>();
                rule_that_is_not_broken_by_action.Setup(r => r.IsAuthorized(Moq.It.IsAny<object>())).Returns(true);
                another_rule_that_is_not_broken_by_action = new Mock<ISecurityRule>();
                another_rule_that_is_not_broken_by_action.Setup(r => r.IsAuthorized(Moq.It.IsAny<object>())).Returns(true);

                security_actor = new SecurityActor();
                security_actor.AddRule(rule_that_is_not_broken_by_action.Object);
                security_actor.AddRule(another_rule_that_is_not_broken_by_action.Object);
            };

        Because of = () => result = security_actor.IsAuthorized(new object());

        It should_be_authorized = () => result.IsAuthorized.ShouldBeTrue();
        It should_not_have_any_broken_rules = () => result.BrokenRules.Any().ShouldBeFalse();
        It should_not_have_any_exceptions = () => result.RulesThatEncounteredAnErrorWhenEvaluating.Any().ShouldBeFalse();
        It should_have_a_reference_to_the_actor_authorizing = () => result.Actor.ShouldEqual(security_actor);
    }
}