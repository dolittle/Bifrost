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
            _statisticsPlugins = _typeDiscoverer.FindMultiple<ICanGenerateStatisticsForCommand>();
        }

        /// <summary>
        /// Add a command that was handled to statistics
        /// </summary>
        /// <param name="command">The command</param>
        public void WasHandled(ICommand command)
        {
            CheckCommand(command);

            // record a handled command statistic
            var statistic = new Statistic();
            statistic.Record("CommandStatistics", "WasHandled");

            HandlePlugin(command, statistic, (p, c) => p.WasHandled(command));

            _statisticsStore.Add(statistic);
        }

        /// <summary>
        /// Add a command that had an exception to statistics
        /// </summary>
        /// <param name="command">The command</param>
        public void HadException(ICommand command)
        {
            CheckCommand(command);

            // record a had exception command statistic
            var statistic = new Statistic();
            statistic.Record("CommandStatistics", "HadException");

            HandlePlugin(command, statistic, (p, c) => p.HadException(command));

            _statisticsStore.Add(statistic);
        }

        /// <summary>
        /// Add a command that had a validation error to statistics
        /// </summary>
        /// <param name="command">The command</param>
        public void HadValidationError(ICommand command)
        {
            CheckCommand(command);

            // record a had validation error command statistic
            var statistic = new Statistic();
            statistic.Record("CommandStatistics", "HadValidationError");

            HandlePlugin(command, statistic, (p, c) => p.HadValidationError(command));

            _statisticsStore.Add(statistic);
        }

        /// <summary>
        /// Adds a command that did not pass security to statistics
        /// </summary>
        /// <param name="command"></param>
        public void DidNotPassSecurity(ICommand command)
        {
            CheckCommand(command);

            // record a had did not pass security command statistic
            var statistic = new Statistic();
            statistic.Record("CommandStatistics", "DidNotPassSecurity");

            HandlePlugin(command, statistic, (p, c) => p.DidNotPassSecurity(command));

            _statisticsStore.Add(statistic);
        }

        private void HandlePlugin(ICommand command, IStatistic statistic, Expression<Func<ICanGenerateStatisticsForCommand, ICommand, bool>> action)
        {
            // let plugins record their statistics
            _statisticsPlugins.ToList().ForEach(type =>
            {
                var constructor = Expression.Lambda(Expression.New(type.GetConstructor(Type.EmptyTypes))).Compile();
                var plugin = (ICanGenerateStatisticsForCommand)constructor.DynamicInvoke();

                if (action.Compile().Invoke(plugin, command))
                {
                    var categories = plugin.Categories;
                    categories.ToList().ForEach(c => statistic.Record(plugin.Context, c));
                }
            });
        }

        private void CheckCommand(ICommand command)
        {
            if (command == null) throw new ArgumentNullException("command");
        }
    }
}
