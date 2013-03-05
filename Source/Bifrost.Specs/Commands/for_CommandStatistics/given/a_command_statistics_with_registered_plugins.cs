using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Statistics;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandStatistics.given
{
    public class a_command_statistics_with_registered_plugins
    {
        protected static ICommandStatistics command_statistics;
        protected static Moq.Mock<IStatisticsStore> statistics_store;
        protected static Moq.Mock<ITypeDiscoverer> type_discoverer; 

        Establish context = () =>
        {
            type_discoverer = new Moq.Mock<ITypeDiscoverer>();
            type_discoverer.Setup(t =>t.FindMultiple<ICanRecordStatisticsForCommand>()).Returns(() => { 
                return new [] { typeof(DummyStatisticsPlugin), typeof(DummyStatisticsPluginWithNoEffect) }; 
            });

            statistics_store = new Moq.Mock<IStatisticsStore>();
            command_statistics = new CommandStatistics(statistics_store.Object, type_discoverer.Object);
        };
    }
}
