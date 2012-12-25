using System;
using NHibernate.UserTypes;
using System.Data;
using NHibernate.SqlTypes;
using NHibernate;

namespace Bifrost.NHibernate.UserTypes
{
    public class TypeUserType : IUserType
    {
        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public new bool Equals(object x, object y)
        {
            if (x == null)
                return false;
            else
                return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public bool IsMutable { get { return false; } }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var typeString = (string)NHibernateUtil.String.NullSafeGet(rs, names[0]);
            return Type.GetType(typeString);
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            var type = (Type)value;
            NHibernateUtil.String.NullSafeSet(cmd, type.AssemblyQualifiedName, index);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType { get { return typeof(Type); } }
        public global::NHibernate.SqlTypes.SqlType[] SqlTypes { get { return new[] { new SqlType(DbType.String) }; } }
    }
}
