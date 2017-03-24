/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEventProcessorStates"/>
    /// </summary>
    public class NullEventProcessorStates : IEventProcessorStates
    {
         
        /// <ineritdoc/>
        public IEventProcessorState GetFor(IEventProcessor eventProcessor)
        {
            return new EventProcessorState(
                    eventProcessor,
                    EventProcessorStatus.Online,
                    DateTimeOffset.MaxValue,
                    EventSequenceNumber.Zero,
                    EventSequenceNumber.Zero,
                    EventProcessingStatus.Success
                );
        }

        /// <ineritdoc/>
        public void ReportFailureFor(IEventProcessor eventProcessor, IEvent @event, IEventEnvelope envelope)
        {
            
        }

        /// <ineritdoc/>
        public void ReportSuccessFor(IEventProcessor eventProcessor, IEvent @event, IEventEnvelope envelope)
        {
            
        }
    }
}
