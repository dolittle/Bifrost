using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_getting_available_subscriptions_and_one_is_in_repository_and_in_running_process : given.an_event_subscription_manager_with_one_subscriber_from_repository_and_matching_in_process
    {
        static IEnumerable<EventSubscription> subscriptions;

        Establish context = () =>
        {
            subscription.LastEventId = 42;
            event_subscription_manager = new EventSubscriptionManager(event_subscription_repository_mock.Object, type_discoverer_mock.Object, container_mock.Object);
        };

        Because of = () => subscriptions = event_subscription_manager.GetAvailableSubscriptions();

        It should_return_one_subscription = () => subscriptions.Count().ShouldEqual(1);
        It should_have_last_event_id_set_to_value_from_repository = () => subscriptions.First().LastEventId.ShouldEqual(42);
    }
}
