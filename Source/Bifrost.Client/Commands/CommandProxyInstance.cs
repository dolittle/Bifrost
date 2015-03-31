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
using System.Collections.Generic;
using Bifrost.Execution;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="IHoldCommandInstance"/>
    /// </summary>
    public class CommandProxyInstance : IHoldCommandInstance, ICanProcessCommandProcess
    {
        List<WeakDelegate> _commandSucceededCallbacks = new List<WeakDelegate>();
        List<WeakDelegate> _commandFailedCallbacks = new List<WeakDelegate>();
        List<WeakDelegate> _commandHandledCallbacks = new List<WeakDelegate>();


#pragma warning disable 1591 // Xml Comments
        public ICommand CommandInstance { get; set; }


        public void AddSucceeded(CommandSucceeded callback)
        {
            _commandSucceededCallbacks.Add(new WeakDelegate(callback));
        }

        public void AddFailed(CommandFailed callback)
        {
            _commandFailedCallbacks.Add(new WeakDelegate(callback));
        }

        public void AddHandled(CommandHandled callback)
        {
            _commandHandledCallbacks.Add(new WeakDelegate(callback));
        }

        public void Process(ICommand command, CommandResult result)
        {
            if (result.Success) On<CommandSucceeded>(_commandSucceededCallbacks, command, result, (d, c, r) => d(c, r));
            else On<CommandFailed>(_commandFailedCallbacks, command, result, (d, c, r) => d(c, r));
            On<CommandHandled>(_commandHandledCallbacks, command, result, (d, c, r) => d(c, r));
        }
#pragma warning restore 1591 // Xml Comments

        void On<T>(List<WeakDelegate> callbacks, ICommand command, CommandResult result, Action<T, ICommand, CommandResult> callbackCaller)
            where T : class
        {
            var forRemoval = new List<WeakDelegate>();
            callbacks.ForEach(r => InvokeCallback<T>(command, result, callbackCaller, forRemoval, r));
            forRemoval.ForEach(r => callbacks.Remove(r));
        }

        void InvokeCallback<T>(ICommand command, CommandResult result, Action<T, ICommand, CommandResult> callbackCaller, List<WeakDelegate> forRemove, WeakDelegate @delegate) where T : class
        {
            if (@delegate.IsAlive)
            {
                @delegate.DynamicInvoke(command, result);
            }
            else
            {
                forRemove.Add(@delegate);
            }
        }
    }
}
