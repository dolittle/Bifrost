/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bifrost.Applications;
using Bifrost.Lifecycle;
using Bifrost.Serialization;
using Microsoft.Azure.ServiceBus;

namespace Bifrost.Events.Azure.ServiceBus
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanReceiveCommittedEventStream"/> for Azure ServiceBus
    /// </summary>
    public class CommittedEventStreamReceiver : ICanReceiveCommittedEventStream
    {
        readonly ISerializer _serializer;
        readonly IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        readonly IApplicationResourceResolver _applicationResourceResolver;
        readonly QueueClient _queueClient;

        /// <inheritdoc/>
        public event CommittedEventStreamReceived Received = (e) => { };

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use for deserializing <see cref="IEvent">events</see></param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter"/> used for converting resource identifiers</param>
        /// <param name="applicationResourceResolver"><see cref="IApplicationResourceResolver"/> used for resolving types from <see cref="IApplicationResourceIdentifier"/></param>
        /// <param name="connectionStringProvider"><see cref="ICanProvideConnectionStringToReceiver">Provider</see> of connection string</param>
        public CommittedEventStreamReceiver(
            ISerializer serializer,
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter,
            IApplicationResourceResolver applicationResourceResolver,
            ICanProvideConnectionStringToReceiver connectionStringProvider)
        {
            _serializer = serializer;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _applicationResourceResolver = applicationResourceResolver;

            var connectionString = connectionStringProvider();
            _queueClient = new QueueClient(connectionString, Constants.QueueName, ReceiveMode.PeekLock);
            _queueClient.RegisterMessageHandler(Receive);
        }



        Task Receive(Message message, CancellationToken token)
        {
            var dynamicEventsAndEnvelopes = new List<dynamic>();
            var json = System.Text.Encoding.UTF8.GetString(message.Body);

            _serializer.FromJson(dynamicEventsAndEnvelopes, json);

            var eventsAndEnvelopes = new List<EventAndEnvelope>();

            foreach (var dynamicEventAndEnvelope in dynamicEventsAndEnvelopes)
            {
                var env = dynamicEventAndEnvelope.Envelope;

                var correlationId = (TransactionCorrelationId)Guid.Parse(env.CorrelationId.ToString());
                var eventId = (EventId)Guid.Parse(env.EventId.ToString());
                var sequenceNumber = (EventSequenceNumber)long.Parse(env.SequenceNumber.ToString());
                var sequenceNumberForEventType = (EventSequenceNumber)long.Parse(env.SequenceNumberForEventType.ToString());
                var generation = (EventGeneration)long.Parse(env.Generation.ToString());
                var @event = _applicationResourceIdentifierConverter.FromString(env.Event.ToString());
                var eventSourceId = (EventSourceId)Guid.Parse(env.EventSourceId.ToString());
                var eventSource = _applicationResourceIdentifierConverter.FromString(env.EventSource.ToString());
                var version = (EventSourceVersion)EventSourceVersion.FromCombined(double.Parse(env.Version.ToString()));
                var causedBy = (CausedBy)env.CausedBy.ToString();
                var occurred = DateTimeOffset.Parse(env.Occurred.ToString());

                var envelope = new EventEnvelope(
                    correlationId,
                    eventId,
                    sequenceNumber,
                    sequenceNumberForEventType,
                    generation,
                    @event,
                    eventSourceId,
                    eventSource,
                    version,
                    causedBy,
                    occurred
                );

                var eventType = _applicationResourceResolver.Resolve(@event);

                var eventInstance = Activator.CreateInstance(eventType, new object[] { eventSourceId }) as IEvent;
                var e = dynamicEventAndEnvelope.Event.ToString();

                _serializer.FromJson(eventInstance, e);
                eventsAndEnvelopes.Add(new EventAndEnvelope(envelope, eventInstance));
            }

            var stream = new CommittedEventStream(eventsAndEnvelopes.First().Envelope.EventSourceId, eventsAndEnvelopes);
            Received(stream);

            _queueClient.CompleteAsync(message.SystemProperties.LockToken);

            return Task.CompletedTask;
        }
    }
}
