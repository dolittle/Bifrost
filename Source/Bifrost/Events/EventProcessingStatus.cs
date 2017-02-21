/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Represents the status of an <see cref="IEventProcessor"/>
    /// </summary>
    public enum EventProcessingStatus
    {
        /// <summary>
        /// The state an <see cref="IEventProcessor"/> is in when it has processed successfully
        /// </summary>
        Successful,

        /// <summary>
        /// The state an <see cref="IEventProcessor"/> is in when it is been given an event in the wrong sequence
        /// </summary>
        WrongSequence,

        /// <summary>
        /// The state an <see cref="IEventProcessor"/> is in when it has been given an event multiple times
        /// </summary>
        Duplicate,

        /// <summary>
        /// The state an <see cref="IEventProcessor"/> is in when it has problems processing
        /// </summary>
        Failed
    }
}
