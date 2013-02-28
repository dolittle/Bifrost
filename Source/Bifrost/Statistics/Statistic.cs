using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Statistics
{
    /// <summary>
    /// A statistic
    /// </summary>
    public class Statistic : IStatistic
    {
        /// <summary>
        /// Statistic Constructor
        /// </summary>
        /// <param name="category">The category for this statistic</param>
        public Statistic(string category)
        {
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("category");
            Category = category;
        }

        /// <summary>
        /// The category for this statistic
        /// </summary>
        public string Category { get; private set; }
    }
}
