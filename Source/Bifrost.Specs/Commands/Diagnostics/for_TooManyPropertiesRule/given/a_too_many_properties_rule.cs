using Bifrost.Commands.Diagnostics;
using Bifrost.Diagnostics;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.Diagnostics.for_TooManyPropertiesRule.given
{
    public class a_too_many_properties_rule
    {
        protected static TooManyPropertiesRule rule;
        protected static Mock<IProblems> problems_mock;

        Establish context = () =>
        {
            problems_mock = new Mock<IProblems>();
            rule = new TooManyPropertiesRule();
        };
    }
}
