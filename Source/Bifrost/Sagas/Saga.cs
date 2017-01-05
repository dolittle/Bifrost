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
        readonly List<IChapter> _chapters = new List<IChapter>();
        readonly Dictionary<Guid, List<IEvent>> _aggregatedRootEvents = new Dictionary<Guid, List<IEvent>>();

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

#pragma warning disable 1591 // Xml Comments
        public Guid Id { get; set; }
        public string Partition { get; set; }
        public string Key { get; set; }

        public IEnumerable<IChapter> Chapters { get { return _chapters; } }
        public IChapter CurrentChapter { get; private set; }

        public void SetCurrentChapter<T>() where T : IChapter
        {
            IChapter chapter = Get<T>();            
            SetCurrentChapter(chapter); 
        }
        
        public void SetCurrentChapter(IChapter chapter)
        {
            CurrentChapter = chapter;
            if (!Contains(chapter.GetType()))
                AddChapter(chapter);
            chapter.OnSetCurrent();
        }
        

        public void AddChapter(IChapter chapter)
        {
            ThrowIfChapterAlreadyExist(chapter.GetType());
            _chapters.Add(chapter);
            SetChapterPropertyIfAny(chapter);
        }

        public bool Contains<T>() where T : IChapter
        {
            return Contains(typeof(T));
        }

        public bool Contains(Type type)
        {
            return _chapters.Any(s => s.GetType() == type);
        }

        public T Get<T>() where T : IChapter
        {
            ThrowIfChapterDoesNotExist(typeof(T));
            return (T)_chapters.Where(s => s.GetType() == typeof (T)).Single();
        }

        public PropertyInfo[] ChapterProperties { get; private set; }


        public IEnumerable<IEvent> GetUncommittedEvents()
        {
            var uncommittedEvents = new List<IEvent>();
            foreach (var events in _aggregatedRootEvents.Values)
                uncommittedEvents.AddRange(events);

            return uncommittedEvents;
        }

        public void SetUncommittedEvents(IEnumerable<IEvent> events)
        {
            var query = events.GroupBy(e => e.EventSourceId).Select(g=>g);
            foreach (var group in query)
                _aggregatedRootEvents[group.Key] = group.ToList();
        }

        public void SaveUncommittedEventsToEventStore(IEventStore eventStore)
        {
            foreach( var aggregatedRootId in _aggregatedRootEvents.Keys )
            {
                var events = _aggregatedRootEvents[aggregatedRootId];

                var uncommittedEventStream = new UncommittedEventStream(aggregatedRootId);
                foreach (var @event in events)
                    uncommittedEventStream.Append(@event);

                eventStore.Commit(uncommittedEventStream);
            }
        }

        public CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId)
        {
            var eventStream = new CommittedEventStream(eventSourceId);
            if( _aggregatedRootEvents.ContainsKey(eventSourceId))
                eventStream.Append(_aggregatedRootEvents[eventSourceId]);
            return eventStream;
        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            if (!_aggregatedRootEvents.ContainsKey(uncommittedEventStream.EventSourceId))
                _aggregatedRootEvents[uncommittedEventStream.EventSourceId] = new List<IEvent>();

            _aggregatedRootEvents[uncommittedEventStream.EventSourceId].AddRange(uncommittedEventStream);

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId);
            committedEventStream.Append(uncommittedEventStream);
            return committedEventStream;
        }

        public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
        {
            if (!_aggregatedRootEvents.ContainsKey(eventSourceId))
                return EventSourceVersion.Zero;

            var @event = _aggregatedRootEvents[eventSourceId].OrderByDescending(e => e.Version).FirstOrDefault();
            if( @event == null ) 
                return EventSourceVersion.Zero;

            return @event.Version;
        }

        public IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEvent> GetAll()
        {
            throw new NotImplementedException();
        }

        public SagaState CurrentState { get; set; }

        public virtual void OnConclude()
        {}

        public virtual void OnBegin()
        {}

        public virtual void OnContinue()
        {}

        public void Begin()
        {
            CurrentState.TransitionTo(SagaState.BEGUN);
            OnBegin();   
        }

        public void Continue()
        {
            CurrentState.TransitionTo(SagaState.CONTINUING);
            OnContinue();
        }

        public void Conclude()
        {
            CurrentState.TransitionTo(SagaState.CONCLUDED);
            OnConclude();
        }

        public bool IsNew
        {
            get { return CurrentState.IsNew; }
        }
        
        public bool IsContinuing
        {
            get { return CurrentState.IsContinuing; }
        }
        
        public bool IsBegun
        {
            get { return CurrentState.IsBegun; }
        }
        
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
