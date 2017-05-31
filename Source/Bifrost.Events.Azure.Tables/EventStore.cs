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
        IApplicationResourceResolver _applicationResourceResolver;
        CloudTable _table;

        /// <summary>
        /// Initializes a new instance of <see cref="EventStore"/>
        /// </summary>
        /// <param name="applicationResources">System for dealing with <see cref="IApplicationResources">Application Resources</see></param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter">Converter</see> for converting to and from string representations</param>
        /// <param name="applicationResourceResolver"><see cref="IApplicationResourceResolver"/> for resolving types from <see cref="IApplicationResourceIdentifier">identifiers</see></param>
        /// <param name="connectionStringProvider"><see cref="ICanProvideConnectionString">ConnectionString provider</see></param>
        public EventStore(
            IApplicationResources applicationResources,
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter,
            IApplicationResourceResolver applicationResourceResolver,
            ICanProvideConnectionString connectionStringProvider)
        {
            _applicationResources = applicationResources;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _applicationResourceResolver = applicationResourceResolver;
            var connectionString = connectionStringProvider();

            var account = CloudStorageAccount.Parse(connectionString);
            
            var tableClient = account.CreateCloudTableClient();
            tableClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromMilliseconds(100), 5);
            _table = tableClient.GetTableReference(EventStoreTable);

            _table.CreateIfNotExistsAsync().Wait();
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

            _table.ExecuteBatchAsync(batch).Wait();
        }

        /// <inheritdoc/>
        public IEnumerable<EventAndEnvelope> GetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var partitionKeyFilter = GetPartitionKeyFilterFor(eventSource, eventSourceId);
            var query = new TableQuery<DynamicTableEntity>().Where(partitionKeyFilter);

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

            var eventsAndEnvelopes = events.OrderBy(e => long.Parse(e.RowKey)).Select(entity => {
                var eventResource = GetApplicationResourceIdentifierFromSanitizedString(entity.Properties[PropertiesFor<EventEnvelope>.GetPropertyName(e => e.Event)].StringValue);
                var eventSourceResource = GetApplicationResourceIdentifierFromSanitizedString(entity.Properties[PropertiesFor<EventEnvelope>.GetPropertyName(e => e.EventSource)].StringValue);
                var envelope = new EventEnvelope(
                    PropertiesFor<EventEnvelope>.GetValue(entity, e => e.CorrelationId),
                    PropertiesFor<EventEnvelope>.GetValue(entity, e => e.EventId),
                    PropertiesFor<EventEnvelope>.GetValue(entity, e => e.SequenceNumber),
                    PropertiesFor<EventEnvelope>.GetValue(entity, e => e.SequenceNumberForEventType),
                    PropertiesFor<EventEnvelope>.GetValue(entity, e => e.Generation),
                    eventResource,
                    PropertiesFor<EventEnvelope>.GetValue(entity, e => e.EventSourceId),
                    eventSourceResource,
                    PropertiesFor<EventEnvelope>.GetValue(entity, e => e.Version),
                    PropertiesFor<EventEnvelope>.GetValue(entity, e => e.CausedBy),
                    PropertiesFor<EventEnvelope>.GetValue(entity, e => e.Occurred)
                );

                var eventType = _applicationResourceResolver.Resolve(envelope.Event);
                var @event = Activator.CreateInstance(eventType, envelope.EventSourceId) as IEvent;
                eventType.GetProperties().Where(p => p.CanWrite).ForEach(p => p.SetValue(@event, PropertyHelper.GetValue(entity, p)));

                return new EventAndEnvelope(envelope, @event);
            });

            return eventsAndEnvelopes;
        }

        /// <inheritdoc/>
        public bool HasEventsFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var partitionKeyFilter = GetPartitionKeyFilterFor(eventSource, eventSourceId);
            var query = new TableQuery<DynamicTableEntity>().Where(partitionKeyFilter);

            var hasEvents = false;
            TableContinuationToken continuationToken = null;
            _table.ExecuteQuerySegmentedAsync(query, continuationToken).ContinueWith(e =>
                hasEvents = e.Result.Results.Count() > 0
            ).Wait();

            return hasEvents;
        }

        /// <inheritdoc/>
        public EventSourceVersion GetVersionFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var partitionKeyFilter = GetPartitionKeyFilterFor(eventSource, eventSourceId);
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

        string GetPartitionKeyFilterFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var partitionKey = GetPartitionKeyFor(eventSource, eventSourceId);
            var partitionKeyFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey);
            return partitionKeyFilter;
        }

        string GetSanitizedApplicationResourceIdentifier(IApplicationResourceIdentifier identifier)
        {
            var identifierAsString = _applicationResourceIdentifierConverter.AsString(identifier);
            identifierAsString = identifierAsString.Replace('#', '|');
            return identifierAsString;
        }

        IApplicationResourceIdentifier GetApplicationResourceIdentifierFromSanitizedString(string identifierAsString)
        {
            identifierAsString = identifierAsString.Replace('|', '#');
            var identifier = _applicationResourceIdentifierConverter.FromString(identifierAsString);
            return identifier;
        }

        string GetPartitionKeyFor(IApplicationResourceIdentifier identifier, EventSourceId id)
        {
            var identifierAsString = GetSanitizedApplicationResourceIdentifier(identifier);
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

            if (value.IsConcept())
            {
                value = value.GetConceptValue();
                valueType = valueType.GetConceptValueType();
            }

            if( value == null )
            {
                var typeInfo = valueType.GetTypeInfo();
                if (typeInfo.IsValueType || valueType.HasDefaultConstructor())
                    value = Activator.CreateInstance(valueType);
                else if (valueType == typeof(string)) value = string.Empty;
            }

            if (valueType == typeof(EventSourceVersion)) entityProperty = new EntityProperty(((EventSourceVersion)value).Combine());
            else if (valueType == typeof(Guid)) entityProperty = new EntityProperty((Guid)value);
            else if (valueType == typeof(int)) entityProperty = new EntityProperty((int)value);
            else if (valueType == typeof(long)) entityProperty = new EntityProperty((long)value);
            else if (valueType == typeof(string)) entityProperty = new EntityProperty((string)value);
            else if (valueType == typeof(DateTime)) entityProperty = new EntityProperty((DateTime)value);
            else if (valueType == typeof(DateTimeOffset)) entityProperty = new EntityProperty((DateTimeOffset)value);
            else if (valueType == typeof(bool)) entityProperty = new EntityProperty((bool)value);
            else if (valueType == typeof(double)) entityProperty = new EntityProperty((double)value);
            else if (valueType == typeof(float)) entityProperty = new EntityProperty((double)value);
            else if (valueType.HasInterface<IApplicationResourceIdentifier>()) entityProperty = new EntityProperty(_applicationResourceIdentifierConverter.AsString((IApplicationResourceIdentifier)value));

            return entityProperty;
        }
    }
}
