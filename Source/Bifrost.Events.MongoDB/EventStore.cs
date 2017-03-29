/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Applications;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Bifrost.Events.MongoDB
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventStore"/> for MongoDB
    /// </summary>
    public class EventStore : IEventStore
    {
        IMongoCollection<BsonDocument> _collection;

        static EventStore()
        {
            BsonSerializer.RegisterSerializationProvider(new ConceptSerializationProvider());
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EventStore"/>
        /// </summary>
        public EventStore()
        {
            _collection = null;
        }

        /// <inheritdoc/>
        public void Commit(IEnumerable<EventAndEnvelope> eventsAndEnvelopes)
        {
            var documents = eventsAndEnvelopes.Select(e => e.Envelope.ToBsonDocument().AddRange(e.Event.ToBsonDocument()));
            _collection.InsertMany(documents);
        }

        /// <inheritdoc/>
        public IEnumerable<EventAndEnvelope> GetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public EventSourceVersion GetVersionFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool HasEventsFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new NotImplementedException();
        }
    }
}
