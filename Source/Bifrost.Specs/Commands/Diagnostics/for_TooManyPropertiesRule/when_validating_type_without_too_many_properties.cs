using Bifrost.Diagnostics;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.Diagnostics.for_TooManyPropertiesRule
{
    public class when_validating_type_without_too_many_properties : given.a_too_many_properties_rule
    {
        Because of = () => rule.Validate(typeof(CommandWithoutTooManyProperties), problems_mock.Object);

        It should_not_report_any_problem = () => problems_mock.Verify(p => p.Report(Moq.It.IsAny<ProblemType>(), Moq.It.IsAny<object>()), Times.Never());
    }
}
