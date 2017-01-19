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
        protected static Guid aggregated_root_id = Guid.NewGuid();
        protected static Mock<IAggregateRoot> aggregated_root_mock;
        protected static UncommittedEventStream uncommitted_events;
        protected static SimpleEvent simple_event;

        Establish context = () =>
                                {
                                    simple_event = new SimpleEvent(aggregated_root_id);
                                    uncommitted_events = new UncommittedEventStream(aggregated_root_id);
                                    uncommitted_events.Append(simple_event);
                                    aggregated_root_mock = new Mock<IAggregateRoot>();
                                    aggregated_root_mock.Setup(a => a.UncommittedEvents).Returns(uncommitted_events);

                                    command_context.RegisterForTracking(aggregated_root_mock.Object);
                                };
    }
}