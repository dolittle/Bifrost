/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;
using Bifrost.Serialization;
using RabbitMQ.Client;

namespace Bifrost.Events.RabbitMQ
{
    /// <summary>
    /// 
    /// </summary>
    public static class DynamicExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (var property in value.GetType().GetTypeInfo().GetProperties() )
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }
    }

    /// <summary>
    /// Represents an implementation of <see cref="ICanSendCommittedEventStream"/>
    /// </summary>
    public class CommittedEventStreamSender : ICanSendCommittedEventStream
    {
        ISerializer _serializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"></param>
        public CommittedEventStreamSender(ISerializer serializer)
        {
            _serializer = serializer;

        }

        /// <inheritdoc/>
        public void Send(CommittedEventStream committedEventStream)
        {
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


            var eventsToSend = new List<dynamic>();
            foreach (var eventAndEnvelope in committedEventStream)
            {
                dynamic eventToSend = new ExpandoObject();
                eventToSend.Envelope = eventAndEnvelope.Envelope;
                eventToSend.Event = eventAndEnvelope.Event.ToDynamic();
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
