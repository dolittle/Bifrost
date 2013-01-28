using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_AuthorizeActorResult
{
    [Subject(typeof(SecurityActor))]
    public class when_building_the_failed_authorization_messages
    {
        static Mock<ISecurityRule> broken_rule;
        static Mock<ISecurityRule> error_rule;
        static SecurityActor security_actor;
        static string actor_description = "actor";
        static string rule_1_description = "rule 1";
        static string rule_2_description = "rule 2";
        static IEnumerable<string> failed_authorization_messages;

        static AuthorizeActorResult result;

        Establish context = () =>
            {
                broken_rule = new Mock<ISecurityRule>();
                broken_rule.Setup(r => r.Description).Returns(rule_1_description);
                error_rule = new Mock<ISecurityRule>();
                error_rule.Setup(r => r.Description).Returns(rule_2_description);

                security_actor = new SecurityActor(actor_description);

                result = new AuthorizeActorResult(security_actor);
                result.AddBrokenRule(broken_rule.Object);
                result.AddErrorRule(error_rule.Object, new Exception());
            };

        Because of = () => failed_authorization_messages = result.BuildFailedAuthorizationMessages();

        It should_return_two_messages = () => failed_authorization_messages.Count().ShouldEqual(2);
        It should_having_one_messages_containing_broken_rule_description = () => failed_authorization_messages.Count(s => s.EndsWith(rule_1_description)).ShouldEqual(1);
        It should_having_one_messages_containing_error_rule_description = () => failed_authorization_messages.Count(s => s.EndsWith("Error")).ShouldEqual(1);
        It should_have_actor_in_each_messages = () => failed_authorization_messages.All(s => s.StartsWith(actor_description)).ShouldBeTrue();

    }
}