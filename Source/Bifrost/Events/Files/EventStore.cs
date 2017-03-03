/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
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
        ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="EventStore"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventStoreConfiguration"/> to use as configuration</param>
        /// <param name="applicationResources"><see cref="IApplicationResources"/> for working with <see cref="IApplicationResource">application resources</see></param>
        /// <param name="eventEnvelopes"><see cref="IEventEnvelopes"/> for working with <see cref="EventEnvelope"/></param>
        /// <param name="serializer"><see cref="ISerializer"/> to use for serialization</param>
        public EventStore(EventStoreConfiguration configuration, IApplicationResources applicationResources, IEventEnvelopes eventEnvelopes, ISerializer serializer)
        {
            _configuration = configuration;
            _eventEnvelopes = eventEnvelopes;
            _applicationResources = applicationResources;
            _serializer = serializer;
        }

#pragma warning disable 1591 // Xml Comments
        public CommittedEventStream GetFor(IEventSource eventSource)
        {
            var eventSourceId = eventSource.EventSourceId;
            var eventPath = GetPathFor(eventSource.GetType().Name, eventSourceId);
            var files = Directory.GetFiles(eventPath).OrderBy(f => f);
            var eventFiles = files.Where(f => f.EndsWith(".event")).ToArray();
            var envelopeFiles = files.Where(f => f.EndsWith(".envelope")).ToArray();
            var eventSourceIdentifier = _applicationResources.Identify(eventSource);
            var eventSourceStringIdentifier = _applicationResources.AsString(eventSourceIdentifier);

            if (eventFiles.Length != envelopeFiles.Length) throw new ApplicationException($"There is a problem with event files for {eventSourceStringIdentifier} with Id {eventSourceId}");

            for( var eventIndex=0; eventIndex<eventFiles.Length; eventIndex++)
            {
                

            }

            return new CommittedEventStream(eventSourceId);

            /*
            var events = new List<EventEnvelopeAndEvent>();

            foreach (var file in files)
            {
                var json = File.ReadAllText(file);


                _serializer.GetKeyValuesFromJson();

                var @event = _serializer.FromJson(target.Type, json) as IEvent;
                events.Add(new EventEnvelopeAndEvent(_eventEnvelopes.CreateFrom(eventSource, @event), @event));
            }
            return new CommittedEventStream(eventSourceId, events);*/

        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            foreach (var eventAndEnvelope in uncommittedEventStream)
            {
                var eventSourceIdentifier = _applicationResources.AsString(eventAndEnvelope.Envelope.EventSource);
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


        public EventSourceVersion GetLastCommittedVersionFor(IEventSource eventSource)
        {
            /*
            var eventPath = GetPathFor(eventSource.GetType().Name, eventSourceId);
            var first = Directory.GetFiles(eventPath).OrderByDescending(f => f).FirstOrDefault();
            if (first == null) return EventSourceVersion.Zero;

            var json = File.ReadAllText(first);
            var target = new EventHolder
            {
                Type = typeof(string),
                Version = EventSourceVersion.Zero,
                Event = string.Empty
            };

            _serializer.FromJson(target, json);

            return target.Version;*/

            throw new NotImplementedException();
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
#pragma warning restore 1591 // Xml Comments
    }
}
