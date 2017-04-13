/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Applications;
using Bifrost.Concepts;
using Bifrost.Extensions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
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
            tableClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromMilliseconds(100), 5);
            _table = tableClient.GetTableReference(EventStoreTable);

            _table.CreateIfNotExistsAsync();
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

                AddPropertiesFrom(@event, e.Envelope);
                AddPropertiesFrom(@event, e.Event, "EventSourceId");

                batch.Add(TableOperation.Insert(@event));
            });

            _table.ExecuteBatchAsync(batch);
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
            var partitionKeyFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey);
            var query = new TableQuery<DynamicTableEntity>().Select(new[] { "Version" }).Where(partitionKeyFilter);

            var events = new List<DynamicTableEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                _table.ExecuteQuerySegmentedAsync(query, continuationToken).ContinueWith(e =>
                {
                    events.AddRange(e.Result.Results);
                    continuationToken = e.Result.ContinuationToken;
                }).Wait();
            } while (continuationToken != null);

            var result = events.OrderByDescending(e => long.Parse(e.RowKey)).FirstOrDefault();
            if (result == null) return EventSourceVersion.Zero;

            var value = result.Properties["Version"].DoubleValue.Value;
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

        void AddPropertiesFrom(DynamicTableEntity @event, object instance, params string[] propertiesToIgnore)
        {
            foreach (var property in instance.GetType().GetTypeInfo().GetProperties())
            {
                if (propertiesToIgnore.Contains(property.Name)) continue;
                var value = property.GetValue(instance);
                var entityProperty = GetEntityPropertyFor(property, value);
                if (entityProperty != null) @event.Properties[property.Name] = entityProperty;
            }
        }

        EntityProperty GetEntityPropertyFor(PropertyInfo property, object value)
        {
            EntityProperty entityProperty = null;
            var valueType = value.GetType();

            if (value.IsConcept()) value = value.GetConceptValue();

            if (valueType == typeof(Guid)) entityProperty = new EntityProperty((Guid)value);
            if (valueType == typeof(int)) entityProperty = new EntityProperty((long)value);
            if (valueType == typeof(long)) entityProperty = new EntityProperty((long)value);
            if (valueType == typeof(string)) entityProperty = new EntityProperty((string)value);
            if (valueType == typeof(DateTime)) entityProperty = new EntityProperty((DateTime)value);
            if (valueType == typeof(DateTimeOffset)) entityProperty = new EntityProperty((DateTimeOffset)value);
            if (valueType == typeof(bool)) entityProperty = new EntityProperty((bool)value);
            if (valueType == typeof(double)) entityProperty = new EntityProperty((double)value);
            if (valueType == typeof(float)) entityProperty = new EntityProperty((double)value);

            return entityProperty;
        }
    }
}
