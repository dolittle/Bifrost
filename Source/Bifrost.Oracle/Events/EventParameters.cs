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
using System.Reflection;
using Bifrost.Events;
using Bifrost.Serialization;
using Oracle.DataAccess.Client;

namespace Bifrost.Oracle.Events
{
    public class EventParameters : IEventParameters
    {
        public const string ID = "ID";
        public const string COMMANDCONTEXT = "COMMANDCONTEXT";
        public const string NAME = "NAME";
        public const string LOGICALNAME = "LOGICALNAME";
        public const string EVENTSOURCEID = "EVENTSOURCEID";
        public const string EVENTSOURCE = "EVENTSOURCE";
        public const string GENERATION = "GENERATION";
        public const string DATA = "DATA";
        public const string CAUSEDBY = "CAUSEDBY";
        public const string ORIGIN = "ORIGIN";
        public const string OCCURED = "OCCURED";
        public const string VERSION = "VERSION";

        readonly ISerializer _serializer;
        readonly IEventMigrationHierarchyManager _eventMigrationHierarchyManager;
        readonly PropertyInfo[] _eventProperties;
        readonly Dictionary<Type, PropertyInfo[]> _cachedEventProperties = new Dictionary<Type, PropertyInfo[]>();
        readonly Dictionary<Type, EventMetaData> _cachedEventMetaData = new Dictionary<Type, EventMetaData>();

        public EventParameters(ISerializer serializer, IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _serializer = serializer;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
            _eventProperties = typeof(IEvent).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Parameters = new[]
                             {
                                 new EventParameter(COMMANDCONTEXT,OracleDbType.Raw,16), 
                                 new EventParameter(NAME,OracleDbType.NVarchar2,512),
                                 new MetaDataEventParameter(LOGICALNAME, OracleDbType.NVarchar2, 512, m => m.LogicalName), 
                                 new EventParameter(EVENTSOURCEID,OracleDbType.Raw,16), 
                                 new EventParameter(EVENTSOURCE,OracleDbType.NVarchar2,512),
                                 new MetaDataEventParameter(GENERATION, OracleDbType.Int32, m => m.Generation),
                                 new DataEventParameter(GetDataProperties, d => _serializer.ToJson(d,null)), 
                                 new EventParameter(CAUSEDBY,OracleDbType.NVarchar2,512), 
                                 new EventParameter(ORIGIN,OracleDbType.NVarchar2,512), 
                                 new EventParameter(OCCURED,OracleDbType.Date),
                                 new EventParameter(VERSION,OracleDbType.Double), 
                                 new IdEventParameter()
                             };
        }

        public EventParameter[] Parameters { get; private set; }

        IEnumerable<PropertyInfo> GetDataProperties(IEvent @event)
        {
            var eventType = @event.GetType();
            if (!_cachedEventProperties.Keys.Contains(eventType))
            {
                _cachedEventProperties.Add(eventType,
                                           eventType.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                                   BindingFlags.DeclaredOnly));
            }

            return _cachedEventProperties[eventType];
        }

        public EventMetaData GetMetaDataFor(IEvent @event)
        {
            var eventType = @event.GetType();
            if (!_cachedEventMetaData.Keys.Contains(eventType))
            {
                var logicalEventType = _eventMigrationHierarchyManager.GetLogicalTypeForEvent(eventType);
                var migrationLevel = _eventMigrationHierarchyManager.GetCurrentMigrationLevelForLogicalEvent(logicalEventType);
                var name = string.Format("{0}, {1}", logicalEventType.FullName, logicalEventType.Assembly.GetName().Name);
                _cachedEventMetaData.Add(eventType, new EventMetaData { Generation = migrationLevel, LogicalName = name });
            }

            return _cachedEventMetaData[eventType];
        }

        public IEnumerable<OracleParameter> BuildFromEvent(int position, IEvent @event)
        {
            return Parameters.Select(parameter => parameter.BuildParameter(_eventProperties, @event, GetMetaDataFor(@event), position));
        }
    }
}