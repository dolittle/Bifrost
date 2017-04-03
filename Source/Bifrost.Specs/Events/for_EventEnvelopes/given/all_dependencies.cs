using Bifrost.Applications;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Time;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_EventEnvelopes.given
{
    public class all_dependencies
    {
        protected static Mock<IApplicationResources> application_resources;
        protected static Mock<ISystemClock> system_clock;
        protected static Mock<IExecutionContext> execution_context;
        protected static Mock<IEventMigrationHierarchyManager> event_migration_hierarchy_manager;

        Establish context = () =>
        {
            application_resources = new Mock<IApplicationResources>();
            system_clock = new Mock<ISystemClock>();
            execution_context = new Mock<IExecutionContext>();
            event_migration_hierarchy_manager = new Mock<IEventMigrationHierarchyManager>();
        };
    }
}
