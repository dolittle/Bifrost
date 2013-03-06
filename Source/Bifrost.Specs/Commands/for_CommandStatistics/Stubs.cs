using Bifrost.Commands;
using Bifrost.Statistics;
using System.Collections.Generic;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class DummyStatisticsPlugin : ICanRecordStatisticsForCommand
    {
        readonly IList<string> _categories;
        public DummyStatisticsPlugin()
        {
            _categories = new List<string>();
        }

        public string Context
        {
            get { return "DummyStatisticsPluginContext"; }
        }

        public ICollection<string> Categories
        {
            get { return _categories; }
        }

        public bool Record(CommandResult commandResult)
        {

            if (!commandResult.PassedSecurity)
                _categories.Add("I touched a did not pass security statistic");

            if (commandResult.Success)
                _categories.Add("I touched a was handled statistic");

            if (commandResult.HasException)
                _categories.Add("I touched a had exception statistic");

            if (commandResult.Invalid)
                _categories.Add("I touched a had validation error statistic");

            _categories.Add("I recorded a command");
            return true;
        }
    }

    public class DummyStatisticsPluginWithNoEffect : ICanRecordStatisticsForCommand
    {
        readonly IList<string> _categories;
        public DummyStatisticsPluginWithNoEffect()
        {
            _categories = new List<string>();
        }

        public string Context
        {
            get { return "DummyStatisticsPluginContext"; }
        }

        public ICollection<string> Categories
        {
            get { return _categories; }
        }

        public bool Record(CommandResult commandResult)
        {
            _categories.Add("I recorded a command");
            return false;
        }
    }
}
