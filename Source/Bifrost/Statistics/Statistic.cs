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
using System.Collections.Generic;

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
