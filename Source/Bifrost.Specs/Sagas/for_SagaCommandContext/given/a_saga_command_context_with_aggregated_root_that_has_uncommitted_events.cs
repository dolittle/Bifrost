using System;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_SagaCommandContext.given
{
	public class a_saga_command_context_with_aggregated_root_that_has_uncommitted_events : a_saga_command_context
	{
		protected static EventSourceId event_source_id = Guid.NewGuid();
		protected static Mock<IAggregateRoot> aggregated_root_mock;
		protected static UncommittedEventStream uncommitted_events;
		protected static SimpleEvent simple_event;
        protected static Mock<IEventEnvelope> simple_event_envelope;

		Establish context = () =>
		                    	{
		                    		simple_event = new SimpleEvent(event_source_id);
                                    simple_event_envelope = new Mock<IEventEnvelope>();
                                    simple_event_envelope.SetupGet(e => e.EventSourceId).Returns(event_source_id);
                                    simple_event_envelope.SetupGet(e => e.Version).Returns(new EventSourceVersion(1, 0));

                                    uncommitted_events = new UncommittedEventStream(event_source_id);
		                    		uncommitted_events.Append(simple_event_envelope.Object, simple_event);
		                    		aggregated_root_mock = new Mock<IAggregateRoot>();
		                    		aggregated_root_mock.Setup(a => a.UncommittedEvents).Returns(uncommitted_events);

									command_context.RegisterForTracking(aggregated_root_mock.Object);
		                    	};
	}
}