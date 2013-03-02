using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Statistics
{
    /// <summary>
    /// Represents a null implementation of <see cref="IStatisticsStore" />
    /// </summary>
    public class NullStatisticsStore : IStatisticsStore
    {
#pragma warning disable 1591 // Xml Comments
        public void Add(IStatistic statistic)
        {
        }
#pragma warning restore 1591 // Xml Comments
    }
}
