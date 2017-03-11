/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
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
        /// 
        /// </summary>
        /// <param name="applicationResources"></param>
        /// <param name="systemsThatHasEventProcessors"></param>
        public EventProcessors(IApplicationResources applicationResources, IInstancesOf<IKnowAboutEventProcessors> systemsThatHasEventProcessors)
        {
            _applicationResources = applicationResources;
            GatherEventProcessors(systemsThatHasEventProcessors);
        }

        /// <inheritdoc/>
        public IEnumerable<IEventProcessor> All => _eventProcessors;

        /// <inheritdoc/>
        public IEventProcessingResults Process(IEvent @event)
        {
            var identifier = _applicationResources.Identify(@event);
            if (_eventProcessorsByResourceIdentifier.ContainsKey(identifier)) return new EventProcessingResults(new IEventProcessingResult[0]);

            List<IEventProcessingResult> results = new List<IEventProcessingResult>();
            var eventProcessors = _eventProcessorsByResourceIdentifier[identifier];
            eventProcessors.ForEach(e => results.Add(e.Process(@event)));

            return new EventProcessingResults(results);
        }

        void GatherEventProcessors(IInstancesOf<IKnowAboutEventProcessors> systemsThatHasEventProcessors)
        {
            _eventProcessorsByResourceIdentifier = new Dictionary<IApplicationResourceIdentifier, List<IEventProcessor>>();
            systemsThatHasEventProcessors.ForEach(a => a.ForEach(e =>
            {
                List<IEventProcessor> eventProcessors;
                if (_eventProcessorsByResourceIdentifier.ContainsKey(e.Identifier)) eventProcessors = _eventProcessorsByResourceIdentifier[e.Identifier];
                else
                {
                    eventProcessors = new List<IEventProcessor>();
                    _eventProcessorsByResourceIdentifier[e.Identifier] = eventProcessors;
                }
                eventProcessors.Add(e);
            }));
        }
    }
}
