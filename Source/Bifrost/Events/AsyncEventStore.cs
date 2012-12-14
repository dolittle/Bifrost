#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Threading.Tasks;

namespace Bifrost.Events
{
    /// <summary>
    /// An asynchronous <see cref="IEventStore"/>
    /// </summary>
    public class AsyncEventStore : IEventStore
    {
        IEventStore _actualEventStore;

        /// <summary>
        /// Initializes an instance of <see cref="AsyncEventStore"/>
        /// </summary>
        /// <param name="eventStore">The concrete <see cref="EventStore"/> used for the actual operations</param>
        public AsyncEventStore(EventStore eventStore)
        {
            _actualEventStore = eventStore;
        }

#pragma warning disable 1591 // Xml Comments
        public CommittedEventStream Load(Type aggregatedRootType, Guid aggregateId)
        {
            return _actualEventStore.Load(aggregatedRootType, aggregateId);
        }

        public void Save(UncommittedEventStream eventsToSave)
        {
            Task.Factory.StartNew(() => _actualEventStore.Save(eventsToSave));
        }

        public EventSourceVersion GetLastCommittedVersion(Type aggregatedRootType, Guid aggregateId)
        {
            return _actualEventStore.GetLastCommittedVersion(aggregatedRootType, aggregateId);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
