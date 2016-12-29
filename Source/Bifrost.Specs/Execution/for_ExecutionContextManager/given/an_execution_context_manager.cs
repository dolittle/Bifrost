using Bifrost.Execution;
using Bifrost.Testing;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ExecutionContextManager.given
{
    public class an_execution_context_manager : dependency_injection
    {
        protected static ExecutionContextManager manager;

        Establish context = () => manager = Get<ExecutionContextManager>();
    }
}