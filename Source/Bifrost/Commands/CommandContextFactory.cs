/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Events;
using Bifrost.Execution;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandContextFactory"/>
    /// </summary>
    public class CommandContextFactory : ICommandContextFactory
    {
        IUncommittedEventStreamCoordinator _uncommittedEventStreamCoordinator;
        IProcessMethodInvoker _processMethodInvoker;
        IExecutionContextManager _executionContextManager;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandContextFactory">CommandContextFactory</see>
        /// </summary>
        /// <param name="uncommittedEventStreamCoordinator">A <see cref="IUncommittedEventStreamCoordinator"/> to use for coordinator an <see cref="UncommittedEventStream"/></param>
        /// <param name="processMethodInvoker">A <see cref="IProcessMethodInvoker"/> for processing events</param>
        /// <param name="executionContextManager">A <see cref="IExecutionContextManager"/> for getting execution context from</param>
        public CommandContextFactory(
            IUncommittedEventStreamCoordinator uncommittedEventStreamCoordinator,
            IProcessMethodInvoker processMethodInvoker,
            IExecutionContextManager executionContextManager)
        {
            _uncommittedEventStreamCoordinator = uncommittedEventStreamCoordinator;
            _processMethodInvoker = processMethodInvoker;
            _executionContextManager = executionContextManager;
        }

        /// <inheritdoc/>
        public ICommandContext Build(ICommand command)
        {
            return new CommandContext(
                command,
                _executionContextManager.Current,
                _uncommittedEventStreamCoordinator
                );
        }
    }
}