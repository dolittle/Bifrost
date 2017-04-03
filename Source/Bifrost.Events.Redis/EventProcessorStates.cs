/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Dynamic;
using Bifrost.Serialization;
using Bifrost.Time;
using StackExchange.Redis;

namespace Bifrost.Events.Redis
{
    /// <summary>
    /// Represents an implementation of <see cref="EventProcessorStates"/> for Redis
    /// </summary>
    public class EventProcessorStates : IEventProcessorStates
    {
        const string KeyPrefix = "EventProcessorStatesFor";

        ISerializer _serializer;
        ISystemClock _systemClock;
        IDatabase _database;


        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessorStates"/>
        /// </summary>
        /// <param name="configuration">The <see cref="EventProcessorStatesConfiguration">configuration</see> to use</param>
        /// <param name="serializer"><see cref="ISerializer"/> to use for serializing state</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> to use for time</param>
        public EventProcessorStates(EventProcessorStatesConfiguration configuration, ISerializer serializer, ISystemClock systemClock)
        {
            _serializer = serializer;
            _systemClock = systemClock;
            var redis = ConnectionMultiplexer.Connect(string.Join(";", configuration.ConnectionStrings));
            _database = redis.GetDatabase();
        }

        /// <inheritdoc/>
        public IEventProcessorState GetFor(IEventProcessor eventProcessor)
        {
            var key = GetKeyFor(eventProcessor);
            var json = _database.StringGet(key);

            var eventProcessorState = new EventProcessorState(
                eventProcessor,
                EventProcessorStatus.Online,
                DateTimeOffset.MinValue,
                EventSequenceNumber.Zero,
                EventSequenceNumber.Zero,
                EventProcessingStatus.Success);

            if( !json.IsNullOrEmpty )
                _serializer.FromJson(eventProcessorState, json.ToString());

            return eventProcessorState;
        }

        /// <inheritdoc/>
        public void ReportFailureFor(IEventProcessor eventProcessor, IEvent @event, IEventEnvelope envelope)
        {
            SetState(eventProcessor, envelope, EventProcessingStatus.Failed);
        }

        /// <inheritdoc/>
        public void ReportSuccessFor(IEventProcessor eventProcessor, IEvent @event, IEventEnvelope envelope)
        {
            SetState(eventProcessor, envelope, EventProcessingStatus.Success);
        }

        void SetState(IEventProcessor eventProcessor, IEventEnvelope envelope, EventProcessingStatus processingStatus)
        {
            dynamic state = new ExpandoObject();
            state.EventProcessor = eventProcessor.Identifier;
            state.LastProcessedSequenceNumber = envelope.SequenceNumber;
            state.LastProcessedSequenceNumberForEventType = envelope.SequenceNumberForEventType;
            state.LastProcessed = _systemClock.GetCurrentTime();
            state.LastProcessingStatus = processingStatus;

            var json = _serializer.ToJson(state);
            _database.StringSet(GetKeyFor(eventProcessor), json);
        }

        string GetKeyFor(IEventProcessor eventProcessor)
        {
            return $"{KeyPrefix}-{eventProcessor.Identifier}";
        }
    }
}
