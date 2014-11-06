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

namespace Bifrost.Rules
{
    /// <summary>
    /// Defines a system for managing <see cref="IRuleContext">rule contexts</see>
    /// </summary>
    public interface IRuleContexts
    {
        /// <summary>
        /// Get a <see cref="IRuleContext"/> for a given instance
        /// </summary>
        /// <param name="instance">Instance to get for</param>
        /// <returns><see cref="IRuleContext"/> to use</returns>
        IRuleContext GetFor(object instance);
    }
}
