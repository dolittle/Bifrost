/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Bifrost.Dynamic;
using Bifrost.Serialization;
using RabbitMQ.Client;

namespace Bifrost.Events.RabbitMQ
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanSendCommittedEventStream"/>
    /// </summary>
    public class CommittedEventStreamSender : ICanSendCommittedEventStream
    {
        ISerializer _serializer;
        string _connectionString;

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamSender"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> used for serializing messages</param>
        /// <param name="connectionStringProvider"><see cref="ICanProvideConnectionStringToSender">Provider</see> for connectionstring</param>
        public CommittedEventStreamSender(ISerializer serializer, ICanProvideConnectionStringToSender connectionStringProvider)
        {
            _serializer = serializer;
            _connectionString = connectionStringProvider();
        }

        /// <inheritdoc/>
        public void Send(CommittedEventStream committedEventStream)
        {
            var factory = new ConnectionFactory();
            factory.Uri = _connectionString;
            var connection = factory.CreateConnection();
            var exchangeName = "Events";
            var routingKey = "RoutingKey";
            var queueName = "Events";

            var model = connection.CreateModel();

            model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            model.QueueDeclare(queueName, false, false, false, null);
            model.QueueBind(queueName, exchangeName, routingKey, null);

            var eventsToSend = new List<dynamic>();
            foreach (var eventAndEnvelope in committedEventStream)
            {
                dynamic eventToSend = new ExpandoObject();
                eventToSend.Envelope = eventAndEnvelope.Envelope;
                eventToSend.Event = eventAndEnvelope.Event.AsExpandoObject();
                eventsToSend.Add(eventToSend);
            }

            var eventsToSendAsJson = _serializer.ToJson(eventsToSend);
            var messageBodyBytes = Encoding.UTF8.GetBytes(eventsToSendAsJson);

            var props = model.CreateBasicProperties();
            props.ContentType = "application/json";

            model.BasicPublish(exchangeName,
                               routingKey, props,
                               messageBodyBytes);
        }
    }
}