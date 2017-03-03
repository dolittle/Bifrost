using Bifrost.Configuration;
using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_EventsConfiguration.given
{
    public class an_events_configuration
    {
        protected static EventsConfiguration events_configuration;

        Establish context = () =>
        {
            events_configuration = new EventsConfiguration();
        };
    }
}
