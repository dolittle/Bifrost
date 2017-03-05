using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandContext.given
{
    public class a_command_context_for_a_simple_command_with_one_tracked_object_with_one_uncommitted_event : a_command_context_for_a_simple_command_with_one_tracked_object
    {
        protected static SimpleEvent    uncommitted_event;
        protected static Mock<IEventEnvelope> event_envelope;

        Establish context = () =>
        {
            uncommitted_event = new SimpleEvent(aggregated_root.EventSourceId);
            event_envelope = new Mock<IEventEnvelope>();
            event_envelope.SetupGet(e => e.EventSourceId).Returns(aggregated_root.EventSourceId);
            event_envelopes.Setup(e => e.CreateFrom(aggregated_root, uncommitted_event)).Returns(event_envelope.Object);
            aggregated_root.Apply(uncommitted_event);
        };
    }
}
