/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Defines a system that knows about state related to <see cref="IEventProcessor">event processors</see>
    /// </summary>
    public interface IEventProcessorStates
    {
        /// <summary>
        /// Get the <see cref="IEventProcessorState">state</see> for a specific <see cref="IEventProcessor"/>
        /// </summary>
        /// <param name="eventProcessor"><see cref="IEventProcessor"/> to get state for</param>
        /// <returns>The <see cref="IEventProcessorState">state</see> of the <see cref="IEventProcessorState"/></returns>
        IEventProcessorState GetFor(IEventProcessor eventProcessor);

        /// <summary>
        /// Report a successful processing for a <see cref="IEventProcessor"/> with a specific 
        /// <see cref="IEvent"/> and its <see cref="IEventEnvelope"/>
        /// </summary>
        /// <param name="eventProcessor"><see cref="IEventProcessor"/> to report for</param>
        /// <param name="event">The associated <see cref="IEvent"/></param>
        /// <param name="envelope">The associated <see cref="IEventEnvelope"/></param>
        void ReportSuccessFor(IEventProcessor eventProcessor, IEvent @event, IEventEnvelope envelope);

        /// <summary>
        /// Report a failed processing for a <see cref="IEventProcessor"/> with a specific 
        /// <see cref="IEvent"/> and its <see cref="IEventEnvelope"/>
        /// </summary>
        /// <param name="eventProcessor"><see cref="IEventProcessor"/> to report for</param>
        /// <param name="event">The associated <see cref="IEvent"/></param>
        /// <param name="envelope">The associated <see cref="IEventEnvelope"/></param>
        void ReportFailureFor(IEventProcessor eventProcessor, IEvent @event, IEventEnvelope envelope);
    }
}