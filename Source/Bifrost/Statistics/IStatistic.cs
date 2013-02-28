using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Statistics
{
    /// <summary>
    /// A statistic
    /// </summary>
    public interface IStatistic
    {
        /// <summary>
        /// The category for this statistic
        /// </summary>
        string Category { get; }
    }
}
