/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Applications;
using Bifrost.Execution;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessors"/>
    /// </summary>
    [Singleton]
    public class EventProcessors : IEventProcessors
    {
        IApplicationResources _applicationResources;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationResources"></param>
        public EventProcessors(IApplicationResources applicationResources)
        {
            _applicationResources = applicationResources;
        }

        /// <inheritdoc/>
        public IEventProcessingResults Process(IEvent @event)
        {
            var identifier = _applicationResources.Identify(@event);
            throw new NotImplementedException();
        }
    }
}
