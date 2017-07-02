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
using Bifrost.Logging;
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
        IFiles _files;

        /// <summary>
        /// Initializes a new instance of <see cref="EventStore"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventStoreConfiguration"/> to use as configuration</param>
        /// <param name="applicationResources"><see cref="IApplicationResources"/> for working with <see cref="IApplicationResource">application resources</see></param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter"/> for working with conversion of <see cref="IApplicationResourceIdentifier"/></param>
        /// <param name="applicationResourceResolver"><see cref="IApplicationResourceResolver"/> for resolving <see cref="IApplicationResourceIdentifier"/> to concrete types</param> 
        /// <param name="eventEnvelopes"><see cref="IEventEnvelopes"/> for working with <see cref="EventEnvelope"/></param>
        /// <param name="serializer"><see cref="ISerializer"/> to use for serialization</param>
        /// <param name="files"><see cref="IFiles"/> to work with files</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public EventStore(
            EventStoreConfiguration configuration, 
            IApplicationResources applicationResources, 
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter,
            IApplicationResourceResolver applicationResourceResolver,
            IEventEnvelopes eventEnvelopes,
            ISerializer serializer,
            IFiles files,
            ILogger logger)
        {
            logger.Information($"Using path : {configuration.Path}");
            _configuration = configuration;
            _eventEnvelopes = eventEnvelopes;
            _applicationResources = applicationResources;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _applicationResourceResolver = applicationResourceResolver;
            _serializer = serializer;
            _files = files;
        }

        /// <inheritdoc/>
        public IEnumerable<EventAndEnvelope> GetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var eventSourceIdentifier = _applicationResourceIdentifierConverter.AsString(eventSource);
            var eventPath = GetPathFor(eventSourceIdentifier, eventSourceId);

            var files = _files.GetFilesIn(eventPath).OrderBy(f => f);
            var eventFiles = files.Where(f => f.EndsWith(".event")).ToArray();
            var envelopeFiles = files.Where(f => f.EndsWith(".envelope")).ToArray();

            if (eventFiles.Length != envelopeFiles.Length) throw new Exception($"There is a problem with event files for {eventSourceIdentifier} with Id {eventSourceId}");

            var events = new List<EventAndEnvelope>();

            for ( var eventIndex=0; eventIndex<eventFiles.Length; eventIndex++)
            {
                var envelopeFile = envelopeFiles[eventIndex];
                var eventFile = eventFiles[eventIndex];

                var envelopeAsJson = _files.ReadString(Path.GetDirectoryName(envelopeFile), Path.GetFileName(envelopeFile));
                var eventAsJson = _files.ReadString(Path.GetDirectoryName(eventFile), Path.GetFileName(eventFile));
                var envelopeValues = _serializer.GetKeyValuesFromJson(envelopeAsJson);

                var _correllationId = Guid.Parse((string)envelopeValues["CorrelationId"]);
                var _eventId = Guid.Parse((string)envelopeValues["EventId"]);
                var _sequenceNumber = (long)envelopeValues["SequenceNumber"];
                var _sequenceNumberForEventType = (long)envelopeValues["SequenceNumberForEventType"];
                var _generation = (long)envelopeValues["Generation"];
                var _event = _applicationResourceIdentifierConverter.FromString((string)envelopeValues["Event"]);
                var _eventSourceId = Guid.Parse((string)envelopeValues["EventSourceId"]);
                var _eventSource = _applicationResourceIdentifierConverter.FromString((string)envelopeValues["EventSource"]);
                var _eventSourceVersion = EventSourceVersion.FromCombined(double.Parse(envelopeValues["Version"].ToString()));
                var _causedBy = (string)envelopeValues["CausedBy"];
                var _occurred = (DateTime)envelopeValues["Occurred"];
                    
                var envelope = new EventEnvelope(
                    _correllationId,
                    _eventId,
                    _sequenceNumber,
                    _sequenceNumberForEventType,
                    (int)_generation,
                    _event,
                    _eventSourceId,
                    _eventSource,
                    _eventSourceVersion,
                    _causedBy,
                    _occurred
                );

                var eventType = _applicationResourceResolver.Resolve(envelope.Event);
                var @event = Activator.CreateInstance(eventType, eventSourceId) as IEvent;
                _serializer.FromJson(@event, eventAsJson);
                events.Add(new EventAndEnvelope(envelope, @event));
            }

            return events;
        }

        /// <inheritdoc/>
        public void Commit(IEnumerable<EventAndEnvelope> eventsAndEnvelopes)
        {
            foreach (var eventAndEnvelope in eventsAndEnvelopes)
            {
                var eventSourceIdentifierAsString = _applicationResourceIdentifierConverter.AsString(eventAndEnvelope.Envelope.EventSource);
                var path = GetPathFor(eventSourceIdentifierAsString, eventAndEnvelope.Event.EventSourceId);

                var envelope = eventAndEnvelope.Envelope;
                var envelopeAsJson = _serializer.ToJson(eventAndEnvelope.Envelope);
                var eventAsJson = _serializer.ToJson(eventAndEnvelope.Event);

                var eventFileName = $"{envelope.Version.Commit}.{envelope.Version.Sequence}.event";
                var envelopeFileName = $"{envelope.Version.Commit}.{envelope.Version.Sequence}.envelope";

                _files.WriteString(path, eventFileName, eventAsJson);
                _files.WriteString(path, envelopeFileName, envelopeAsJson);
            }
        }

        /// <inheritdoc/>
        public bool HasEventsFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var eventSourceIdentifier = _applicationResourceIdentifierConverter.AsString(eventSource);
            var eventPath = GetPathFor(eventSourceIdentifier, eventSourceId);
            var files = _files.GetFilesIn(eventPath, "*.event");
            return files.Count() > 0;
        }

        /// <inheritdoc/>
        public EventSourceVersion GetVersionFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            var eventSourceIdentifier = _applicationResourceIdentifierConverter.AsString(eventSource);
            var eventPath = GetPathFor(eventSourceIdentifier, eventSourceId);

            var first = _files.GetFilesIn(eventPath,"*.event").OrderByDescending(f => f).FirstOrDefault();
            if (first == null) return EventSourceVersion.Zero;

            var versionAsString = Path.GetFileNameWithoutExtension(first);
            var versionAsDouble = double.Parse(versionAsString, CultureInfo.InvariantCulture);

            return EventSourceVersion.FromCombined(versionAsDouble);
        }

        string GetPathFor(string eventSource)
        {
            var fullPath = Path.Combine(_configuration.Path, "EventStore", eventSource);
            return fullPath;
        }

        string GetPathFor(string eventSource, EventSourceId eventSourceId)
        {
            var fullPath = Path.Combine(GetPathFor(eventSource), eventSourceId.ToString());
            return fullPath;
        }

    }
}
