using Bifrost.Statistics;
using System.Collections.Generic;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class DummyStatisticsPlugin : ICanGenerateStatisticsForCommand
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

        public bool WasHandled(Bifrost.Commands.ICommand command)
        {
            _categories.Add("I touched a was handled statistic");
            return true;
        }

        public bool HadException(Bifrost.Commands.ICommand command)
        {
            _categories.Add("I touched a had exception statistic");
            return true;
        }

        public bool HadValidationError(Bifrost.Commands.ICommand command)
        {
            _categories.Add("I touched a had validation error statistic");
            return true;
        }

        public bool DidNotPassSecurity(Bifrost.Commands.ICommand command)
        {
            _categories.Add("I touched a did not pass security statistic");
            return true;
        }
    }

    public class DummyStatisticsPluginWithNoEffect : ICanGenerateStatisticsForCommand
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

        public bool WasHandled(Bifrost.Commands.ICommand command)
        {
            _categories.Add("I touched a was handled statistic");
            return false;
        }

        public bool HadException(Bifrost.Commands.ICommand command)
        {
            _categories.Add("I touched a had exception statistic");
            return false;
        }

        public bool HadValidationError(Bifrost.Commands.ICommand command)
        {
            _categories.Add("I touched a had validation error statistic");
            return false;
        }

        public bool DidNotPassSecurity(Bifrost.Commands.ICommand command)
        {
            _categories.Add("I touched a did not pass security statistic");
            return false;
        }
    }
}
