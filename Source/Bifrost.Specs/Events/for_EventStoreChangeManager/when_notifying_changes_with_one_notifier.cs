using Bifrost.Globalization;
using Moq;
using Machine.Specifications;
using It = Machine.Specifications.It;
using Bifrost.Events;


namespace Bifrost.Specs.Events.for_EventStoreChangeManager
{
    [Subject(typeof(EventStoreChangeManager))]
    public class when_notifying_changes_with_one_notifier : given.an_event_store_change_manager_with_one_notifier_registered
    {
        static Mock<IEventStore>    event_store_mock;

        Establish context = () => event_store_mock = new Mock<IEventStore>();

        Because of = () => event_store_change_manager.NotifyChanges(event_store_mock.Object);

        It should_forward_notification_to_the_notifier = () => notifier.NotifyCalled.ShouldBeTrue();
        It should_forward_event_store_to_the_notifier_during_notification = () => notifier.EventStore.ShouldEqual(event_store_mock.Object);
    }
}
