using Bifrost.Configuration;
using Machine.Specifications;

namespace Bifrost.NHibernate.Specs.for_ConfigurationExtension
{
    public class when_using_nhibernate_statistics_store : given.a_statistics_configuration_and_container
    {
        Because of = () => { statistics_configuration.UsingNHibernate(); };

        It should_configure_the_store_to_an_nhibernate_implementation = () =>
        {
            statistics_configuration.StoreType.FullName.ShouldBeTheSameAs(typeof(NHibernate.Statistics.StatisticsStore).FullName);
        };
    }
}
