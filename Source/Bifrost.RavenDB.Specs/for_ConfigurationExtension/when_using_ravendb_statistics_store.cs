using Bifrost.Configuration;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConfigurationExtension
{
    public class when_using_ravendb_statistics_store : given.a_statistics_configuration_and_container
    {
        Because of = () => { statistics_configuration.UsingRavenDB(); };

        It should_configure_the_store_to_a_ravendb_implementation = () =>
        {
            statistics_configuration.StoreType.FullName.ShouldBeTheSameAs(typeof(RavenDB.Statistics.StatisticsStore).FullName);
        };
    }
}
