using System.Collections.Generic;
using System.Linq;
using Bifrost.Diagnostics;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Diagnostics.for_TypeRules
{
    public class when_validating_all_with_one_rule_and_two_types_in_the_system_with_problems : given.type_rules_with_one_rule_and_two_types
    {
        static List<IProblems>  problems_reported;

        Establish context = () =>
        {
            first_problems.SetupGet(p => p.Any).Returns(true);
            second_problems.SetupGet(p => p.Any).Returns(true);
            problems_reported = new List<IProblems>();

            first_problems.Setup(f => f.GetEnumerator()).Returns(new List<Problem>().GetEnumerator());

            problems_reporter_mock.Setup(r => r.Report(Moq.It.IsAny<IProblems>())).Callback((IProblems p) => problems_reported.Add(p));
        };

        Because of = () => type_rules.ValidateAll();

        It should_report_any_problems_for_first_set_of_problems = () => problems_reported.Where(p => p == first_problems.Object).Count().ShouldEqual(1);
        It should_report_any_problems_for_second_set_of_problems = () => problems_reported.Where(p => p == second_problems.Object).Count().ShouldEqual(1);
    }
}
