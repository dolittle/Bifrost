#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
namespace Bifrost.Rules
{
    /// <summary>
    /// Represents a reason for why a <see cref="IRule"/> is broken
    /// </summary>
    public class BrokenRuleReason
    {
        /// <summary>
        /// Gets the identifier for the <see cref="BrokenRuleReason"/>
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Private constructor - so we can't instantiate these without going through create
        /// </summary>
        BrokenRuleReason() { }

        /// <summary>
        /// Creates a new instance of <see cref="BrokenRuleReason"/> from a given unique identifier
        /// </summary>
        /// <param name="id">Unique identifier of the reason - this has to be a valid Guid in string format</param>
        /// <returns>A <see cref="BrokenRuleReason"/> instance</returns>
        /// <remarks>
        /// The format of the Guid has to be : 
        /// 00000000-0000-0000-0000-000000000000
        /// </remarks>
        public static BrokenRuleReason  Create(string id)
        {
            return new BrokenRuleReason
            {
                Id = Guid.Parse(id)
            };
        }
    }
}
