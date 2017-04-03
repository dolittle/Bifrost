/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// An EventSource is a domain object that is capable of generating and applying events.  It is an AggregateRoot in the context
    /// of event sourcing.
    /// </summary>
    public interface IEventSource
    {
		/// <summary>
		/// The Id of the Event Source.  
		/// </summary>
		EventSourceId EventSourceId { get; }

		/// <summary>
		/// Gets the version of this EventSource
		/// </summary>
		EventSourceVersion Version { get; }

		/// <summary>
		/// A stream of events that have been applied to the <seealso cref="EventSource">EventSource</seealso> but have not yet been committed to the EventStore.
		/// </summary>
		UncommittedEventStream UncommittedEvents { get; }

		/// <summary>
		/// Apply a new event to the EventSource.  This will be applied and added to the <see cref="UncommittedEvents">UncommitedEvents</see>.
		/// </summary>
		/// <param name="event">The event that is to be applied</param>
    	void Apply(IEvent @event);

		/// <summary>
		/// Reapply an event from a stream
		/// </summary>
		/// <param name="eventStream">Stream that contains the events to reapply</param>
    	void ReApply(CommittedEventStream eventStream);

        /// <summary>
        /// Fast forward to the specified version of the <seealso cref="EventSource">EventSource</seealso>
        /// </summary>
        /// <param name="version">Version to fast foward to</param>
        void FastForward(EventSourceVersion version);
    }
}