#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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
using System.Collections.Generic;
using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Execution;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a <see cref="ICommandContext"/> as a <see cref="Bifrost.Lifecycle.IUnitOfWork"/> for a <see cref="ICommand"/> applied to a <see cref="ISaga"/>
    /// </summary>
    public class SagaCommandContext : ICommandContext
    {
        readonly ISaga _saga;
        readonly IEventStore _eventStore;
        readonly IProcessMethodInvoker _processMethodInvoker;
        readonly ISagaLibrarian _sagaLibrarian;
        private readonly List<IAggregatedRoot> _objectsTracked = new List<IAggregatedRoot>();

        /// <summary>
        /// Initializes an instance of the <see cref="SagaCommandContext"/> for a saga
        /// </summary>
        /// <param name="saga"><see cref="ISaga"/> to start the context for</param>
        /// <param name="command"><see cref="ICommand"/> that will be applied </param>
        /// <param name="executionContext">A <see cref="IExecutionContext"/> that is the context of execution for the <see cref="ICommand"/></param>
        /// <param name="eventStore">A <see cref="IEventStore"/> that will receive any events generated</param>
        /// <param name="processMethodInvoker">A <see cref="IProcessMethodInvoker"/> for processing events on the <see cref="ISaga"/></param>
        /// <param name="sagaLibrarian">A <see cref="ISagaLibrarian"/> for dealing with the <see cref="ISaga"/> and persistence</param>
        public SagaCommandContext(
            ISaga saga,
            ICommand command,
            IExecutionContext executionContext,
            IEventStore eventStore,
            IProcessMethodInvoker processMethodInvoker,
            ISagaLibrarian sagaLibrarian)
        {
            Command = command;
            ExecutionContext = executionContext;
            _saga = saga;
            _eventStore = eventStore;
            _processMethodInvoker = processMethodInvoker;
            _sagaLibrarian = sagaLibrarian;

            EventStores = new[] {_eventStore, saga};
        }

#pragma warning disable 1591 // Xml Comments
        public ICommand Command { get; private set; }
        public IExecutionContext ExecutionContext { get; private set; }
        public IEnumerable<IEventStore> EventStores { get; private set; }

        public void RegisterForTracking(IAggregatedRoot aggregatedRoot)
        {
            _objectsTracked.Add(aggregatedRoot);
        }

        public IEnumerable<IAggregatedRoot> GetObjectsBeingTracked()
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
                	events.MarkEventsWithCommand(Command);
                    events.ExpandExecutionContext(ExecutionContext);
                    ProcessEvents(events);
                    _saga.Save(events);
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
