using System.Data;
using NHibernate.SqlTypes;

namespace Bifrost.NHibernate.UserTypes
{
    public abstract class ValueTypeOfShort<T> : SimpleImmutableUserTypeBase<T>
    {
        public override SqlType[] SqlTypes
        {
            get { return new[] { new SqlType(DbType.Int16) }; }
        }
    }
}