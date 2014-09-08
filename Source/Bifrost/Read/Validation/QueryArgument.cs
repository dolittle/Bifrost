#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using System.Reflection;
using Bifrost.Rules;

namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents an argument on a query
    /// </summary>
    public class QueryArgument
    {
        /// <summary>
        /// Gets or sets the property info for the argument
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// Gets or sets the rules for the argument
        /// </summary>
        public IEnumerable<IRule> Rules { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public QueryArgumentValidationResult    Validate(IRuleContext context)
        {
            var result = new QueryArgumentValidationResult(null);

            return result;
        }
    }
}
