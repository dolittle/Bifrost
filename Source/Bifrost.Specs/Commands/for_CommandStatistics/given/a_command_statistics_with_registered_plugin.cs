using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost;
using Machine.Specifications;
using Bifrost.Statistics;
using Bifrost.Execution;

namespace Bifrost.Specs.Commands.for_CommandStatistics.given
{
    public class a_command_statistics_with_registered_plugin
    {
        protected static ICommandStatistics command_statistics;
        protected static Moq.Mock<IStatisticsStore> statistics_store;
        protected static Moq.Mock<ITypeDiscoverer> type_discoverer; 

        Establish context = () =>
        {
            type_discoverer = new Moq.Mock<ITypeDiscoverer>();
            type_discoverer.Setup(t =>t.FindMultiple<ICommandStatistics>()).Returns(() => { return new [] { typeof(DummyStatisticsPlugin) }; });

            statistics_store = new Moq.Mock<IStatisticsStore>();
            command_statistics = new CommandStatistics(statistics_store.Object, type_discoverer.Object);
        };
    }
}
