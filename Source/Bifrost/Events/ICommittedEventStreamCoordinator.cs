/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Defines the coordinator that coordinates <see cref="CommittedEventStream">committed event streams</see>
    /// </summary>
    public interface ICommittedEventStreamCoordinator
    {
        /// <summary>
        /// Initialize the coordinator
        /// </summary>
        void Initialize();
    }
}
