using System;
using System.Data;
using Bifrost.NHibernate.UserTypes;
using NHibernate;

namespace Bifrost.Web
{
    public class TimestampType : ValueTypeOfDateTime<Timestamp>
    {
        public override object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            Timestamp timestamp = (DateTime)NHibernateUtil.DateTime.NullSafeGet(rs, names[0]);
            return timestamp;
        }

        public override void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            DateTime timestamp = value as Timestamp;
            NHibernateUtil.DateTime.NullSafeSet(cmd, timestamp, index);
        }
    }
}