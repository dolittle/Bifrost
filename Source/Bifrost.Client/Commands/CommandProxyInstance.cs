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

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="IHoldCommandInstance"/>
    /// </summary>
    public class CommandProxyInstance : IHoldCommandInstance, ICanProcessCommandProcess
    {
        List<WeakReference<CommandSucceeded>> _commandSucceededCallbacks = new List<WeakReference<CommandSucceeded>>();
        List<WeakReference<CommandFailed>> _commandFailedCallbacks = new List<WeakReference<CommandFailed>>();
        List<WeakReference<CommandHandled>> _commandHandledCallbacks = new List<WeakReference<CommandHandled>>();


#pragma warning disable 1591 // Xml Comments
        public ICommand CommandInstance { get; set; }


        public void AddSucceeded(CommandSucceeded callback)
        {
            _commandSucceededCallbacks.Add(new WeakReference<CommandSucceeded>(callback));
        }

        public void AddFailed(CommandFailed callback)
        {
            _commandFailedCallbacks.Add(new WeakReference<CommandFailed>(callback));
        }

        public void AddHandled(CommandHandled callback)
        {
            _commandHandledCallbacks.Add(new WeakReference<CommandHandled>(callback));
        }

        public void Process(ICommand command, CommandResult result)
        {
            if (result.Success) On<CommandSucceeded>(_commandSucceededCallbacks, command, result, (d, c, r) => d(c, r));
            else On<CommandFailed>(_commandFailedCallbacks, command, result, (d, c, r) => d(c, r));
            On<CommandHandled>(_commandHandledCallbacks, command, result, (d, c, r) => d(c, r));
        }
#pragma warning restore 1591 // Xml Comments

        void On<T>(List<WeakReference<T>> callbacks, ICommand command, CommandResult result, Action<T, ICommand, CommandResult> callbackCaller)
            where T : class
        {
            var forRemoval = new List<WeakReference<T>>();
            callbacks.ForEach(r => InvokeCallback<T>(command, result, callbackCaller, forRemoval, r));
            forRemoval.ForEach(r => callbacks.Remove(r));
        }

        void InvokeCallback<T>(ICommand command, CommandResult result, Action<T, ICommand, CommandResult> callbackCaller, List<WeakReference<T>> forRemove, WeakReference<T> r) where T : class
        {
            T callback;
            if (r.TryGetTarget(out callback))
            {

                callbackCaller(callback, command, result);
            }
            else
            {
                forRemove.Add(r);
            }
        }
    }
}
