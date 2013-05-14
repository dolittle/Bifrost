using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_ExecutionContextManager.given
{
    public class given_an_execution_context_manager
    {
        protected static Mock<IExecutionContextFactory> execution_context_factory_mock;
        protected static ExecutionContextManager    manager;

        Establish context = () =>
        {
            execution_context_factory_mock = new Mock<IExecutionContextFactory>();
            manager = new ExecutionContextManager(execution_context_factory_mock.Object);
        };
    }
}
