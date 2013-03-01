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
            CheckCommand(command);

            // record a handled command statistic
            var statistic = new Statistic();
            statistic.Record("CommandStatistics", "Handled");

            // let plugins record their statistics
            _statisticsPlugins.ToList().ForEach(plugin =>
                {
                    HandlePlugin(plugin, command, statistic, (p, c) => p.WasHandled(command));
                });

            _statisticsStore.Add(statistic);
        }

        /// <summary>
        /// Add a command that had an exception to statistics
        /// </summary>
        /// <param name="command">The command</param>
        public void HadException(Command command)
        {
            CheckCommand(command);

            // record a had exception command statistic
            var statistic = new Statistic();
            statistic.Record("CommandStatistics", "HadException");

            // let plugins record their statistics
            _statisticsPlugins.ToList().ForEach(plugin =>
                {
                    HandlePlugin(plugin, command, statistic, (p, c) => p.HadException(command));
                });

            _statisticsStore.Add(statistic);
        }

        private void HandlePlugin(Type type, Command command, IStatistic statistic, Expression<Func<IStatisticsPlugin, Command, bool>> action)
        {
            var constructor = Expression.Lambda(Expression.New(type.GetConstructor(Type.EmptyTypes))).Compile();
            var plugin = (IStatisticsPlugin)constructor.DynamicInvoke();

            if (action.Compile().Invoke(plugin, command))
            {
                var categories = plugin.Categories;
                categories.ToList().ForEach(c => statistic.Record(plugin.Context, c));
            }
        }

        private void CheckCommand(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");
        }
    }
}
