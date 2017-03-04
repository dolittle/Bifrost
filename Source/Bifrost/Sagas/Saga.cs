/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Events;
using Bifrost.Extensions;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a <see cref="ISaga"/>
    /// </summary>
    public class Saga : ISaga
    {
        List<IChapter> _chapters = new List<IChapter>();
        Dictionary<EventSourceId, List<EventWithEnvelope>> _eventsByEventSource = new Dictionary<EventSourceId, List<EventWithEnvelope>>();

        /// <summary>
        /// Initializes a new instance of <see cref="Saga"/>
        /// </summary>
        public Saga()
        {
            Id = Guid.NewGuid();
            Partition = GetType().Name;
            Key = Guid.NewGuid().ToString();
            CurrentState = new SagaState();
            ChapterProperties = GetType().GetTypeInfo().DeclaredProperties.Where(p => p.PropertyType.HasInterface<IChapter>()).ToArray();
        }

        /// <summary>
        /// Gets or sets the <see cref="IEventEnvelopes"/> - a dependency that would normally be on the constructor, but these type of objects should
        /// not have dependencies on the constructor level as it bleeds through all over the place. This is therefor then required by systems creating
        /// an instance and working with <see cref="Saga"/> to set this
        /// </summary>
        public IEventEnvelopes EventEnvelopes { get; set; }


        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <inheritdoc/>
        public string Partition { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IChapter> Chapters { get { return _chapters; } }

        /// <inheritdoc/>
        public IChapter CurrentChapter { get; private set; }

        /// <inheritdoc/>
        public void SetCurrentChapter<T>() where T : IChapter
        {
            IChapter chapter = Get<T>();            
            SetCurrentChapter(chapter); 
        }

        /// <inheritdoc/>
        public void SetCurrentChapter(IChapter chapter)
        {
            CurrentChapter = chapter;
            if (!Contains(chapter.GetType()))
                AddChapter(chapter);
            chapter.OnSetCurrent();
        }

        /// <inheritdoc/>
        public void AddChapter(IChapter chapter)
        {
            ThrowIfChapterAlreadyExist(chapter.GetType());
            _chapters.Add(chapter);
            SetChapterPropertyIfAny(chapter);
        }

        /// <inheritdoc/>
        public bool Contains<T>() where T : IChapter
        {
            return Contains(typeof(T));
        }

        /// <inheritdoc/>
        public bool Contains(Type type)
        {
            return _chapters.Any(s => s.GetType() == type);
        }

        /// <inheritdoc/>
        public T Get<T>() where T : IChapter
        {
            ThrowIfChapterDoesNotExist(typeof(T));
            return (T)_chapters.Where(s => s.GetType() == typeof (T)).Single();
        }

        /// <inheritdoc/>
        public PropertyInfo[] ChapterProperties { get; private set; }

        /// <inheritdoc/>
        public IEnumerable<EventWithEnvelope> GetUncommittedEvents()
        {
            var uncommittedEvents = new List<EventWithEnvelope>();
            foreach (var events in _eventsByEventSource.Values)
                uncommittedEvents.AddRange(events);

            return uncommittedEvents;
        }

        /// <inheritdoc/>
        public void SetUncommittedEvents(IEnumerable<EventWithEnvelope> events)
        {
            var query = events.GroupBy(e => e.Envelope.EventSourceId).Select(g=>g);
            foreach (var group in query)
                _eventsByEventSource[group.Key] = group.ToList();
        }

        /// <inheritdoc/>
        public void SaveUncommittedEventsToEventStore(IEventStore eventStore)
        {
            throw new NotImplementedException();
#if (false)
            foreach ( var aggregatedRootId in _eventsByEventSource.Keys )
            {
                var events = _eventsByEventSource[aggregatedRootId];

                var uncommittedEventStream = new UncommittedEventStream(aggregatedRootId);
                /*
                foreach (var @event in events)
                    uncommittedEventStream.Append(_eventEnvelopes.CreateFrom( @event);*/

                eventStore.Commit(uncommittedEventStream);
            }
#endif
        }

        /// <inheritdoc/>
        public CommittedEventStream GetFor(IEventSource eventSource)
        {
            var eventStream = new CommittedEventStream(eventSource.EventSourceId, _eventsByEventSource[eventSource.EventSourceId]);
            return eventStream;
        }

        /// <inheritdoc/>
        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            if (!_eventsByEventSource.ContainsKey(uncommittedEventStream.EventSourceId))
                _eventsByEventSource[uncommittedEventStream.EventSourceId] = new List<EventWithEnvelope>();

            _eventsByEventSource[uncommittedEventStream.EventSourceId].AddRange(uncommittedEventStream);

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, uncommittedEventStream);
            return committedEventStream;
        }

        /// <inheritdoc/>
        public EventSourceVersion GetLastCommittedVersionFor(IEventSource eventSource)
        {
            if (!_eventsByEventSource.ContainsKey(eventSource.EventSourceId))
                return EventSourceVersion.Zero;

            var eventAndEnvelope = _eventsByEventSource[eventSource.EventSourceId].OrderByDescending(e => e.Envelope.Version).FirstOrDefault();
            if( eventAndEnvelope == null ) 
                return EventSourceVersion.Zero;

            return eventAndEnvelope.Envelope.Version;
        }

        /// <inheritdoc/>
        public IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEvent> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public SagaState CurrentState { get; set; }

        /// <inheritdoc/>
        public virtual void OnConclude()
        {}

        /// <inheritdoc/>
        public virtual void OnBegin()
        {}

        /// <inheritdoc/>
        public virtual void OnContinue()
        {}

        /// <inheritdoc/>
        public void Begin()
        {
            CurrentState.TransitionTo(SagaState.BEGUN);
            OnBegin();   
        }

        /// <inheritdoc/>
        public void Continue()
        {
            CurrentState.TransitionTo(SagaState.CONTINUING);
            OnContinue();
        }

        /// <inheritdoc/>
        public void Conclude()
        {
            CurrentState.TransitionTo(SagaState.CONCLUDED);
            OnConclude();
        }

        /// <inheritdoc/>
        public bool IsNew
        {
            get { return CurrentState.IsNew; }
        }

        /// <inheritdoc/>
        public bool IsContinuing
        {
            get { return CurrentState.IsContinuing; }
        }

        /// <inheritdoc/>
        public bool IsBegun
        {
            get { return CurrentState.IsBegun; }
        }

        /// <inheritdoc/>
        public bool IsConcluded
        {
            get { return CurrentState.IsConcluded; }
        }

#pragma warning restore 1591 // Xml Comments
        void SetChapterPropertyIfAny(IChapter chapter)
        {
            var property = ChapterProperties.Where(p => p.PropertyType.Equals(chapter.GetType())).SingleOrDefault();
            if (property != null)
                property.SetValue(this, chapter, null);
        }

        void ThrowIfChapterAlreadyExist(Type type)
        {
            if (_chapters.Any(s => s.GetType() == type))
                throw new ChapterAlreadyExistException();
        }

        void ThrowIfChapterDoesNotExist(Type type)
        {
            if (!_chapters.Any(s => s.GetType() == type))
                throw new ChapterDoesNotExistException();
        }
    }
}
