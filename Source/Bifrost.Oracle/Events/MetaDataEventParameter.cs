using System;
using System.Reflection;
using Bifrost.Events;
using Oracle.DataAccess.Client;

namespace Bifrost.Oracle.Events
{
    public class MetaDataEventParameter : EventParameter
    {
        readonly Func<EventMetaData, object> _valueFromMetaData;

        public MetaDataEventParameter(string name, OracleDbType dbType, int size, Func<EventMetaData, object> valueFromMetaData)
            : base(name, dbType, size)
        {
            _valueFromMetaData = valueFromMetaData;
        }

        public MetaDataEventParameter(string name, OracleDbType dbType, Func<EventMetaData, object> valueFromMetaData)
            : this(name, dbType, 0, valueFromMetaData)
        {
        }

        protected override object GetValue(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData)
        {
            return _valueFromMetaData.Invoke(metaData);
        }
    }
}