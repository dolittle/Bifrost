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

namespace Bifrost.Commands
{
    /// <summary>
    /// Command Statistics
    /// </summary>
    public interface ICommandStatistics
    {
        /// <summary>
        /// Adds a command that was handled to statistics
        /// </summary>
        /// <param name="command">The command</param>
        void WasHandled(ICommand command);

        /// <summary>
        /// Adds a command that had an exception to statistics
        /// </summary>
        /// <param name="command"></param>
        void HadException(ICommand command);

        /// <summary>
        /// Add a command that had a validation error to statistics
        /// </summary>
        /// <param name="command">The command</param>
        void HadValidationError(ICommand command);
        
        /// <summary>
        /// Adds a command that did not pass security to statistics
        /// </summary>
        /// <param name="command"></param>
        void DidNotPassSecurity(ICommand command);
    }
}
