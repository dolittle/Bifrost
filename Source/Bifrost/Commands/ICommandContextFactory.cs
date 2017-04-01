/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Commands
{
    /// <summary>
    /// Creates <see cref="ICommandContext"/> for <see cref="ICommand"/>
    /// </summary>
    public interface ICommandContextFactory
    {
        /// <summary>
        /// Creates an <see cref="ICommandContext"/> for a specific <see cref="ICommand" />
        /// </summary>
        /// <param name="command"><see cref="ICommand" /> to create a context for.</param>
        /// <returns>An <see cref="ICommandContext"/> for the specified <see cref="ICommand"/></returns>
        ICommandContext Build(ICommand command);
    }
}