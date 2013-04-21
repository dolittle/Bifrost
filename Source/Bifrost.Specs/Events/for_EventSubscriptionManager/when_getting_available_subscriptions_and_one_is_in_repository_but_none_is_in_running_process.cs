using System.Collections.Generic;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_getting_available_subscriptions_and_one_is_in_repository_but_none_is_in_running_process : given.an_event_subscription_manager_with_one_subscriber_from_repository
    {
        static IEnumerable<EventSubscription> subscriptions;

        Because of = () => subscriptions = event_subscription_manager.GetAvailableSubscriptions();

        It should_return_no_subscriptions = () => subscriptions.ShouldBeEmpty();
    }
}
