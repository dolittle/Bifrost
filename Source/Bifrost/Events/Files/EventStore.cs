/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Bifrost.Applications;
using Bifrost.Serialization;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents a simple file based <see cref="IEventStore"/>
    /// </summary>
    public class EventStore : IEventStore
    {
        EventStoreConfiguration _configuration;
        IEventEnvelopes _eventEnvelopes;
        IApplicationResources _applicationResources;
        IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        IApplicationResourceResolver _applicationResourceResolver;
        ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="EventStore"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventStoreConfiguration"/> to use as configuration</param>
        /// <param name="applicationResources"><see cref="IApplicationResources"/> for working with <see cref="IApplicationResource">application resources</see></param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter"/> for working with conversion of <see cref="IApplicationResourceIdentifier"/></param>
        /// <param name="applicationResourceResolver"><see cref="IApplicationResourceResolver"/> for resolving <see cref="IApplicationResourceIdentifier"/> to concrete types</param> 
        /// <param name="eventEnvelopes"><see cref="IEventEnvelopes"/> for working with <see cref="EventEnvelope"/></param>
        /// <param name="serializer"><see cref="ISerializer"/> to use for serialization</param>
        public EventStore(
            EventStoreConfiguration configuration, 
            IApplicationResources applicationResources, 
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter,
            IApplicationResourceResolver applicationResourceResolver,
            IEventEnvelopes eventEnvelopes, 
            ISerializer serializer)
        {
            _configuration = configuration;
            _eventEnvelopes = eventEnvelopes;
            _applicationResources = applicationResources;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _applicationResourceResolver = applicationResourceResolver;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public CommittedEventStream GetFor(IEventSource eventSource)
        {
            var eventSourceId = eventSource.EventSourceId;

            var applicationResourceIdentifier = _applicationResources.Identify(eventSource);
            var eventSourceIdentifier = _applicationResourceIdentifierConverter.AsString(applicationResourceIdentifier);
            var eventPath = GetPathFor(eventSourceIdentifier, eventSource.EventSourceId);

            var files = Directory.GetFiles(eventPath).OrderBy(f => f);
            var eventFiles = files.Where(f => f.EndsWith(".event")).ToArray();
            var envelopeFiles = files.Where(f => f.EndsWith(".envelope")).ToArray();

            if (eventFiles.Length != envelopeFiles.Length) throw new ApplicationException($"There is a problem with event files for {eventSourceIdentifier} with Id {eventSourceId}");

            var events = new List<EventAndEnvelope>();

            for ( var eventIndex=0; eventIndex<eventFiles.Length; eventIndex++)
            {
                var envelopeFile = envelopeFiles[eventIndex];
                var eventFile = eventFiles[eventIndex];

                var envelopeAsJson = File.ReadAllText(envelopeFile);
                var eventAsJson = File.ReadAllText(eventFile);
                var envelopeValues = _serializer.GetKeyValuesFromJson(envelopeAsJson);


                var _eventId = (long)envelopeValues["EventId"];
                var _generation = (long)envelopeValues["Generation"];
                var _event = _applicationResourceIdentifierConverter.FromString((string)envelopeValues["Event"]);
                var _eventSourceId = Guid.Parse((string)envelopeValues["EventSourceId"]);
                var _eventSource = _applicationResourceIdentifierConverter.FromString((string)envelopeValues["EventSource"]);
                var _eventSourceVersion = EventSourceVersion.FromCombined(double.Parse(envelopeValues["Version"].ToString()));
                var _causedBy = (string)envelopeValues["CausedBy"];
                var _occurred = (DateTime)envelopeValues["Occurred"];
                    
                var envelope = new EventEnvelope(
                    _eventId,
                    (int)_generation,
                    _event,
                    _eventSourceId,
                    _eventSource,
                    _eventSourceVersion,
                    _causedBy,
                    _occurred
                );

                var eventType = _applicationResourceResolver.Resolve(envelope.Event);
                var eventInstance = _serializer.FromJson(eventType, eventAsJson) as IEvent;
                events.Add(new EventAndEnvelope(envelope, eventInstance));
            }

            return new CommittedEventStream(eventSourceId, events);
        }

        /// <inheritdoc/>
        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            foreach (var eventAndEnvelope in uncommittedEventStream)
            {
                var eventSourceIdentifier = _applicationResourceIdentifierConverter.AsString(eventAndEnvelope.Envelope.EventSource);
                var path = GetPathFor(eventSourceIdentifier, eventAndEnvelope.Envelope.EventSourceId);
                var eventId = GetNextEventId();

                var envelope = eventAndEnvelope.Envelope.WithEventId(eventId);

                var envelopeAsJson = _serializer.ToJson(envelope);
                var eventAsJson = _serializer.ToJson(eventAndEnvelope.Event);

                var eventPath = Path.Combine(path, $"{envelope.Version.Commit}.{envelope.Version.Sequence}.event");
                var envelopePath = Path.Combine(path, $"{envelope.Version.Commit}.{envelope.Version.Sequence}.envelope");

                File.WriteAllText(envelopePath, envelopeAsJson);
                File.WriteAllText(eventPath, eventAsJson);
            }

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, uncommittedEventStream);
            return committedEventStream;
        }

        /// <inheritdoc/>
        public EventSourceVersion GetLastCommittedVersionFor(IEventSource eventSource)
        {
            var applicationResourceIdentifier = _applicationResources.Identify(eventSource);
            var eventSourceIdentifier = _applicationResourceIdentifierConverter.AsString(applicationResourceIdentifier);
            var eventPath = GetPathFor(eventSourceIdentifier, eventSource.EventSourceId);

            var first = Directory.GetFiles(eventPath, "*.event").OrderByDescending(f => f).FirstOrDefault();
            if (first == null) return EventSourceVersion.Zero;

            var versionAsString = Path.GetFileNameWithoutExtension(first);
            var versionAsDouble = double.Parse(versionAsString, CultureInfo.InvariantCulture);

            return EventSourceVersion.FromCombined(versionAsDouble);
        }

        string GetPathFor(string eventSource)
        {
            var fullPath = Path.Combine(_configuration.Path, "EventStore", eventSource);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            return fullPath;
        }

        string GetPathFor(string eventSource, EventSourceId eventSourceId)
        {
            var fullPath = Path.Combine(GetPathFor(eventSource), eventSourceId.ToString());
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            return fullPath;
        }

        int GetNextEventId()
        {
            var id = 0;

            var idFile = Path.Combine(_configuration.Path, "LastEventID");

            if (File.Exists(idFile))
            {
                var idAsString = File.ReadAllText(idFile);
                int.TryParse(idAsString, out id);
            }

            id++;
            File.WriteAllText(idFile, id.ToString());

            return id;
        }

    }
}
