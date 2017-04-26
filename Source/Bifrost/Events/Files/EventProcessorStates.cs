/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Dynamic;
using Bifrost.Logging;
using Bifrost.Serialization;
using Bifrost.Time;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents a simple and naïve implementation for handling <see cref="IEventProcessorStates"/>
    /// directly on the filesystem
    /// </summary>
    public class EventProcessorStates : IEventProcessorStates
    {
        EventProcessorStatesConfiguration _configuration;
        ISerializer _serializer;
        IFiles _files;
        ISystemClock _systemClock;

        /// <summary>
        /// Initializes a new instance of <see cref="ISerializer"/>
        /// </summary>
        /// <param name="configuration">The <see cref="EventProcessorStatesConfiguration">configuration</see></param>
        /// <param name="serializer"><see cref="ISerializer"/></param>
        /// <param name="files"><see cref="IFiles"/> to work with files</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for getting time from the system</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public EventProcessorStates(
            EventProcessorStatesConfiguration configuration, 
            ISerializer serializer, 
            IFiles files, 
            ISystemClock systemClock,
            ILogger logger)
        {
            _configuration = configuration;
            _serializer = serializer;
            _files = files;
            _systemClock = systemClock;
            logger.Information($"Using path : {configuration.Path}");
        }

        /// <inheritdoc/>
        public IEventProcessorState GetFor(IEventProcessor eventProcessor)
        {
            var fileName = GetFileNameFor(eventProcessor);
            var json = _files.ReadString(_configuration.Path, fileName);

            var eventProcessorState = new EventProcessorState(
                eventProcessor, 
                EventProcessorStatus.Online,
                DateTimeOffset.MinValue, 
                EventSequenceNumber.Zero,
                EventSequenceNumber.Zero, 
                EventProcessingStatus.Success);
            _serializer.FromJson(eventProcessorState, json);
            return eventProcessorState;
        }

        /// <inheritdoc/>
        public void ReportFailureFor(IEventProcessor eventProcessor, IEvent @event, IEventEnvelope envelope)
        {
            WriteState(eventProcessor, envelope, EventProcessingStatus.Failed);
        }

        /// <inheritdoc/>
        public void ReportSuccessFor(IEventProcessor eventProcessor, IEvent @event, IEventEnvelope envelope)
        {
            WriteState(eventProcessor, envelope, EventProcessingStatus.Success);
        }

        void WriteState(IEventProcessor eventProcessor, IEventEnvelope envelope, EventProcessingStatus processingStatus)
        {
            dynamic state = new ExpandoObject();
            state.EventProcessor = eventProcessor.Identifier;
            state.LastProcessedSequenceNumber = envelope.SequenceNumber;
            state.LastProcessedSequenceNumberForEventType = envelope.SequenceNumberForEventType;
            state.LastProcessed = _systemClock.GetCurrentTime();
            state.LastProcessingStatus = processingStatus;

            var json = _serializer.ToJson(state);
            var fileName = GetFileNameFor(eventProcessor);

            _files.WriteString(_configuration.Path, fileName, json);
        }

        string GetFileNameFor(IEventProcessor eventProcessor)
        {
            return $"{eventProcessor.Identifier.Value.GetHashCode().ToString()}.state";
        }
    }
}
