/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 namespace Bifrost.Events
{
    /// <summary>
    /// Defines a system that knows about <see cref="IEventProcessor">event processors</see>
    /// </summary>
    public interface IEventProcessors
    {
        /// <summary>
        /// Process an <see cref="IEvent">event</see>
        /// </summary>
        /// <param name="event"><see cref="IEvent">Event</see> to process</param>
        /// <returns><see cref="IEventProcessorResults">Results</see> from processing</returns>
        IEventProcessorResults Process(IEvent @event);
    }
}
