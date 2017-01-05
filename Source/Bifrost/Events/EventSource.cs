/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;
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
        /// <param name="id">Id of the event source</param>
        protected EventSource(Guid id)
        {
            Id = id;
            UncommittedEvents = new UncommittedEventStream(id);
        }

#pragma warning disable 1591 // Xml Comments
        public Guid Id { get; set; }

        public EventSourceVersion Version { get; private set; }

        public UncommittedEventStream UncommittedEvents { get; private set; }

        public void Apply(IEvent @event)
        {
            Apply(@event, true);
        }

        public void Apply(Expression<Action> expression)
        {
            var eventToApply = MethodEventFactory.CreateMethodEventFromExpression(Id, expression);
            Apply(eventToApply);

        }

        public virtual void ReApply(CommittedEventStream eventStream)
        {
            ValidateEventStream(eventStream);

            foreach (var @event in eventStream)
                ReApply(@event);

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
            UncommittedEvents = new UncommittedEventStream(Id);
            Version = Version.NextCommit();
        }


        public virtual void Rollback()
        {
            UncommittedEvents = new UncommittedEventStream(Id);
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
            	@event.EventSource = EventSourceType.AssemblyQualifiedName;
                UncommittedEvents.Append(@event);
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
                    string.Format(@"Expected events for Id {0} but got events for Id {1}", Id, eventStream.EventSourceId));
        }

        bool IsForThisEventSource(Guid targetEventSourceId)
        {
            return targetEventSourceId == Id;
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
