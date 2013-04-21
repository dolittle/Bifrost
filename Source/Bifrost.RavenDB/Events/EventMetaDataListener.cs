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
using Bifrost.Events;
using Bifrost.Extensions;
using Raven.Client.Listeners;
using Raven.Json.Linq;

namespace Bifrost.RavenDB.Events
{
    public class EventMetaDataListener : IDocumentStoreListener
    {
        const string Generation = "Generation";
        const string LogicalEventType = "LogicalEventType";

        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

        public EventMetaDataListener(IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
        }

        public void AfterStore(string key, object entityInstance, RavenJObject metadata)
        {
        }

        public bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original)
        {
            var eventType = entityInstance.GetType();
            if( !eventType.HasInterface<IEvent>() )
                return false;

            var logicalEventType = _eventMigrationHierarchyManager.GetLogicalTypeForEvent(eventType);
            var migrationLevel = _eventMigrationHierarchyManager.GetCurrentMigrationLevelForLogicalEvent(logicalEventType);
            metadata[Generation] = migrationLevel;
            metadata[LogicalEventType] = string.Format("{0}, {1}", logicalEventType.FullName, logicalEventType.Assembly.GetName().Name);
            return false;
        }
    }
}
