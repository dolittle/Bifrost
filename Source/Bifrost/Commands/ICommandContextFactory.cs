/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Sagas;

namespace Bifrost.Commands
{
    /// <summary>
    /// Creates <see cref="IComamndContext"/> for <see cref="ICommand"/>
    /// </summary>
    public interface ICommandContextFactory
    {
        /// <summary>
        /// Creates an <see cref="ICommandContext"/> for a specific <see cref="ICommand" />
        /// </summary>
        /// <param name="command"><see cref="ICommand" /> to create a context for.</param>
        /// <returns>An <see cref="ICommandContext"/> for the specified <see cref="ICommand"/></returns>
        ICommandContext Build(ICommand command);

        /// <summary>
        /// Creates an <see cref="ICommandContext"/> for a specific <see cref="ICommand" /> with the context of an <see cref="ISaga"/>
        /// </summary>
        /// <param name="saga"><see cref="ISaga" /> that the context is created in.</param>
        /// <param name="command"><see cref="ICommand" /> to create a context for.</param>
        /// <returns>An <see cref="ICommandContext"/> for the specified <see cref="ICommand"/> in the specified <see cref="ISaga"/> context</returns>
        ICommandContext Build(ISaga saga, ICommand command);
    }
}