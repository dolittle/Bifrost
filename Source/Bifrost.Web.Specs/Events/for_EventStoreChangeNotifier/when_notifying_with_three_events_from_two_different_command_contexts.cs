using Bifrost.Web.Events;
using Machine.Specifications;
using Moq;
using Bifrost.Events;
using System;
using Bifrost.Testing.Fakes.Events;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Specs.Events.for_EventStoreChangeNotifier
{
    public class when_notifying_with_three_events_from_two_different_command_contexts
    {
        static Guid first_command_context = Guid.NewGuid();
        static Guid second_command_context = Guid.NewGuid();
        static Guid event_source_id = Guid.NewGuid();
        static Mock<IEventStore> event_store_mock;
        static Mock<ICommandCoordinatorEvents> command_coordinator_events_mock;
        static EventStoreChangeNotifier    notifier;
        static UncommittedEventStream   events;


        Establish   context = () => 
        {
            event_store_mock = new Mock<IEventStore>();
            command_coordinator_events_mock = new Mock<ICommandCoordinatorEvents>();
            notifier = new EventStoreChangeNotifier(command_coordinator_events_mock.Object);
            events = new UncommittedEventStream(event_source_id);
            events.Append(new SimpleEvent(event_source_id) { CommandContext = first_command_context });
            events.Append(new SimpleEvent(event_source_id) { CommandContext = first_command_context });
            events.Append(new SimpleEvent(event_source_id) { CommandContext = second_command_context });
        };

        Because of = () => notifier.Notify(event_store_mock.Object, events);

        It should_call_events_processed_once_for_first_command_context = () => command_coordinator_events_mock.Verify(c => c.EventsProcessed(first_command_context), Times.Once());
        It should_call_events_processed_once_for_second_command_context = () => command_coordinator_events_mock.Verify(c => c.EventsProcessed(second_command_context), Times.Once());
    }
}
