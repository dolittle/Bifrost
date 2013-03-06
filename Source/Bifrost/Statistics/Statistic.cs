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
using System;
using System.Collections.Generic;

namespace Bifrost.Statistics
{
    /// <summary>
    /// A statistic
    /// </summary>
    public class Statistic : IStatistic
    {
        string _defaultContext;

        /// <summary>
        /// Statistic constructor
        /// </summary>
        /// <param name="defaultContext">The default context for generating statistics</param>
        public Statistic(string defaultContext)
        {
            if (string.IsNullOrEmpty(defaultContext))
                throw new ArgumentNullException("defaultContext");
            _defaultContext = defaultContext;
            Categories = new Dictionary<string, ICollection<string>>();
        }

        /// <summary>
        /// The categories for this statistic
        /// </summary>
        public IDictionary<string, ICollection<string>> Categories { get; private set; }

        /// <summary>
        /// Record a category against this statistic
        /// </summary>
        /// <param name="category">The category</param>
        /// <param name="context">The context of this category</param>
        public void Record(string context, string category)
        {
            ICollection<string> categories = new List<string>();
            if (Categories.ContainsKey(context))
            {
                categories = Categories[context];
                categories.Add(category);
                Categories[context] = categories;
            }
            else
            {
                categories.Add(category);
                Categories.Add(new KeyValuePair<string, ICollection<string>>(context, categories));
            }
        }

        /// <summary>
        /// Record a category against this statistic
        /// </summary>
        /// <param name="category">The category</param>
        public void Record(string category)
        {
            Record(_defaultContext, category);
        }

        /// <summary>
        /// Sets the current context for recording statistics;
        /// </summary>
        /// <param name="context"></param>
        public void SetContext(string context)
        {
            if (string.IsNullOrEmpty(context))
                throw new ArgumentNullException("context");
            _defaultContext = context;
        }
    }
}
