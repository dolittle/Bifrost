using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Statistics
{
    /// <summary>
    /// A statistics store
    /// </summary>
    public interface IStatisticsStore
    {
        /// <summary>
        /// Adds a statistic
        /// </summary>
        /// <param name="statistic">The statistic</param>
        void Add(IStatistic statistic);
    }
}
