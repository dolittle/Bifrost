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
using System;
using Bifrost.Events;
using Bifrost.Sagas;
using Bifrost.Execution;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandContextManager">Command context manager</see>
    /// </summary>
    public class CommandContextManager : ICommandContextManager
    {
        IUncommittedEventStreamCoordinator _uncommittedEventStreamCoordinator;
        IEventStore _eventStore;
        ISagaLibrarian _sagaLibrarian;
        IProcessMethodInvoker _processMethodInvoker;
        IExecutionContextManager _executionContextManager;

        [ThreadStatic] static ICommandContext _currentContext;

        /// <summary>
        /// Reset context
        /// </summary>
        public static void ResetContext()
        {
            _currentContext = null;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CommandContextManager">CommandContextManager</see>
        /// </summary>
        /// <param name="uncommittedEventStreamCoordinator">A <see cref="IUncommittedEventStreamCoordinator"/> to use for coordinator an <see cref="UncommittedEventStream"/></param>
        /// <param name="sagaLibrarian">A <see cref="ISagaLibrarian"/> for saving sagas to</param>
        /// <param name="processMethodInvoker">A <see cref="IProcessMethodInvoker"/> for processing events</param>
        /// <param name="executionContextManager">A <see cref="IExecutionContextManager"/> for getting execution context from</param>
        /// <param name="eventStore">A <see cref="IEventStore"/> that will receive any events generated</param>
        public CommandContextManager(
            IUncommittedEventStreamCoordinator uncommittedEventStreamCoordinator,
            ISagaLibrarian sagaLibrarian,
            IProcessMethodInvoker processMethodInvoker,
            IExecutionContextManager executionContextManager,
            IEventStore eventStore)
        {
            _uncommittedEventStreamCoordinator = uncommittedEventStreamCoordinator;
            _sagaLibrarian = sagaLibrarian;
            _processMethodInvoker = processMethodInvoker;
            _eventStore = eventStore;
            _executionContextManager = executionContextManager;
        }

        private static bool IsInContext(ICommand command)
        {
            var inContext = null != _currentContext && _currentContext.Command.Equals(command);
            return inContext;
        }

#pragma warning disable 1591 // Xml Comments
        public bool HasCurrent
        {
            get { return _currentContext != null; }
        }

        public ICommandContext GetCurrent()
        {
            if (!HasCurrent)
            {
                throw new InvalidOperationException(ExceptionStrings.CommandNotEstablished);
            }
            return _currentContext;
        }

        public ICommandContext EstablishForCommand(ICommand command)
        {
            if (!IsInContext(command))
            {
                var commandContext = new CommandContext(
                    command,
                    _executionContextManager.Current,
                    _eventStore,
                    _uncommittedEventStreamCoordinator
                    );

                _currentContext = commandContext;
            }
            return _currentContext;
        }

        public ICommandContext EstablishForSaga(ISaga saga, ICommand command)
        {
            if (!IsInContext(command))
            {
                var commandContext = new SagaCommandContext(
                        saga,
                        command,
                        _executionContextManager.Current,
                        _eventStore,
                        _uncommittedEventStreamCoordinator,
                        _processMethodInvoker,
                        _sagaLibrarian
                    );

                _currentContext = commandContext;
            }
            return _currentContext;
        }
#pragma warning restore 1591 // Xml Comments
    }
}