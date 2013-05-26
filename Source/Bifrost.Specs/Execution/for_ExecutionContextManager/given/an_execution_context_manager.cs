using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_ExecutionContextManager.given
{
    public class an_execution_context_manager : all_dependencies
    {
        protected static ExecutionContextManager    manager;

        Establish context = () =>
        {
            
            manager = new ExecutionContextManager(execution_context_factory_mock.Object, call_context_mock.Object);
        };
    }
}
