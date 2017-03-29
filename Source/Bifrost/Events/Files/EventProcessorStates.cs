/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Dynamic;
using System.IO;
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
        ISystemClock _systemClock;

        /// <summary>
        /// Initializes a new instance of <see cref="ISerializer"/>
        /// </summary>
        /// <param name="configuration">The <see cref="EventProcessorStatesConfiguration">configuration</see></param>
        /// <param name="serializer"><see cref="ISerializer"/></param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for getting time from the system</param>
        public EventProcessorStates(EventProcessorStatesConfiguration configuration, ISerializer serializer, ISystemClock systemClock)
        {
            _configuration = configuration;
            _serializer = serializer;
            _systemClock = systemClock;
        }

        /// <inheritdoc/>
        public IEventProcessorState GetFor(IEventProcessor eventProcessor)
        {
            var path = GetPathFor(eventProcessor);
            var json = File.ReadAllText(path);

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
            MakeSurePathExists();

            WriteState(eventProcessor, envelope, EventProcessingStatus.Failed);
        }

        /// <inheritdoc/>
        public void ReportSuccessFor(IEventProcessor eventProcessor, IEvent @event, IEventEnvelope envelope)
        {
            MakeSurePathExists();

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
            var path = GetPathFor(eventProcessor);
            File.WriteAllText(path, json);
        }

        string GetPathFor(IEventProcessor eventProcessor)
        {
            var path = Path.Combine(_configuration.Path, eventProcessor.Identifier, ".state");
            return path;
        }

        void MakeSurePathExists()
        {
            if (!Directory.Exists(_configuration.Path)) Directory.CreateDirectory(_configuration.Path);
        }
    }
}
