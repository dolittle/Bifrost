using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Configuration.for_Configure
{
    public class when_initializing : given.a_configure_instance
    {
        Because of = () => configure_instance.Initialize();

        It should_call_initialize_on_events_configuration = () => events_configuration.Verify(e => e.Initialize(configure_instance.Container), Times.Once());
        It should_call_initialize_on_views_configuration = () => views_configuration.Verify(e => e.Initialize(configure_instance.Container), Times.Once());
        It should_call_initialize_on_serialization_configuration = () => serialization_configuration.Verify(e => e.Initialize(configure_instance.Container), Times.Once());
        It should_call_initialize_on_commands_configuratuibn = () => commands_configuration.Verify(c => c.Initialize(configure_instance.Container), Times.Once());
        It should_call_initialize_on_default_storage = () => default_storage_configuration.Verify(d => d.Initialize(configure_instance.Container), Times.Once());
    }
}
