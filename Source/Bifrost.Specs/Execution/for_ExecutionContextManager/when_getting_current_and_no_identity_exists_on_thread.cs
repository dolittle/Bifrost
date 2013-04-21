using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ExecutionContextManager
{
    public class when_getting_current_and_no_identity_exists_on_thread : given.given_an_execution_context_manager
    {
        static IExecutionContext execution_context;

        Establish context = () => manager.Reset();

        Because of = () => execution_context = manager.Current;

        It should_have_an_identity = () => execution_context.Identity.ShouldNotBeNull();
        It should_have_an_identity_with_a_name = () => execution_context.Identity.Name.ShouldNotBeNull();
    }
}
