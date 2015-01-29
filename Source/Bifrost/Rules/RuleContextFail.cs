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
namespace Bifrost.Rules
{
    /// <summary>
    /// Delegate that gets called when a <see cref="IRule"/> fails
    /// </summary>
    /// <param name="rule"><see cref="IRule"/> that is failing</param>
    /// <param name="instance">Instance it was evaluating</param>
    /// <param name="reason"><see cref="BrokenRuleReason">Reason</see> for failing</param>
    public delegate void RuleFailed(IRule rule, object instance, BrokenRuleReason reason);
}
