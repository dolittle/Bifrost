/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Defines a coordinator for dealing with <see cref="UncommittedEventStream"/>
    /// </summary>
    public interface IUncommittedEventStreamCoordinator
    {
        /// <summary>
        /// Commit a <see cref="UncommittedEventStream"/>
        /// </summary>
        /// <param name="eventStream"><see cref="UncommittedEventStream"/> to commit</param>
        void Commit(UncommittedEventStream eventStream);
    }
}
