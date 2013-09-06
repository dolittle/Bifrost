using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Diagnostics.for_TypeRules
{
    public class when_validating_all_with_one_rule_reporting_problems : given.type_rules_with_one_rule
    {
        Establish context = () => problems_mock.SetupGet(p => p.Any).Returns(true);

        Because of = () => type_rules.ValidateAll();

        It should_report_any_problems = () => problems_reporter_mock.Verify(r => r.Report(problems_mock.Object), Times.Once());
    }
}
