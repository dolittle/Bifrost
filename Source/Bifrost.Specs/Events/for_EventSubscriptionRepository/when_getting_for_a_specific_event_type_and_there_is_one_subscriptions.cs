using System.Linq;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository
{
    public class when_getting_for_a_specific_event_type_and_there_is_one_subscriptions : given.an_event_subscription_repository_with_one_subscription
    {
        static EventSubscription subscription;

        Because of = () => subscription = repository.GetForEvent(typeof(SimpleEvent)).Single();

        It should_get_subscription_with_correct_id = () => subscription.Id.ShouldEqual(subscription_holder.Id);
        It should_get_subscription_with_correct_last_event_id = () => subscription.LastEventId.ShouldEqual(subscription.LastEventId);
        It should_get_subscription_with_correct_owner_type = () => subscription.Owner.ShouldEqual(subscription.Owner);
        It should_get_subscription_with_correct_method = () => subscription.Method.Name.ShouldEqual(ProcessMethodInvoker.ProcessMethodName);
        It should_get_subscription_with_correct_event_type = () => subscription.EventType.ShouldEqual(subscription.EventType);
        It should_get_subscription_with_correct_event_name = () => subscription.EventName.ShouldEqual(subscription.EventName);
    }
}
