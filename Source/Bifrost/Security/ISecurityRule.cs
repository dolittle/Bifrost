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

namespace Bifrost.Security
{
    /// <summary>
    /// Defines a rule for security
    /// </summary>
    public interface ISecurityRule
    {
        /// <summary>
        /// Check if a securable instance is authorized
        /// </summary>
        /// <param name="securable">The securable instance to check</param>
        /// <returns>True if has access, false if not</returns>
        bool IsAuthorized(object securable);

        /// <summary>
        /// Returns a description of the rule
        /// </summary>
        string Description { get; }

    }
}
