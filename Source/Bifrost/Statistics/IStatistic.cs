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
        /// The categories for this statistic
        /// </summary>
        ICollection<KeyValuePair<string, string>> Categories { get; }

        /// <summary>
        /// Record a category against this statistic
        /// </summary>
        /// <param name="category">The category</param>
        /// <param name="context">The context of this category</param>
        void Record(string context, string category);
    }
}
