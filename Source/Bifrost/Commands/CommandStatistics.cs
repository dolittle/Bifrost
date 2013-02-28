using Bifrost.Commands;
using Bifrost.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost
{
    /// <summary>
    /// Command statistics
    /// </summary>
    public class CommandStatistics : ICommandStatistics
    {
        readonly IStatisticsStore _statisticsStore;

        /// <summary>
        /// Constructor for the command statistics
        /// </summary>
        /// <param name="statisticsStore"></param>
        public CommandStatistics(IStatisticsStore statisticsStore)
        {
            if (statisticsStore == null)
                throw new ArgumentNullException("statisticsStore");
            _statisticsStore = statisticsStore;
        }
        /// <summary>
        /// Add a command that was handled to statistics
        /// </summary>
        /// <param name="command">The command</param>
        public void WasHandled(Command command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            // store a handled command statistic
            var statistic = new Statistic("Handled");
            _statisticsStore.Add(statistic);
        }
    }
}
