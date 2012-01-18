using System;
using System.Linq;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository
{
    public class when_updating_a_subscription : given.an_event_subscription_repository
    {
        static EventSubscription subscription;
        static EventSubscriptionHolder subscription_holder;
        static Type owner_type;
        static Type event_type;

        Establish context = () =>
        {
            owner_type = typeof(Subscribers);
            event_type = typeof(SimpleEvent);
            subscription = new EventSubscription
            {
                Owner = owner_type,
                Method = owner_type.GetMethod(ProcessMethodInvoker.ProcessMethodName, new[] { event_type }),
                EventType = event_type,
                EventName = event_type.Name
            };
            var holder = new EventSubscriptionHolder
            {
                Owner = subscription.Owner.AssemblyQualifiedName,
                Method = subscription.Method.Name,
                EventType = subscription.EventType.AssemblyQualifiedName,
                EventName = subscription.EventName
            };

            entity_context_mock.Setup(e => e.Entities).Returns(new[] { holder }.AsQueryable());
            entity_context_mock.Setup(e => e.Update(Moq.It.IsAny<EventSubscriptionHolder>())).Callback((EventSubscriptionHolder h) => subscription_holder = h);
        };


        Because of = () => repository.Update(subscription);

        It should_update_holder_with_correct_owner_type = () => subscription_holder.Owner.ShouldEqual(owner_type.AssemblyQualifiedName);
        It should_update_holder_with_correct_method = () => subscription_holder.Method.ShouldEqual(ProcessMethodInvoker.ProcessMethodName);
        It should_update_holder_with_correct_event_type = () => subscription_holder.EventType.ShouldEqual(event_type.AssemblyQualifiedName);
        It should_update_holder_with_correct_event_name = () => subscription_holder.EventName.ShouldEqual(event_type.Name);
    }
}
