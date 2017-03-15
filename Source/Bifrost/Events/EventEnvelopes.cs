/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Applications;
using Bifrost.Execution;
using Bifrost.Time;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventEnvelopes"/>
    /// </summary>
    public class EventEnvelopes : IEventEnvelopes
    {
        IApplicationResources _applicationResources;
        ISystemClock _systemClock;
        IExecutionContext _executionContext;
        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

        /// <summary>
        /// Initializes a new instance of <see cref="EventEnvelopes"/>
        /// </summary>
        /// <param name="applicationResources"><see cref="IApplicationResources"/> for identifying resources</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for working with time</param>
        /// <param name="executionContext"><see cref="IExecutionContext"/> for working with metadata related to current execution context</param>
        /// <param name="eventMigrationHierarchyManager"><see cref="IEventMigrationHierarchyManager"/> for working with the migration levels of <see cref="IEvent">events</see></param>
        public EventEnvelopes(
            IApplicationResources applicationResources, 
            ISystemClock systemClock, 
            IExecutionContext executionContext, 
            IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _applicationResources = applicationResources;
            _systemClock = systemClock;
            _executionContext = executionContext;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
        }

        /// <inheritdoc/>
        public IEventEnvelope CreateFrom(IEventSource eventSource, IEvent @event)
        {
            var envelope = new EventEnvelope(
                Guid.NewGuid(), // TODO: Correlation ID
                Guid.NewGuid(),
                EventSequenceNumber.None, // TODO: We will get this 
                EventSequenceNumber.None, // TODO: We will get this 
                _eventMigrationHierarchyManager.GetCurrentGenerationFor(@event.GetType()),
                _applicationResources.Identify(@event),
                eventSource.EventSourceId,
                _applicationResources.Identify(eventSource),
                eventSource.Version,
                _executionContext.Principal.Identity.Name,
                _systemClock.GetCurrentTime()
            );

            return envelope;
        }
    }
}
