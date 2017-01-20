using Bifrost.Configuration;
using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_EventsConfiguration.given
{
    public class an_events_configuration
    {
        protected static EventsConfiguration events_configuration;
        protected static Mock<IEventStoreChangeManager> event_store_change_manager_mock;

        Establish context = () =>
        {
            event_store_change_manager_mock = new Mock<IEventStoreChangeManager>();
            events_configuration = new EventsConfiguration(event_store_change_manager_mock.Object);
        };
    }
}
