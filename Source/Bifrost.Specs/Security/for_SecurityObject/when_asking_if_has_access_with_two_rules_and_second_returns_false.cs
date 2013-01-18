using Machine.Specifications;
using Bifrost.Security;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityObject
{
    public class when_asking_if_has_access_with_two_rules_and_second_returns_false
    {
        const string securable = "something";
        static SecurityObject   security_object;
        static bool result;
        static SecurityRule first_rule;
        static SecurityRule second_rule;


        Establish context = () =>
        {
            security_object = new SecurityObject();
            first_rule = new SecurityRule { HasAccessResult = true };
            security_object.AddRule(first_rule);
            second_rule = new SecurityRule { HasAccessResult = false };
            security_object.AddRule(second_rule);
        };

        Because of = () => result = security_object.HasAccess(securable);

        It should_return_false = () => result.ShouldBeFalse();
        It should_ask_the_first_rule = () => first_rule.HasAccessCalled.ShouldBeTrue();
        It should_ask_the_second_rule = () => second_rule.HasAccessCalled.ShouldBeTrue();
        It should_pass_the_securable_to_the_first_rule = () => first_rule.SecurablePassedToHasAccess.ShouldEqual(securable);
        It should_pass_the_securable_to_the_second_rule = () => second_rule.SecurablePassedToHasAccess.ShouldEqual(securable);
    }
}
