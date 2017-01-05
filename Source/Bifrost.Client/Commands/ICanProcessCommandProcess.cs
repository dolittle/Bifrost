/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
