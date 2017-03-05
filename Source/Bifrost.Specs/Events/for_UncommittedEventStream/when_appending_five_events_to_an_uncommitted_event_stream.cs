using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_UncommittedEventStream
{
    public class when_appending_five_events_to_an_uncommitted_event_stream : given.an_empty_uncommitted_event_stream
    {
        static List<EventAndEnvelope> events_and_envelopes;
        static List<IEventEnvelope> event_envelops;

        Establish context =
            () =>
                {
                    events_and_envelopes = new List<EventAndEnvelope>();
                    for (var i = 0; i < 5; i++ )
                    {
                        var @event = new SimpleEvent(event_source_id);
                        var envelope = new Mock<IEventEnvelope>();
                        envelope.SetupGet(e => e.EventSourceId).Returns(event_source_id);
                        events_and_envelopes.Add(new EventAndEnvelope(envelope.Object, @event));
                    }
                };

        Because of = () => events_and_envelopes.ForEach(e => event_stream.Append(e.Envelope, e.Event));

        It should_have_events = () => event_stream.HasEvents.ShouldBeTrue();
        It should_have_an_event_count_of_5 = () => event_stream.Count.ShouldEqual(5);
    }
}