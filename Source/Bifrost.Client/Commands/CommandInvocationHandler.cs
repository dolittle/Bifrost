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
using Bifrost.Reflection;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="System.Windows.Input.ICommand"/> that knows how
    /// to handle an invocation coming through an interceptor
    /// </summary>
    public class CommandInvocationHandler : System.Windows.Input.ICommand, INeedTargetInstance
    {
        ICommandCoordinator _commandCoordinator;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandInvocationHandler"/>
        /// </summary>
        /// <param name="commandCoordinator"></param>
        public CommandInvocationHandler(ICommandCoordinator commandCoordinator)
        {
            _commandCoordinator = commandCoordinator;
        }


#pragma warning disable 1591 // Xml Comments
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = (s, e) => { };

        public void Execute(object parameter)
        {
            var command = TargetInstance.GetType().GetProperty("Instance").GetValue(TargetInstance) as ICommand;
            var result = _commandCoordinator.Handle(command);
        }

        public System.Windows.Input.ICommand TargetInstance { get; set; }

#pragma warning restore 1591 // Xml Comments
    }
}
