using Bifrost.Diagnostics;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.Diagnostics.for_CommandInheritanceRule
{
    public class when_validating_type_inheriting_command : given.a_command_inheritance_rule
    {
        Because of = () => rule.Validate(typeof(CommandInheritingCommand), problems_mock.Object);

        It should_not_report_any_problems = () => problems_mock.Verify(p => p.Report(Moq.It.IsAny<ProblemType>(), Moq.It.IsAny<object>()), Times.Never());
    }
}
