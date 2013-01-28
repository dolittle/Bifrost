using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_RoleRule
{
    [Subject(typeof (RoleRule))]
    public class when_getting_the_rule_description : given.a_rule_role
    {
        static string description;
        static string expected_description;

        Establish context = () => expected_description = string.Format(RoleRule.DescriptionFormat, required_role);

        Because of = () => description = rule.Description;

        It should_indicate_the_role_that_is_required = () => description.ShouldEqual(expected_description);
    }
}