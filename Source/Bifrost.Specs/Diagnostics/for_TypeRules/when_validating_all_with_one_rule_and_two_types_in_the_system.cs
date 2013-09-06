using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Diagnostics.for_TypeRules
{
    public class when_validating_all_with_one_rule_and_two_types_in_the_system : given.type_rules_with_one_rule_and_two_types
    {
        Because of = () => type_rules.ValidateAll();

        It should_create_two_problems_instance = () => problems_factory_mock.Verify(p => p.Create(), Times.Exactly(2));
        It should_ask_rule_to_validate_first_rule = () => type_rule_mock.Verify(t => t.Validate(first_type, problems_mock.Object));
        It should_ask_rule_to_validate_second_rule = () => type_rule_mock.Verify(t => t.Validate(second_type, problems_mock.Object));
        It should_not_report_any_problems = () => problems_reporter_mock.Verify(r => r.Report(problems_mock.Object), Times.Never());
    }
}
