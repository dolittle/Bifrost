#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

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
    [Serializable]
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
            ChapterProperties = GetType().GetProperties().Where(p => p.PropertyType.HasInterface<IChapter>()).ToArray();
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

                eventStore.Save(uncommittedEventStream);
            }
        }

        public CommittedEventStream Load(Type aggregatedRootType, Guid aggregateId)
        {
            var eventStream = new CommittedEventStream(aggregateId);
            if( _aggregatedRootEvents.ContainsKey(aggregateId))
                eventStream.Append(_aggregatedRootEvents[aggregateId]);
            return eventStream;
        }

        public void Save(UncommittedEventStream eventsToSave)
        {
            if (!_aggregatedRootEvents.ContainsKey(eventsToSave.EventSourceId))
                _aggregatedRootEvents[eventsToSave.EventSourceId] = new List<IEvent>();

            _aggregatedRootEvents[eventsToSave.EventSourceId].AddRange(eventsToSave);
        }

        public EventSourceVersion GetLastCommittedVersion(Type aggregatedRootType, Guid aggregateId)
        {
            if (!_aggregatedRootEvents.ContainsKey(aggregateId))
                return EventSourceVersion.Zero;

            var @event = _aggregatedRootEvents[aggregateId].OrderByDescending(e => e.Version).FirstOrDefault();
            if( @event == null ) 
                return EventSourceVersion.Zero;

            return @event.Version;
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
