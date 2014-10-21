using Bifrost.Commands;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandValidators.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ICommandValidator>> validators_mock;

        Establish context = () => validators_mock = new Mock<IInstancesOf<ICommandValidator>>();
    }
}
