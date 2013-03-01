using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bifrost
{
    /// <summary>
    /// Command statistics
    /// </summary>
    [Singleton]
    public class CommandStatistics : ICommandStatistics
    {
        readonly IStatisticsStore _statisticsStore;
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly ICollection<Type> _statisticsPlugins = new List<Type>();

        /// <summary>
        /// Constructor for the command statistics
        /// </summary>
        /// <param name="statisticsStore"></param>
        /// <param name="typeDiscoverer"></param>
        public CommandStatistics(IStatisticsStore statisticsStore, ITypeDiscoverer typeDiscoverer)
        {
            if (statisticsStore == null)
                throw new ArgumentNullException("statisticsStore");

            if (typeDiscoverer == null)
                throw new ArgumentNullException("typeDiscoverer");

            _statisticsStore = statisticsStore;
            _typeDiscoverer = typeDiscoverer;
            _statisticsPlugins = _typeDiscoverer.FindMultiple<ICommandStatistics>();
        }

        /// <summary>
        /// Add a command that was handled to statistics
        /// </summary>
        /// <param name="command">The command</param>
        public void WasHandled(Command command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            // record a handled command statistic
            var statistic = new Statistic();
            statistic.Record("CommandStatistics", "Handled");

            // let plugins record their statistics
            _statisticsPlugins.ToList().ForEach(s => 
                {
                    var constructor = Expression.Lambda(Expression.New(s.GetConstructor(Type.EmptyTypes))).Compile();
                    var plugin = (IStatisticsPlugin)constructor.DynamicInvoke();
                    
                    if (plugin.WasHandled(command))
                    {
                        var categories = plugin.Categories;
                        categories.ToList().ForEach(c => statistic.Record(plugin.Context, c));
                    }
                });

            _statisticsStore.Add(statistic);
        }
    }
}
