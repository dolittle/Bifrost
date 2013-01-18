using System.Data;
using System.Reflection;
using Bifrost.Events;
using Oracle.DataAccess.Client;

namespace Bifrost.Oracle.Events
{
    public class IdEventParameter : EventParameter
    {
        public IdEventParameter()
            : base(EventParameters.ID, OracleDbType.Int64, 10)
        {
        }

        public override OracleParameter BuildParameter(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData, int position)
        {
            return new OracleParameter(GetFormattedParameterName(position), DbType, ParameterDirection.Output);
        }

        protected override object GetValue(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData)
        {
            return null;
        }
    }
}