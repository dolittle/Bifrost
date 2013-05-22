using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_ExecutionContextManager.given
{
    public class all_dependencies
    {
        protected static Mock<IExecutionContextFactory> execution_context_factory_mock;
        protected static Mock<ICallContext> call_context_mock;

        Establish context = () =>
        {
            call_context_mock = new Mock<ICallContext>();
            execution_context_factory_mock = new Mock<IExecutionContextFactory>();
        };
    }
}
