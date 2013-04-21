using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ExecutionContextManager.given
{
    public class given_an_execution_context_manager
    {
        protected static ExecutionContextManager    manager;

        Establish context = () => manager = new ExecutionContextManager();
    }
}
