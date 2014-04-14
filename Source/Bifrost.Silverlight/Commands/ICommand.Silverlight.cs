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

namespace Bifrost.Commands
{
    public partial interface ICommand : System.Windows.Input.ICommand, INotifyCommandResultsReceived, INotifyEventsProcessed
    {
        /// <summary>
        /// Gets or sets the name of the command
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the parameters for the command
        /// </summary>
        dynamic Parameters { get; }

        /// <summary>
        /// Gets or sets wether or not the command is busy
        /// </summary>
        bool IsBusy { get; set; }

        /// <summary>
        /// Gets or sets wether or not the command is being processed
        /// </summary>
        bool IsProcessing { get; set; }

        /// <summary>
        /// Gets or sets the command coordinator used by the command when executing it
        /// </summary>
        ICommandCoordinator CommandCoordinator { get; set; }
    }
}
