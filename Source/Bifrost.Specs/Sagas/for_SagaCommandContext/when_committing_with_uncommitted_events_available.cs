using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaCommandContext
{
    public class when_committing_with_uncommitted_events_available : given.a_saga_command_context_with_aggregated_root_that_has_uncommitted_events
    {
        Because of = () => command_context.Commit();

        It should_save_the_events = () => saga_mock.Verify(s => s.Commit(uncommitted_events));
        It should_try_to_process_the_events_on_the_saga = () => process_method_invoker_mock.Verify(p => p.TryProcess(saga_mock.Object, simple_event));
        
        It should_commit_the_aggregated_root = () => aggregated_root_mock.Verify(a => a.Commit());
    }
}
