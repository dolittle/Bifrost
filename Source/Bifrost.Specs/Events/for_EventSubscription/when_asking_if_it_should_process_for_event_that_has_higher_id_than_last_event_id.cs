using Bifrost.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventSubscription
{
    public class when_asking_if_it_should_process_for_event_that_has_higher_id_than_last_event_id
    {
        static EventSubscription    subscription;
        static EventAndEnvelope event_and_envelope;
        static bool result;

        Establish context = () =>
        {
            subscription = new EventSubscription
            {
                LastEventId = 2
            };

            var @event = new Mock<IEvent>();
            var event_envelope = new Mock<IEventEnvelope>();
            event_envelope.SetupGet(e => e.EventId).Returns(3);
            event_and_envelope = new EventAndEnvelope(event_envelope.Object, @event.Object);
        };

        Because of = () => result = subscription.CanProcess(event_and_envelope);

        It should_be_able_to_process = () => result.ShouldBeTrue();
    }
}
