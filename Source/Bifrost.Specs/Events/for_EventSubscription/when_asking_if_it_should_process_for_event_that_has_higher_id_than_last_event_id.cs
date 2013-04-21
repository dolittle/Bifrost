using Bifrost.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventSubscription
{
    public class when_asking_if_it_should_process_for_event_that_has_higher_id_than_last_event_id
    {
        static EventSubscription    subscription;
        static Mock<IEvent> @event_mock;
        static bool result;

        Establish context = () =>
        {
            subscription = new EventSubscription
            {
                LastEventId = 2
            };

            @event_mock = new Mock<IEvent>();
            @event_mock.SetupGet(e => e.Id).Returns(3);
        };

        Because of = () => result = subscription.ShouldProcess(@event_mock.Object);

        It should_process = () => result.ShouldBeTrue();
    }
}
