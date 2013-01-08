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
        static UncommittedEventStream   uncommitted_event_stream;
        static CommittedEventStream committed_event_stream;

        Establish context = () =>
        {
            uncommitted_event_stream = new UncommittedEventStream(event_source_id);
            committed_event_stream = new CommittedEventStream(event_source_id);
            event_store_mock.Setup(e => e.Commit(uncommitted_event_stream)).Returns(committed_event_stream);
        };

        Because of = () => coordinator.Commit(uncommitted_event_stream);

        It should_insert_event_stream_into_repository = () => event_store_mock.Verify(e => e.Commit(uncommitted_event_stream), Times.Once());
        It should_notify_changes_with_the_comitted_event_stream = () => event_store_change_manager_mock.Verify(e => e.NotifyChanges(event_store_mock.Object, committed_event_stream), Times.Once());
        It should_delegate_processing_of_the_committed_event_stream_to_the_subscription_manager = () => event_subscription_manager_mock.Verify(e => e.Process(committed_event_stream), Times.Once());
    }
}
