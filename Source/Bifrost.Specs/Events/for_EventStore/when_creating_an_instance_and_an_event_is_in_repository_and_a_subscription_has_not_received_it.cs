using Machine.Specifications;
using Bifrost.Events;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventStore
{
    [Subject(typeof(EventStore))]
    public class when_creating_an_instance_and_an_event_is_in_repository_and_a_subscription_has_not_received_it : Globalization.given.a_localizer_mock
    {
        protected static EventStore    event_store;
        protected static Mock<IEventRepository> event_repository;
        protected static Mock<IEventStoreChangeManager> event_store_change_manager_mock;
        protected static Mock<IEventSubscriptionManager> event_subscription_manager_mock;

        Establish context = () => 
        {
            event_repository = new Mock<IEventRepository>();
            event_store_change_manager_mock = new Mock<IEventStoreChangeManager>();
            event_subscription_manager_mock = new Mock<IEventSubscriptionManager>();

            //event_repository.Setup(e=>e.
            
        };

        Because of = () => event_store = new EventStore(event_repository.Object, event_store_change_manager_mock.Object, event_subscription_manager_mock.Object, localizer_mock.Object);
        
        It should_delegate_the_processing_of_the_event_to_the_subscription_manager = () => { };
    }
}
