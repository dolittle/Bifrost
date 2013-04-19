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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Events;
using Bifrost.Read;

namespace Bifrost.Mimir.Views.EventStores
{
    public class AllEventSubscriptions : IQueryFor<EventSubscription>
    {
        IEventSubscriptions _eventSubscriptions;

        public AllEventSubscriptions(IEventSubscriptions eventSubscriptions)
        {
            _eventSubscriptions = eventSubscriptions;
        }

        public IQueryable<EventSubscription> Query
        {
            get 
            {
                return _eventSubscriptions.GetAll().Select(e => new EventSubscription
                        {
                            Owner = e.Owner.Name,
                            OwnerNamespace = e.Owner.Namespace,
                            OwnerAssembly = e.Owner.Assembly.FullName,
                            Event = e.EventName,
                            LastEventId = e.LastEventId
                        }).AsQueryable();
            }
        }
    }
}
