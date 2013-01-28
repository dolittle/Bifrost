using System;
using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityActor
{
    [Subject(typeof(SecurityActor))]
    public class when_authorizing_action_with_a_rule_that_causes_an_error
    {
        static Mock<ISecurityRule> rule_that_causes_an_error;
        static Mock<ISecurityRule> rule_that_is_not_broken_by_action;
        static SecurityActor security_actor;
        static AuthorizeActorResult result;
        static Exception exception;

        Establish context = () =>
            {
                exception = new Exception("Rule could not be applied");
                rule_that_is_not_broken_by_action = new Mock<ISecurityRule>();
                rule_that_is_not_broken_by_action.Setup(r => r.IsAuthorized(Moq.It.IsAny<object>())).Returns(true);
                rule_that_causes_an_error = new Mock<ISecurityRule>();
                rule_that_causes_an_error.Setup(r => r.IsAuthorized(Moq.It.IsAny<object>())).Throws(exception);

                security_actor = new SecurityActor(string.Empty);
                security_actor.AddRule(rule_that_is_not_broken_by_action.Object);
                security_actor.AddRule(rule_that_causes_an_error.Object);
            };

        Because of = () => result = security_actor.IsAuthorized(new object());

        It should_not_be_authorized = () => result.IsAuthorized.ShouldBeFalse();
        It should_not_have_any_broken_rules = () => result.BrokenRules.Any().ShouldBeFalse();
        It should_have_the_rule_that_caused_the_error = () =>
            {
                result.RulesThatEncounteredAnErrorWhenEvaluating.Count().ShouldEqual(1);
                var error = result.RulesThatEncounteredAnErrorWhenEvaluating.First();
                error.Rule.ShouldEqual(rule_that_causes_an_error.Object);
                error.Error.ShouldEqual(exception);
            };
        It should_have_a_reference_to_the_actor_authorizing = () => result.Actor.ShouldEqual(security_actor);
    }
}