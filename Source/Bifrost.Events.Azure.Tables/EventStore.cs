/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Applications;
using Bifrost.Extensions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bifrost.Events.Azure.Tables
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventStore"/> for Azure Tables
    /// </summary>
    public class EventStore : IEventStore
    {
        const string EventStoreTable = "Events";

        IApplicationResources _applicationResources;
        IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        CloudTable _table;

        /// <summary>
        /// Initializes a new instance of <see cref="EventStore"/>
        /// </summary>
        /// <param name="applicationResources">System for dealing with <see cref="IApplicationResources">Application Resources</see></param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter">Converter</see> for converting to and from string representations</param>
        /// <param name="connectionStringProvider"><see cref="ICanProvideConnectionString">ConnectionString provider</see></param>
        public EventStore(
            IApplicationResources applicationResources,
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter, 
            ICanProvideConnectionString connectionStringProvider)
        {
            _applicationResources = applicationResources;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            var connectionString = connectionStringProvider();

            var account = CloudStorageAccount.Parse(connectionString);
            var tableClient = account.CreateCloudTableClient();
            _table = tableClient.GetTableReference(EventStoreTable);
            _table.CreateIfNotExists();
        }

        /// <inheritdoc/>
        public void Commit(IEnumerable<EventAndEnvelope> eventsAndEnvelopes)
        {
            var batch = new TableBatchOperation();

            eventsAndEnvelopes.ForEach((e) =>
            {
                var partitionKey = GetPartitionKeyFor(e.Envelope.EventSource, e.Envelope.EventSourceId);
                var rowKey = e.Envelope.SequenceNumber.Value.ToString();
                var @event = new DynamicTableEntity(partitionKey, rowKey);
                @event["CorrelationId"] = new EntityProperty(e.Envelope.CorrelationId);
                @event["Event"] = new EntityProperty(_applicationResourceIdentifierConverter.AsString(e.Envelope.Event));
                @event["EventId"] = new EntityProperty(e.Envelope.EventId);
                @event["SequenceNumber"] = new EntityProperty(e.Envelope.SequenceNumber);
                @event["SequenceNumberForEvnetType"] = new EntityProperty(e.Envelope.SequenceNumberForEventType);
                @event["Generation"] = new EntityProperty(e.Envelope.Generation);
                @event["EventSource"] = new EntityProperty(_applicationResourceIdentifierConverter.AsString(e.Envelope.EventSource));
                @event["EventSourceId"] = new EntityProperty(e.Envelope.EventSourceId);
                @event["Version"] = new EntityProperty(e.Envelope.Version.Combine());
                @event["CausedBy"] = new EntityProperty(e.Envelope.CausedBy);
                @event["Occurred"] = new EntityProperty(e.Envelope.Occurred);

                batch.Add(TableOperation.Insert(@event));
            });

            _table.BeginExecuteBatch(batch, (state) => { }, null);
        }

        /// <inheritdoc/>
        public IEnumerable<EventAndEnvelope> GetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool HasEventsFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public EventSourceVersion GetVersionFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var partitionKey = GetPartitionKeyFor(eventSource, eventSourceId);
            var query = new TableQuery<DynamicTableEntity>().Select(new[] { "Version" });
            var events = _table.ExecuteQuery(query).ToArray();
            var result = events.OrderByDescending(e => long.Parse(e.RowKey)).FirstOrDefault();
            if (result == null) return EventSourceVersion.Zero;

            var value = result["Version"].DoubleValue.Value;
            var version = EventSourceVersion.FromCombined(value);
            return version;
        }

        string GetPartitionKeyFor(IApplicationResourceIdentifier identifier, EventSourceId id)
        {
            var identifierAsString = _applicationResourceIdentifierConverter.AsString(identifier);
            identifierAsString = identifierAsString.Replace('#', '|');

            var partitionKey = $"{identifierAsString}-{id}";
            return partitionKey;
        }
    }
}
