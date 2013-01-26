using System.Data;
using System.Reflection;
using NHibernate;
using NHibernate.Engine;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// Represents a <see cref="NullSafeMapping">property mapping strategy</see> that uses the inbuilt type inference within NHibernate
    /// </summary>
    public class InferredMapping : NullSafeMapping
    {
#pragma warning disable 1591
        public override object Get(PropertyInfo property, IDataReader dr, string propertyName, ISessionImplementor session, object owner)
        {
            return NHibernateUtil.GuessType(property.PropertyType).NullSafeGet(dr, propertyName, session, owner); ;
        }

        public override void Set(PropertyInfo property, object value, IDbCommand cmd, int index, ISessionImplementor session)
        {
            var propValue = property.GetValue(value, null);
            NHibernateUtil.GuessType(property.PropertyType).NullSafeSet(cmd, propValue, index, session);
        }
#pragma warning restore 1591
    }
}