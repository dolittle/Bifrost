using Machine.Specifications;
using Bifrost.Events;
using System;
using Moq;
using It = Machine.Specifications.It;


namespace Bifrost.Specs.Events.for_EventStore
{
    [Subject(typeof(EventStore))]
    public class when_saving_uncommitted_events : given.an_event_store
    {
        static Guid event_source_id = Guid.NewGuid();
        static UncommittedEventStream   event_stream;

        Establish context = () => event_stream = new UncommittedEventStream(event_source_id);

        Because of = () => event_store.Save(event_stream);

        It should_insert_event_stream_into_repository = () => event_repository_mock.Verify(e => e.Insert(event_stream), Times.Once());
        It should_notify_changes_with_event_stream = () => event_store_change_manager_mock.Verify(e => e.NotifyChanges(event_store, event_stream), Times.Once());
        It should_ensure_events_are_persisted_in_a_localized_scope = () => localizer_mock.Verify(s => s.BeginScope(), Times.Once());
    }
}
