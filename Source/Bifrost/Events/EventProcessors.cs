/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Applications;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessors"/>
    /// </summary>
    [Singleton]
    public class EventProcessors : IEventProcessors
    {
        Dictionary<IApplicationResourceIdentifier, List<IEventProcessor>> _eventProcessorsByResourceIdentifier;
        List<IEventProcessor> _eventProcessors = new List<IEventProcessor>();
        IApplicationResources _applicationResources;

        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessors"/>
        /// </summary>
        /// <param name="applicationResources"></param>
        /// <param name="systemsThatKnowsAboutEventProcessors"></param>
        public EventProcessors(IApplicationResources applicationResources, IInstancesOf<IKnowAboutEventProcessors> systemsThatKnowsAboutEventProcessors)
        {
            _applicationResources = applicationResources;
            GatherEventProcessorsFrom(systemsThatKnowsAboutEventProcessors);
        }

        /// <inheritdoc/>
        public IEnumerable<IEventProcessor> All => _eventProcessors;

        /// <inheritdoc/>
        public IEventProcessingResults Process(IEventEnvelope envelope, IEvent @event)
        {
            var identifier = _applicationResources.Identify(@event);
            var i = _eventProcessorsByResourceIdentifier.First().Value;
            if (!_eventProcessorsByResourceIdentifier.ContainsKey(identifier)) return new EventProcessingResults(new IEventProcessingResult[0]);

            List<IEventProcessingResult> results = new List<IEventProcessingResult>();
            var eventProcessors = _eventProcessorsByResourceIdentifier[identifier];
            eventProcessors.ForEach(e => results.Add(e.Process(envelope, @event)));

            return new EventProcessingResults(results);
        }

        void GatherEventProcessorsFrom(IEnumerable<IKnowAboutEventProcessors> systemsThatHasEventProcessors)
        {
            _eventProcessorsByResourceIdentifier = new Dictionary<IApplicationResourceIdentifier, List<IEventProcessor>>();
            systemsThatHasEventProcessors.ForEach(a => a.ForEach(e =>
            {
                List<IEventProcessor> eventProcessors;
                if (_eventProcessorsByResourceIdentifier.ContainsKey(e.Event)) eventProcessors = _eventProcessorsByResourceIdentifier[e.Event];
                else
                {
                    eventProcessors = new List<IEventProcessor>();
                    _eventProcessorsByResourceIdentifier[e.Event] = eventProcessors;
                }
                eventProcessors.Add(e);
            }));
        }
    }
}
