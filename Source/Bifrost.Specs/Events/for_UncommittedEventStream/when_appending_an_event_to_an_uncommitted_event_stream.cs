using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_UncommittedEventStream
{
    public class when_appending_an_event_to_an_uncommitted_event_stream : given.an_empty_uncommitted_event_stream
    {
        static IEvent @event;
        static EventSourceVersion version;

        Establish context = () =>
        {
            @event = new SimpleEvent(event_source_id);
            version = new EventSourceVersion(1, 2);
        };

        Because of = () => event_stream.Append(@event, version);

        It should_have_events = () => event_stream.HasEvents.ShouldBeTrue();
        It should_have_an_event_count_of_1 = () => event_stream.Count.ShouldEqual(1);
    }
}