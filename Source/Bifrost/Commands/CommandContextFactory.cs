/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Sagas;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandContextFactory"/>
    /// </summary>
    public class CommandContextFactory : ICommandContextFactory
    {
        readonly IUncommittedEventStreamCoordinator _uncommittedEventStreamCoordinator;
        readonly IEventStore _eventStore;
        readonly ISagaLibrarian _sagaLibrarian;
        readonly IProcessMethodInvoker _processMethodInvoker;
        readonly IExecutionContextManager _executionContextManager;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandContextFactory">CommandContextFactory</see>
        /// </summary>
        /// <param name="uncommittedEventStreamCoordinator">A <see cref="IUncommittedEventStreamCoordinator"/> to use for coordinator an <see cref="UncommittedEventStream"/></param>
        /// <param name="sagaLibrarian">A <see cref="ISagaLibrarian"/> for saving sagas to</param>
        /// <param name="processMethodInvoker">A <see cref="IProcessMethodInvoker"/> for processing events</param>
        /// <param name="executionContextManager">A <see cref="IExecutionContextManager"/> for getting execution context from</param>
        /// <param name="eventStore">A <see cref="IEventStore"/> that will receive any events generated</param>
        public CommandContextFactory(
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

#pragma warning disable 1591 // Xml Comments
        public ICommandContext Build(ICommand command)
        {
            return new CommandContext(
                command,
                _executionContextManager.Current,
                _eventStore,
                _uncommittedEventStreamCoordinator
                );
        }

        public ICommandContext Build(ISaga saga, ICommand command)
        {
            return new SagaCommandContext(
                saga,
                command,
                _executionContextManager.Current,
                _eventStore,
                _uncommittedEventStreamCoordinator,
                _processMethodInvoker,
                _sagaLibrarian
                );
        }
#pragma warning restore 1591 // Xml Comments
    }
}