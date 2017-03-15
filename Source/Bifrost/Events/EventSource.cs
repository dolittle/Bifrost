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
    ///
    /// This is a base abstract class for any EventSource
    /// </summary>
    public abstract class EventSource : IEventSource, ITransaction
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

#pragma warning disable 1591 // Xml Comments

        public EventSourceId EventSourceId { get; set; }

        public EventSourceVersion Version { get; private set; }

        public UncommittedEventStream UncommittedEvents { get; private set; }

        public void Apply(IEvent @event)
        {
            UncommittedEvents.Append(@event, Version);
            Version = Version.NextSequence();
        }

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

        public void FastForward(EventSourceVersion lastVersion)
        {
            ThrowIfStateful();
            ThrowIfNotInitialVersion();

            Version = lastVersion.NextCommit();
        }

        public virtual void Commit()
        {
            UncommittedEvents = new UncommittedEventStream(this);
            Version = Version.NextCommit();
        }


        public virtual void Rollback()
        {
            UncommittedEvents = new UncommittedEventStream(this);
            Version = Version.PreviousCommit();
        }

		public void Dispose()
		{
			Commit();
		}

#pragma warning restore 1591 // Xml Comments
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
