using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository
{
    public class when_getting_for_a_specific_event_type_and_there_is_no_subscriptions : given.an_event_subscription_repository
    {
        static IEnumerable<EventSubscription> result;

        Because of = () => result = repository.GetForEvent(typeof(SimpleEvent));

        It should_return_an_empty_collection = () => result.Count().ShouldEqual(0);
    }
}
