using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_getting_all_subscriptions_and_there_is_none_in_running_process : given.an_event_subscription_manager_with_one_subscriber_from_repository
    {
        static IEnumerable<EventSubscription> subscriptions;

        Because of = () => subscriptions = event_subscription_manager.GetAllSubscriptions();

        It should_return_one_subscription = () => subscriptions.Count().ShouldEqual(1);
    }
}
