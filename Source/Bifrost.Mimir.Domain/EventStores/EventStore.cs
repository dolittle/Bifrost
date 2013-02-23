#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using Bifrost.Domain;
using Bifrost.Mimir.Events.EventStores;

namespace Bifrost.Mimir.Domain.EventStores
{
    public class EventStore : AggregateRoot
    {
        public static readonly Guid SystemEventStoreId = new Guid("5E3FD0AD-FF67-43A6-933D-E5B9B68AF5C3");

        public EventStore(Guid eventSourceId)
            : base(eventSourceId)
        {
        }

        public void ReplayAll()
        {
            Apply(new AllEventsReplayed(Id));
        }
    }
}
