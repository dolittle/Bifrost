using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost;
using Machine.Specifications;
using Bifrost.Statistics;

namespace Bifrost.Specs.Commands.for_CommandStatistics.given
{
    public class a_command_statistics
    {
        protected static ICommandStatistics command_statistics;
        protected static Moq.Mock<IStatisticsStore> statistics_store;

        Establish context = () =>
        {
            statistics_store = new Moq.Mock<IStatisticsStore>();
            command_statistics = new CommandStatistics(statistics_store.Object);
        };
    }
}
