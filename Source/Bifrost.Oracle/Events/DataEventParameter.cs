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
using System.Dynamic;
using System.Reflection;
using Bifrost.Events;
using Oracle.DataAccess.Client;

namespace Bifrost.Oracle.Events
{
    public class DataEventParameter : EventParameter
    {
        readonly Func<IEvent, IEnumerable<PropertyInfo>> _getDataProperties;
        readonly Func<dynamic, string> _serializer;

        public DataEventParameter(Func<IEvent, IEnumerable<PropertyInfo>> getDataProperties, Func<dynamic, string> serializer)
            : base(EventParameters.DATA, OracleDbType.NClob, 0)
        {
            _getDataProperties = getDataProperties;
            _serializer = serializer;
        }

        public override OracleParameter BuildParameter(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData, int position)
        {
            var value = GetValue(propertiesOnEvent, @event, metaData) as string;

            var param = new OracleParameter(GetFormattedParameterName(position), DbType);
            param.Size = value.Length;
            param.Value = value;
            return param;
        }

        protected override object GetValue(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData)
        {
            return SerializeDataPropertiesToString(_getDataProperties.Invoke(@event), @event);
        }

        string SerializeDataPropertiesToString(IEnumerable<PropertyInfo> properties, IEvent @event)
        {
            var eventData = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)eventData;
            foreach (var property in properties)
            {
                dictionary[property.Name] = property.GetValue(@event, null);
            }
            return _serializer.Invoke(eventData);
        }
    }
}