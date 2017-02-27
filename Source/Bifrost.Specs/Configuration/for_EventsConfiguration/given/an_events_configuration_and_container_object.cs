using Bifrost.Configuration;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_EventsConfiguration.given
{
    public class an_events_configuration_and_container_object : an_events_configuration
    {
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                };
    }
}