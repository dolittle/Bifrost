using Bifrost.Specs.Commands.for_CommandHandlerInvoker;
using Bifrost.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class DummyStatisticsPlugin : IStatisticsPlugin
    {
        public string Context
        {
            get { return "DummyStatisticsPluginContext"; }
        }

        public ICollection<string> Categories
        {
            get { return new List<string>() { "ImADummy" }; }
        }

        public bool WasHandled(Bifrost.Commands.Command command)
        {
            return true;
        }

        public bool HadException(Bifrost.Commands.Command command)
        {
            return true;
        }
    }
}
