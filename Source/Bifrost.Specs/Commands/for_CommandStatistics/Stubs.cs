using Bifrost.Commands;
using Bifrost.Statistics;
using System.Collections.Generic;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class DummyStatisticsPlugin : ICanRecordStatisticsForCommand
    {
        public void Record(CommandResult commandResult, IStatistic statistic)
        {

            if (!commandResult.PassedSecurity)
                statistic.Record(this.GetType().Name, "I touched a did not pass security statistic");

            if (commandResult.Success)
                statistic.Record(this.GetType().Name, "I touched a was handled statistic");

            if (commandResult.HasException)
                statistic.Record(this.GetType().Name, "I touched a had exception statistic");

            if (commandResult.Invalid)
                statistic.Record(this.GetType().Name, "I touched a had validation error statistic");

            statistic.Record(this.GetType().Name, "I recorded a command");
        }
    }

    public class DummyStatisticsPluginWithNoEffect : ICanRecordStatisticsForCommand
    {
        public void Record(CommandResult commandResult, IStatistic statistic)
        {
        }
    }
}
