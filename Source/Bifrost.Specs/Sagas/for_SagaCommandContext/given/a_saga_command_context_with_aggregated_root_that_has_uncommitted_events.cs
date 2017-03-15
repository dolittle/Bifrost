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
		protected static Mock<IAggregateRoot> aggregated_root;
		protected static UncommittedEventStream uncommitted_events;
		protected static SimpleEvent simple_event;

		Establish context = () =>
		                    	{
                                    aggregated_root = new Mock<IAggregateRoot>();
                                    aggregated_root.Setup(a => a.UncommittedEvents).Returns(uncommitted_events);
                                    aggregated_root.SetupGet(e => e.EventSourceId).Returns(event_source_id);

                                    var version = new EventSourceVersion(1, 0);

                                    simple_event = new SimpleEvent(event_source_id);

                                    uncommitted_events = new UncommittedEventStream(aggregated_root.Object);
		                    		uncommitted_events.Append(simple_event, version);

									command_context.RegisterForTracking(aggregated_root.Object);
		                    	};
	}
}