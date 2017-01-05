/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Sagas;

namespace Bifrost.Commands
{
    /// <summary>
    /// Manages command contexts
    /// </summary>
    public interface ICommandContextManager
    {
        /// <summary>
        /// Gets whether or not we have a current command context
        /// </summary>
        bool HasCurrent { get; }

        /// <summary>
        /// Gets the current <see cref="ICommandContext">command context</see>, if any
        /// </summary>
        /// <returns>
        /// The current <see cref="ICommandContext">command context</see>.
        /// If there is no current context, it will throw an InvalidOperationException
        /// </returns>
        ICommandContext GetCurrent();

        /// <summary>
        /// Establish a <see cref="ICommandContext">command context</see> for a specific <see cref="ICommand">command</see>.
        /// This will be the current command context, unless something else establishes a new context
        /// </summary>
        /// <param name="command"><see cref="ICommand">Command</see> to establish for</param>
        /// <returns>Established context</returns>
        /// <remarks>
        /// The contexts are not stacked. So establishing two contexts after one another does not give you a chance to
        /// go back up the "stack".
        /// </remarks>
        ICommandContext EstablishForCommand(ICommand command);

        /// <summary>
        /// Establish a <see cref="ICommandContext">command context</see> for a specific <see cref="ICommand">command</see> in the
        /// context of a Saga.
        /// This will be the current command context, unless something else establishes a new context
        /// </summary>
        /// <param name="saga"><see cref="ISaga"/> to be in context of</param>
        /// <param name="command"><see cref="ICommand">Command</see> to establish for</param>
        /// <returns>Established context</returns>
        /// <remarks>
        /// The contexts are not stacked. So establishing two contexts after one another does not give you a chance to
        /// go back up the "stack".
        /// </remarks>
        ICommandContext EstablishForSaga(ISaga saga, ICommand command);
    }
}
