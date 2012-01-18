using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository
{
    public class when_getting_all_subscribers_and_there_are_two_available : given.an_event_subscription_repository_with_two_subscriptions_for_different_event_types
    {
        static IEnumerable<EventSubscription>   result;

        Because of = () => result = repository.GetAll();

        It should_return_two_subscriptions = () => result.Count().ShouldEqual(2);
    }
}
