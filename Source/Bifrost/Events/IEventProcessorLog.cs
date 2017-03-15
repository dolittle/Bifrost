/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Defines a system for logging related to <see cref="IEventProcessor">event processors</see>
    /// </summary>
    public interface IEventProcessorLog
    {
        /// <summary>
        /// Report a failed <see cref="IEventProcessor"/> with the failing <see cref="IEvent"/>
        /// </summary>
        /// <param name="processor"><see cref="IEventProcessor"/> to report for</param>
        /// <param name="event"><see cref="IEvent"/> that was being processed</param>
        /// <param name="envelope"><see cref="IEventEnvelope"/> related to the <see cref="IEvent"/> being processed</param>
        void Failed(IEventProcessor processor, IEvent @event, IEventEnvelope envelope);
    }
}