using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_EventSource.given
{
	public class a_stateful_event_source : all_dependencies
	{
		protected static StatefulAggregatedRoot event_source;
		protected static EventSourceId event_source_id;
		protected static IEvent @event;
        protected static Mock<IEventEnvelope> event_envelope;

		Establish context = () =>
				{
					event_source_id = Guid.NewGuid();
					event_source = new StatefulAggregatedRoot(event_source_id);
                    event_source.EventEnvelopes = event_envelopes.Object;
				};
	}
}