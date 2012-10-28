using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Configuration.for_Configure
{
    public class when_initializing : given.a_configure_instance
    {
        Because of = () => configure_instance.Initialize();

        It should_call_initialize_on_events_configuration = () => events_configuration_mock.Verify(e => e.Initialize(configure_instance), Times.Once());
        It should_call_initialize_on_views_configuration = () => views_configuration_mock.Verify(e => e.Initialize(configure_instance), Times.Once());
        It should_call_initialize_on_sagas_configuration = () => sagas_configuration_mock.Verify(e => e.Initialize(configure_instance), Times.Once());
		It should_call_initialize_on_serialization_configuration = () => serialization_configuration_mock.Verify(e => e.Initialize(configure_instance), Times.Once());
    }
}
