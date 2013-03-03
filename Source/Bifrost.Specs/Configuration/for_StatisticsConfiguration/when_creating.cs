using Bifrost.Configuration;
using Bifrost.Statistics;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.for_StatisticsConfiguration
{
    public class when_creating
    {
        static IStatisticsConfiguration statistics_configuration;

        Because of = () => { statistics_configuration = new StatisticsConfiguration(); };

        It should_configure_the_store_to_a_null_implementation = () =>
        {
            statistics_configuration.StoreType.FullName.ShouldBeTheSameAs(typeof(NullStatisticsStore).FullName);
        };
    }
}
