using Bifrost.Diagnostics;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.Diagnostics.for_CommandInheritanceRule
{
    public class when_validating_type_with_nested_inheritance_hierarchy : given.a_command_inheritance_rule
    {
        Because of = () => rule.Validate(typeof(NestedCommandInheritanceHierarchy), problems_mock.Object);

        It should_report_one_problem = () => problems_mock.Verify(p => p.Report(Moq.It.IsAny<ProblemType>(), Moq.It.IsAny<object>()), Times.Once());
    }
}
