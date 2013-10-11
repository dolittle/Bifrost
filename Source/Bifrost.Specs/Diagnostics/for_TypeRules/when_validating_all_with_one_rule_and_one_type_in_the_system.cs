using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Diagnostics.for_TypeRules
{
    public class when_validating_all_with_one_rule_and_one_type_in_the_system : given.type_rules_with_one_rule
    {
        Because of = () => type_rules.ValidateAll();

        It should_create_one_problems_instance = () => problems_factory_mock.Verify(p => p.Create(), Times.Once());
        It should_ask_rule_to_validate = () => type_rule_mock.Verify(t => t.Validate(type_for_rule, problems_mock.Object));
        It should_not_report_any_problems = () => problems_reporter_mock.Verify(r => r.Report(problems_mock.Object), Times.Never());
    }
}
