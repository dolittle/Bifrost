/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Execution;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a <see cref="ICommandContext"/> as a <see cref="ITransaction"/> for a <see cref="ICommand"/> applied to a <see cref="ISaga"/>
    /// </summary>
    public class SagaCommandContext : ICommandContext
    {
        ISaga _saga;
        IEventStore _eventStore;
        IUncommittedEventStreamCoordinator _uncommittedEventStreamCoordinator;
        IProcessMethodInvoker _processMethodInvoker;
        ISagaLibrarian _sagaLibrarian;
        List<IAggregateRoot> _objectsTracked = new List<IAggregateRoot>();


        /// <summary>
        /// Initializes an instance of the <see cref="SagaCommandContext"/> for a saga
        /// </summary>
        /// <param name="saga"><see cref="ISaga"/> to start the context for</param>
        /// <param name="command"><see cref="ICommand"/> that will be applied </param>
        /// <param name="executionContext">A <see cref="IExecutionContext"/> that is the context of execution for the <see cref="ICommand"/></param>
        /// <param name="eventStore">A <see cref="IEventStore"/> that will receive any events generated</param>
        /// <param name="uncommittedEventStreamCoordinator">A <see cref="IUncommittedEventStreamCoordinator"/> to use for coordinating a <see cref="UncommittedEventStream"/></param>
        /// <param name="processMethodInvoker">A <see cref="IProcessMethodInvoker"/> for processing events on the <see cref="ISaga"/></param>
        /// <param name="sagaLibrarian">A <see cref="ISagaLibrarian"/> for dealing with the <see cref="ISaga"/> and persistence</param>
        public SagaCommandContext(
            ISaga saga,
            ICommand command,
            IExecutionContext executionContext,
            IEventStore eventStore,
            IUncommittedEventStreamCoordinator uncommittedEventStreamCoordinator,
            IProcessMethodInvoker processMethodInvoker,
            ISagaLibrarian sagaLibrarian)
        {
            Command = command;
            ExecutionContext = executionContext;
            _saga = saga;
            _eventStore = eventStore;
            _uncommittedEventStreamCoordinator = uncommittedEventStreamCoordinator;
            _processMethodInvoker = processMethodInvoker;
            _sagaLibrarian = sagaLibrarian;
        }

#pragma warning disable 1591 // Xml Comments
        public ICommand Command { get; private set; }
        public IExecutionContext ExecutionContext { get; private set; }

        public void RegisterForTracking(IAggregateRoot aggregatedRoot)
        {
            _objectsTracked.Add(aggregatedRoot);
        }

        public IEnumerable<IAggregateRoot> GetObjectsBeingTracked()
        {
            return _objectsTracked;
        }


        public void Commit()
        {
            var trackedObjects = GetObjectsBeingTracked();
            foreach (var trackedObject in trackedObjects)
            {
                var events = trackedObject.UncommittedEvents;
                if (events.HasEvents)
                {
                	events.MarkEventsWithCommandDetails(Command);
                    events.ExpandExecutionContext(ExecutionContext);
                    ProcessEvents(events);
                    _saga.Commit(events);
					_uncommittedEventStreamCoordinator.Commit(events);
                    trackedObject.Commit();
                    _sagaLibrarian.Catalogue(_saga);
                }
            }
        }

        public void Rollback()
        {

        }

        public void Dispose()
        {
            Commit();
        }

        public CommittedEventStream GetCommittedEventsFor(EventSource eventSource, Guid eventSourceId)
        {
            var stream = _eventStore.GetForEventSource(eventSource, eventSourceId);
            var sagaStream = _saga.GetForEventSource(eventSource, eventSourceId);
            stream.Append(sagaStream);
            return stream;
        }

        public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
        {
            var eventStoreVersion = _eventStore.GetLastCommittedVersion(eventSource, eventSourceId);
            var sagaVersion = _saga.GetLastCommittedVersion(eventSource, eventSourceId);
            return new[] { eventStoreVersion, sagaVersion }.Max();
        }

#pragma warning restore 1591 // Xml Comments


        void ProcessEvents(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                var chapters = _saga.Chapters;
                foreach (var chapter in chapters )
                    _processMethodInvoker.TryProcess(chapter, @event);

                _processMethodInvoker.TryProcess(_saga, @event);
            }
        }
    }
}
