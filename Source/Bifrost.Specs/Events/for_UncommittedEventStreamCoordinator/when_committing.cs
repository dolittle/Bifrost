using Machine.Specifications;
using Bifrost.Events;
using System;
using Moq;
using It = Machine.Specifications.It;


namespace Bifrost.Specs.Events.for_UncommittedEventStreamCoordinator
{
    [Subject(typeof(UncommittedEventStream))]
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
        It should_delegate_coordination_of_the_committed_event_stream = () => committed_event_stream_coordinator_mock.Verify(c => c.Handle(committed_event_stream), Times.Once());
    }
}
