/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Lifecycle;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IEventSource">IEventSource</see>
    /// </summary>
    public class EventSource : IEventSource, ITransaction
    {
        /// <summary>
        /// Initializes an instance of <see cref="EventSource">EventSource</see>
        /// </summary>
        /// <param name="id"><see cref="Events.EventSourceId"/> of the event source</param>
        protected EventSource(EventSourceId id)
        {
            EventSourceId = id;
            UncommittedEvents = new UncommittedEventStream(this);
        }

        /// <inheritdoc/>
        public EventSourceId EventSourceId { get; set; }

        /// <inheritdoc/>
        public EventSourceVersion Version { get; private set; }

        /// <inheritdoc/>
        public UncommittedEventStream UncommittedEvents { get; private set; }

        /// <inheritdoc/>
        public void Apply(IEvent @event)
        {
            UncommittedEvents.Append(@event, Version);
            Version = Version.NextSequence();
            InvokeOnMethod(@event);
        }
        
        /// <inheritdoc/>
        public virtual void ReApply(CommittedEventStream eventStream)
        {
            ValidateEventStream(eventStream);

            foreach (var eventAndEnvelope in eventStream)
            {
                InvokeOnMethod(eventAndEnvelope.Event);
                Version = eventAndEnvelope.Envelope.Version;
            }

            Version = Version.NextCommit();
        }

        /// <inheritdoc/>
        public void FastForward(EventSourceVersion lastVersion)
        {
            ThrowIfStateful();
            ThrowIfNotInitialVersion();

            Version = lastVersion.NextCommit();
        }

        /// <inheritdoc/>
        public virtual void Commit()
        {
            UncommittedEvents = new UncommittedEventStream(this);
            Version = Version.NextCommit();
        }

        /// <inheritdoc/>
        public virtual void Rollback()
        {
            UncommittedEvents = new UncommittedEventStream(this);
            Version = Version.PreviousCommit();
        }

        /// <inheritdoc/>
		public void Dispose()
		{
			Commit();
		}

        void InvokeOnMethod(IEvent @event)
        {
            var handleMethod = this.GetOnMethod(@event);
            if (handleMethod != null)
                handleMethod.Invoke(this, new[] { @event });
        }

        void ValidateEventStream(CommittedEventStream eventStream)
        {
            if (!IsForThisEventSource(eventStream.EventSourceId))
                throw new InvalidOperationException("Cannot apply an EventStream belonging to a different event source." +
                    string.Format(@"Expected events for Id {0} but got events for Id {1}", EventSourceId, eventStream.EventSourceId));
        }

        bool IsForThisEventSource(Guid targetEventSourceId)
        {
            return targetEventSourceId == EventSourceId;
        }

        void ThrowIfStateful()
        {
            if (!this.IsStateless())
                throw new InvalidFastForwardException("Cannot fast forward stateful event source");
        }

        void ThrowIfNotInitialVersion()
        {
            if (!Version.Equals(EventSourceVersion.Zero))
                throw new InvalidFastForwardException("Cannot fast forward event source that is not an initial version");
        }
    }
}
