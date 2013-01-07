using Machine.Specifications;
using Bifrost.Events;
using System;
using Moq;
using It = Machine.Specifications.It;
using Bifrost.Fakes.Events;


namespace Bifrost.Specs.Events.for_EventStore
{
    [Subject(typeof(EventStore))]
    public class when_committing_two_uncommitted_events : given.an_event_store
    {
        static Guid event_source_id = Guid.NewGuid();
        static UncommittedEventStream   event_stream;
        static SimpleEvent first_event;
        static SimpleEvent second_event;

        Establish context = () =>
        {
            first_event = new SimpleEvent(event_source_id) { Content = "First" };
            second_event = new SimpleEvent(event_source_id) { Content = "Second" };
            event_stream = new UncommittedEventStream(event_source_id);
            event_stream.Append(first_event);
            event_stream.Append(second_event);
        };

        Because of = () => event_store.Commit(event_stream);

        It should_insert_first_event = () => entity_context_mock.Verify(e => e.Insert(first_event), Times.Once());
        It should_insert_second_event = () => entity_context_mock.Verify(e => e.Insert(second_event), Times.Once());
    }
}
