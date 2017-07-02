/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Bifrost.Dynamic;
using Bifrost.Serialization;
using Microsoft.Azure.ServiceBus;

namespace Bifrost.Events.Azure.ServiceBus
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanSendCommittedEventStream"/> for Azure ServiceBus
    /// </summary>
    public class CommittedEventStreamSender : ICanSendCommittedEventStream
    {
        readonly ISerializer _serializer;
        readonly string _connectionString;
        readonly IQueueClient _queueClient;

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamSender"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> used for serializing messages</param>
        /// <param name="connectionStringProvider"><see cref="ICanProvideConnectionStringToSender">Provider</see> for connectionstring</param>
        public CommittedEventStreamSender(ICanProvideConnectionStringToSender connectionStringProvider, ISerializer serializer)
        {
            _serializer = serializer;
            _connectionString = connectionStringProvider();

            _queueClient = new QueueClient(_connectionString, Constants.QueueName, ReceiveMode.PeekLock, RetryPolicy.Default);
        }

        /// <inheritdoc/>
        public void Send(CommittedEventStream committedEventStream)
        {
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
            var message = new Message(messageBodyBytes);
            _queueClient.SendAsync(message);
        }
    }
}
