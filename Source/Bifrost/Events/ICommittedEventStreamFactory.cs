/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Defines a system that is capable of creating <see cref="CommittedEventStream"/>
    /// </summary>
    public interface ICommittedEventStreamFactory
    {
        /// <summary>
        /// Create a <see cref="CommittedEventStream"/> from a <see cref="UncommittedEventStream"/>
        /// </summary>
        /// <param name="eventStream"><see cref="UncommittedEventStream"/> to create from</param>
        /// <returns></returns>
        CommittedEventStream CreateFrom(UncommittedEventStream eventStream);
    }
}
