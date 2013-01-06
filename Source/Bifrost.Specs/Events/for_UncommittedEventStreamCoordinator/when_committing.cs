using Machine.Specifications;
using Bifrost.Events;
using System;
using Moq;
using It = Machine.Specifications.It;


namespace Bifrost.Specs.Events.for_UncommittedEventStreamCoordinator
{
    [Subject(typeof(EventStore))]
    public class when_committing : given.an_uncommitted_event_stream_coordinator
    {
        static Guid event_source_id = Guid.NewGuid();
        static UncommittedEventStream   event_stream;

        Establish context = () => event_stream = new UncommittedEventStream(event_source_id);

        Because of = () => coordinator.Commit(event_stream);

        It should_insert_event_stream_into_repository = () => event_store_mock.Verify(e => e.Save(event_stream), Times.Once());
        It should_notify_changes_with_event_stream = () => event_store_change_manager_mock.Verify(e => e.NotifyChanges(event_store_mock.Object, event_stream), Times.Once());
        It should_delegate_processing_to_the_subscription_manager = () => event_subscription_manager_mock.Verify(e => e.Process(event_stream), Times.Once());
    }
}
