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