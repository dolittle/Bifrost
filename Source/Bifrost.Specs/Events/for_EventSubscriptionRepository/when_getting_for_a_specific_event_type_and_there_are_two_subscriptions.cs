using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository
{
    public class when_getting_for_a_specific_event_type_and_there_are_two_subscriptions_for_different_event_types : given.an_event_subscription_repository_with_two_subscriptions_for_different_event_types
    {
        static IEnumerable<EventSubscription> result;

        Because of = () => result = repository.GetForEvent(typeof(SimpleEvent));

        It should_return_one_event = () => result.Count().ShouldEqual(1);
    }
}
