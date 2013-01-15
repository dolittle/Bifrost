using System;
using System.Data;
using System.Reflection;
using Bifrost.Concepts;
using NHibernate;
using NHibernate.Engine;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// Represents a <see cref="NullSafeMapping">property mapping strategy</see> for handing Guids to and from an Oracle database
    /// </summary>
    public class OracleGuidMapping : NullSafeMapping
    {
#pragma warning disable 1591
        public override object Get(PropertyInfo property, IDataReader dr, string propertyName, ISessionImplementor session, object owner)
        {
            Guid result;
            var buffer = (byte[])NHibernateUtil.Binary.NullSafeGet(dr, propertyName, session, owner);
            if (null != buffer)
            {
                result = new Guid(buffer);
                Array.Clear(buffer, 0, buffer.Length);
                return result;
            }
            return Guid.Empty;
        }

        public override void Set(PropertyInfo property, object value, IDbCommand cmd, int index, ISessionImplementor session)
        {
            if (value == null)
                return;

            Guid guidValue = Guid.Empty;

            if (value is Guid)
                guidValue = (Guid) value;

            if (value is ConceptAs<Guid>)
                guidValue = (ConceptAs<Guid>) value;

            if(guidValue == Guid.Empty)
                throw new InvalidOperationException("Invalid type: " + value.GetType());

            var buffer = guidValue.ToByteArray();
            NHibernateUtil.Binary.NullSafeSet(cmd, buffer, index);
            Array.Clear(buffer, 0, buffer.Length);
        }
#pragma warning restore 1591
    }
}