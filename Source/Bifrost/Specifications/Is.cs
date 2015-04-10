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
namespace Bifrost.Specifications
{
    /// <summary>
    /// Helps chain simple <see cref="Specification{T}"/> together
    /// </summary>
    public static class Is
    {
        /// <summary>
        /// Creates a Not rule based on the rule passed in.
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="rule">The rule being extended</param>
        /// <returns>A Not{T} rule"></returns>
        public static Specification<T> Not<T>(Specification<T> rule)
        {
            return new Negative<T>(rule);
        }
    }
}
