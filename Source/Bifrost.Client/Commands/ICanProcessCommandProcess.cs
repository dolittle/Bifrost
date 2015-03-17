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

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines something that knows about handling <see cref="ICommandProcess"/>
    /// </summary>
    public interface ICanProcessCommandProcess
    {
        /// <summary>
        /// Add <see cref="CommandSucceeded"/> callback
        /// </summary>
        /// <param name="callback"><see cref="CommandSucceeded">Callback</see> to add</param>
        void AddSucceeded(CommandSucceeded callback);

        /// <summary>
        /// Add <see cref="CommandFailed"/> callback
        /// </summary>
        /// <param name="callback"><see cref="CommandFailed">Callback</see> to add</param>
        void AddFailed(CommandFailed callback);

        /// <summary>
        /// Add <see cref="CommandHandled"/> callback
        /// </summary>
        /// <param name="callback"><see cref="CommandHandled">Callback</see> to add</param>
        void AddHandled(CommandHandled callback);
        
        /// <summary>
        /// Handle the command and its result
        /// </summary>
        /// <param name="command"><see cref="ICommand"/> to handle</param>
        /// <param name="result"><see cref="CommandResult"/> for the <see cref="ICommand"/></param>
        void Process(ICommand command, CommandResult result);
    }
}
