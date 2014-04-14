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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Tasks;

namespace Bifrost.Mimir.Read.EventStores
{
    public class ResetAllEventsForAllSubscriptionsTask : Task
    {
        public override TaskOperation[] Operations
        {
            get { return new TaskOperation[] { Perform }; }
        }

        public int PageNumber { get; set; }

        IEventSubscriptionManager _eventSubscriptionManager;
        IEventStore _eventStore;

        public ResetAllEventsForAllSubscriptionsTask(
                IEventSubscriptionManager eventSubcriptionManager,
                IEventStore eventStore
            )
        {
            _eventSubscriptionManager = eventSubcriptionManager;
            _eventStore = eventStore;
        }


        void Perform(Task task, int operationIndex)
        {
            IEnumerable<IEvent> events;
            do
            {
                events = _eventStore.GetBatch(PageNumber, 10);
                if (events.Count() <= 0)
                    break;

                var actualEvents = events.Where(e => !e.GetType().Namespace.Contains("Mimir"));
                _eventSubscriptionManager.Process(actualEvents);
                PageNumber++;
                Progress();
            } while (events.Count() > 0 && events != null);
        }
    }
}
