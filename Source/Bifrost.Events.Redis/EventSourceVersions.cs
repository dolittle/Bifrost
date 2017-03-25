/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Applications;
using Bifrost.Execution;
using StackExchange.Redis;

namespace Bifrost.Events.Redis
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSourceVersions"/>
    /// </summary>
    [Singleton]
    public class EventSourceVersions : IEventSourceVersions
    {
        const string VersionForPrefix = "VersionFor";

        IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        IEventStore _eventStore;
        IDatabase _database;
        

        /// <summary>
        /// Initializes a new instance of <see cref="EventSourceVersions"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventSourceVersionsConfiguration">Configuration</see></param>
        /// <param name="eventStore"><see cref="IEventStore"/> for getting information if not in the database</param>
        /// <param name="applicationResourceIdentifierConverter">Converter for converting <see cref="IApplicationResourceIdentifier"/> "/></param>
        public EventSourceVersions(
            EventSourceVersionsConfiguration configuration,
            IEventStore eventStore,
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter)
        {
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _eventStore = eventStore;

            var redis = ConnectionMultiplexer.Connect(string.Join(";", configuration.ConnectionStrings));
            _database = redis.GetDatabase();
        }

        /// <inheritdoc/>
        public EventSourceVersion GetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var key = GetKeyFor(eventSource, eventSourceId);
            var version = EventSourceVersion.Zero;
            var value = _database.StringGet(key);
            if (value.IsNull)
            {
                version = _eventStore.GetVersionFor(eventSource, eventSourceId);
                SetFor(eventSource, eventSourceId, version);
            }
            else version = EventSourceVersion.FromCombined(double.Parse(value.ToString()));

            return version;
        }

        /// <inheritdoc/>
        public void SetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId, EventSourceVersion version)
        {
            var key = GetKeyFor(eventSource, eventSourceId);
            _database.StringSet(key, version.Combine());
        }


        string GetKeyFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var key = $"{VersionForPrefix}-{_applicationResourceIdentifierConverter.AsString(eventSource)}-{eventSourceId.Value}";
            return key;
        }
    }
}