#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
            _statisticsPlugins = _typeDiscoverer.FindMultiple<ICanRecordStatisticsForCommand>();
        }

        /// <summary>
        /// Record statistics about a command result
        /// </summary>
        /// <param name="commandResult">The command result</param>
        public void Record(CommandResult commandResult)
        {
            CheckCommand(commandResult);

            // record a handled command statistic
            var statistic = new Statistic();
            statistic.Record("CommandStatistics", "WasHandled");

            HandlePlugin(commandResult, statistic, (p, c) => p.Record(commandResult));

            _statisticsStore.Add(statistic);
        }

        private void HandlePlugin(CommandResult commandResult, IStatistic statistic, Expression<Func<ICanRecordStatisticsForCommand, CommandResult, bool>> action)
        {
            // let plugins record their statistics
            _statisticsPlugins.ToList().ForEach(type =>
            {
                var constructor = Expression.Lambda(Expression.New(type.GetConstructor(Type.EmptyTypes))).Compile();
                var plugin = (ICanRecordStatisticsForCommand)constructor.DynamicInvoke();

                if (action.Compile().Invoke(plugin, commandResult))
                {
                    var categories = plugin.Categories;
                    categories.ToList().ForEach(c => statistic.Record(plugin.Context, c));
                }
            });
        }

        private void CheckCommand(CommandResult commandResult)
        {
            if (commandResult == null) throw new ArgumentNullException("commandResult");
        }
    }
}
