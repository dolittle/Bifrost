using Bifrost.Commands;
using Bifrost.Statistics;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.for_StatisticsConfiguration
{
    public class when_setting_store_type_and_initializing : given.a_statistics_configuration_and_container
    {
        Because of = () =>
        {
            statistics_configuration.StoreType = typeof(NullStatisticsStore);
            statistics_configuration.Initialize(container_mock.Object);
        };

        It should_bind_the_implementation = () =>
        {
            container_mock.Verify(c => c.Bind<ICommandStatistics>(
                typeof(CommandStatistics),
                Bifrost.Execution.BindingLifecycle.Singleton), Moq.Times.Once());
        };
      

        It should_bind_the_category_store = () =>
        {
            container_mock.Verify(c => c.Bind<IStatisticsStore>(
                statistics_configuration.StoreType,
                Bifrost.Execution.BindingLifecycle.Singleton), Moq.Times.Once());
        };
    }
}
