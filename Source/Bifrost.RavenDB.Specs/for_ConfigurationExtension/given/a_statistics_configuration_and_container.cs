using Bifrost.Configuration;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.RavenDB.Specs.for_ConfigurationExtension.given
{
    public class a_statistics_configuration_and_container
    {
        protected static StatisticsConfiguration statistics_configuration;
        protected static Mock<IContainer> container_mock;
        Establish context = () =>
        {
            container_mock = new Mock<IContainer>();
            statistics_configuration = new StatisticsConfiguration();
        };
    }
}
