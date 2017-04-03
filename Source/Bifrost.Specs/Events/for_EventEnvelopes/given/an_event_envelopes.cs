using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventEnvelopes.given
{
    public class an_event_envelopes : all_dependencies
    {
        protected static EventEnvelopes event_envelopes;

        Establish context = () => event_envelopes = new EventEnvelopes(
                application_resources.Object,
                system_clock.Object,
                execution_context.Object,
                event_migration_hierarchy_manager.Object
            );
    }
}
