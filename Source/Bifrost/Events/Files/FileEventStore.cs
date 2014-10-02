#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Bifrost.Serialization;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents a simple file based <see cref="IEventStore"/>
    /// </summary>
    public class FileEventStore : IEventStore
    {
        FileEventStoreConfiguration _configuration;
        ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="FileEventStore"/>
        /// </summary>
        /// <param name="configuration"><see cref="FileEventStoreConfiguration"/> to use as configuration</param>
        /// <param name="serializer"><see cref="ISerializer"/> to use for serialization</param>
        public FileEventStore(FileEventStoreConfiguration configuration, ISerializer serializer)
        {
            _configuration = configuration;
            _serializer = serializer;
        }

#pragma warning disable 1591 // Xml Comments
        public CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId)
        {
            var eventPath = GetPathFor(eventSource.GetType().Name, eventSourceId);
            var files = Directory.GetFiles(eventPath).OrderBy(f => f);

            var stream = new CommittedEventStream(eventSourceId);

            var target = new
            {
                Type = typeof(string),
                Version = EventSourceVersion.Zero,
                Event = string.Empty
            };

            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                _serializer.FromJson(target, json);

                var @event = _serializer.FromJson(target.Type, json) as IEvent;
                stream.Append(new[] { @event });
            }
            return stream;
        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            foreach (var @event in uncommittedEventStream)
            {
                var eventPath = GetPathFor(Type.GetType(@event.EventSource).Name, @event.EventSourceId);

                var json = _serializer.ToJson(new
                {
                    Type = @event.GetType(),
                    Version = @event.Version,
                    Event = _serializer.ToJson(@event)
                });

                var id = 1;
                var first = Directory.GetFiles(eventPath).OrderByDescending(f => f).FirstOrDefault();
                if (first != null) id = int.Parse(first) + 1;

                File.WriteAllText(string.Format("{0}\\{1}",eventPath,id), json);
            }

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId);
            committedEventStream.Append(uncommittedEventStream);
            return committedEventStream;
        }

        public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
        {
            var eventPath = GetPathFor(eventSource.GetType().Name, eventSourceId);
            var first = Directory.GetFiles(eventPath).OrderByDescending(f => f).FirstOrDefault();
            if (first == null) return EventSourceVersion.Zero;

            var json = File.ReadAllText(first);
            var target = new
            {
                Type = typeof(string),
                Version = EventSourceVersion.Zero,
                Event = string.Empty
            };

            _serializer.FromJson(target, json);

            return target.Version;
        }

        public IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEvent> GetAll()
        {
            throw new NotImplementedException();
        }


        string GetPathFor(string eventSource, Guid eventSourceId)
        {
            var fullPath = Path.Combine(_configuration.Path, "EventStore", eventSource, eventSourceId.ToString());
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            return fullPath;
        }
#pragma warning restore 1591 // Xml Comments


        
    }
}
