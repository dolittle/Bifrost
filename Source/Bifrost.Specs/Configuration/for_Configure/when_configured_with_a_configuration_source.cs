using Bifrost.Configuration;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Configuration.for_Configure
{
    public class when_configured_with_a_configuration_source : given.a_configure_instance
    {
        static Mock<IConfigurationSource> configuration_source_mock;

            Establish context = () =>
                                {
                                    configuration_source_mock = new Mock<IConfigurationSource>();
                                    configure_instance.ConfigurationSource(configuration_source_mock.Object);
                                };

        Because of = () => configure_instance.Initialize();

        It should_call_initialize_on_configuration_source = () => configuration_source_mock.Verify(c => c.Initialize(configure_instance));
    }
}
