using Bifrost.Execution;
using Bifrost.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Configuration
{
    /// <summary>
    /// A statistics configuration implementation of <see cref="IStatisticsConfiguration"/>
    /// </summary>
    public class StatisticsConfiguration : IStatisticsConfiguration
    {
        /// <summary>
        /// Creates a new statistics configuration object
        /// </summary>
        public StatisticsConfiguration()
        {
            StoreType = typeof(NullStatisticsStore);
        }

        /// <summary>
        /// Initialize the configuration
        /// </summary>
        /// <param name="container">The container</param>
        public void Initialize(IContainer container)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The type of store used by statistics
        /// </summary>
        public Type StoreType { get; set; }
    }
}
