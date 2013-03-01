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
        public Statistic()
        {
            Categories = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// The categories for this statistic
        /// </summary>
        public ICollection<KeyValuePair<string, string>> Categories { get; private set; }

        /// <summary>
        /// Record a category against this statistic
        /// </summary>
        /// <param name="category">The category</param>
        /// <param name="context">The context of this category</param>
        public void Record(string context, string category)
        {
            Categories.Add(new KeyValuePair<string,string>(context, category));
        }
    }
}
