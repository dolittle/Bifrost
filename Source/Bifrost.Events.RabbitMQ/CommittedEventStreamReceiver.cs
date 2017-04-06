/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bifrost.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Bifrost.Applications;
using Bifrost.Lifecycle;
using System;

namespace Bifrost.Events.RabbitMQ
{
    /// <summary>
    /// 
    /// </summary>
    public class CommittedEventStreamReceiver : ICanReceiveCommittedEventStream
    {
        ISerializer _serializer;

        /// <inheritdoc/>
        public event CommittedEventStreamReceived Received = (e) => { };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="applicationResourceIdentifierConverter"></param>
        /// <param name="applicationResourceResolver"></param>
        public CommittedEventStreamReceiver(
            ISerializer serializer,
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter,
            IApplicationResourceResolver applicationResourceResolver)
        {
            _serializer = serializer;
            var factory = new ConnectionFactory();
            factory.Uri = "amqp://guest:guest@localhost:5672/";

            var connection = factory.CreateConnection();
            var exchangeName = "Events";
            var routingKey = "RoutingKey";
            var queueName = "Events";

            var model = connection.CreateModel();

            model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            model.QueueDeclare(queueName, false, false, false, null);
            model.QueueBind(queueName, exchangeName, routingKey, null);

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += (ch, ea) =>
            {
                try
                {
                    var dynamicEventsAndEnvelopes = new List<dynamic>();
                    var json = System.Text.Encoding.UTF8.GetString(ea.Body);

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
                        var @event = applicationResourceIdentifierConverter.FromString(env.Event.ToString());
                        var eventSourceId = (EventSourceId)Guid.Parse(env.EventSourceId.ToString());
                        var eventSource = applicationResourceIdentifierConverter.FromString(env.EventSource.ToString());
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

                        var eventType = applicationResourceResolver.Resolve(@event);
                        var eventInstance = Activator.CreateInstance(eventType, new object[] { eventSourceId }) as IEvent;
                        var e = dynamicEventAndEnvelope.Event.ToString();

                        _serializer.FromJson(eventInstance, e);

                        eventsAndEnvelopes.Add(new EventAndEnvelope(envelope, eventInstance));
                    }

                    var stream = new CommittedEventStream(eventsAndEnvelopes.First().Envelope.EventSourceId, eventsAndEnvelopes);
                    Received(stream);

                    model.BasicAck(ea.DeliveryTag, false);
                }
                catch
                {

                }
                finally
                {

                }


            };

            Task.Run(() => model.BasicConsume(queueName, false, consumer));
        }
    }
}