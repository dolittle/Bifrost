using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Domain;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource
{
    [Subject(Subjects.applying_events)]
	public class when_applying_an_event_without_event_envelopes
	{
        static IEvent @event;
        static IEventSource event_source;
        static Exception exception;

		Establish context = () =>
				{
                    var id = Guid.NewGuid();
                    @event = new SimpleEvent(id);
                    event_source = new StatelessAggregatedRoot(id);
                };

        Because of = () => exception = Catch.Exception(() => event_source.Apply(@event));

        It should_throw_event_envelops_missing = () => exception.ShouldBeOfExactType<EventEnvelopesMissing>();
	}
}