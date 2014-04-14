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

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Severity of a <see cref="Problem"/>
    /// </summary>
    public enum ProblemSeverity
    {
        /// <summary>
        /// A suggestion meerly represents something for the developer to consider
        /// </summary>
        Suggestion,

        /// <summary>
        /// A warning should be considered something for the developer to really look into and most likely fix
        /// </summary>
        Warning,
        
        /// <summary>
        /// A problem marked as critical is something the developer should give immediate attention
        /// </summary>
        Critical
    }
}
