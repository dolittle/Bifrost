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
            UncommittedEvents = new UncommittedEventStream(id);
        }

        /// <summary>
        /// Gets or sets the <see cref="IEventEnvelopes"/> - a dependency that would normally be on the constructor, but these type of objects should
        /// not have dependencies on the constructor level as it bleeds through all over the place. This is therefor then required by systems creating
        /// an instance and working with <see cref="EventSource"/> to set this
        /// </summary>

        public IEventEnvelopes EventEnvelopes { get; set; }

#pragma warning disable 1591 // Xml Comments

        public EventSourceId EventSourceId { get; set; }

        public EventSourceVersion Version { get; private set; }

        public UncommittedEventStream UncommittedEvents { get; private set; }

        public void Apply(IEvent @event)
        {
            Apply(@event, true);
        }

        public virtual void ReApply(CommittedEventStream eventStream)
        {
            ValidateEventStream(eventStream);

            foreach (var eventAndEnvelope in eventStream)
                ReApply(eventAndEnvelope.Event);

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
            UncommittedEvents = new UncommittedEventStream(EventSourceId);
            Version = Version.NextCommit();
        }


        public virtual void Rollback()
        {
            UncommittedEvents = new UncommittedEventStream(EventSourceId);
            Version = Version.PreviousCommit();
        }

		public void Dispose()
		{
			Commit();
		}

#pragma warning restore 1591 // Xml Comments
        /// <summary>
        /// Get the event source type
        /// </summary>
		protected virtual Type EventSourceType { get { return GetType(); } }


        void ReApply(IEvent @event)
        {
            Apply(@event, false);
        }

        void Apply(IEvent @event, bool isNew)
        {
            if (isNew)
            {
                ThrowIfEventEnvelopesNotSet();
                var envelope = EventEnvelopes.CreateFrom(this, @event);
            	@event.EventSource = EventSourceType.AssemblyQualifiedName;
                UncommittedEvents.Append(envelope, @event);
                Version = Version.NextSequence();
                @event.Version = Version;
            }
            HandleInternally(@event);
        }

        void HandleInternally(IEvent @event)
        {
            var handleMethod = this.GetOnMethod(@event);
            if (handleMethod != null)
                handleMethod.Invoke(this, new[] { @event });
            Version = @event.Version;
        }


        void ValidateEventStream(EventStream eventStream)
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

        void ThrowIfEventEnvelopesNotSet()
        {
            if (EventEnvelopes == null) throw new EventEnvelopesMissing();
        }
    }
}
