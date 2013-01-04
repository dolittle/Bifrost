using System;
using System.Data;
using System.Reflection;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.Type;
using NHibernate.UserTypes;


namespace Bifrost.NHibernate.Concepts
{
    public class ConceptValueType<T> : IUserType
    {
        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
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

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            var valueContainer = Activator.CreateInstance(typeof(T));

            var valueProperty = ValueProperty;
            var valueType = valueProperty.PropertyType;

            object value = null;

            var nullableType = GetNullableType(valueType);
            if (nullableType != null)
            {
                value = nullableType.NullSafeGet(rs, names[0]);
                valueProperty.SetValue(valueContainer, value, null);
            }

            return valueContainer;
        }


        public void NullSafeSet(System.Data.IDbCommand cmd, object valueContainer, int index)
        {
            var valueProperty = ValueProperty;
            var valueType = valueProperty.PropertyType;
            var value = valueProperty.GetValue(valueContainer, null);
            var nullableType = GetNullableType(valueType);

            if (nullableType != null)
                nullableType.NullSafeSet(cmd, value, index);
        }

        public Type ReturnedType { get { return typeof(T); } }
        public SqlType[] SqlTypes
        {
            get
            {
                var valueType = ValueProperty.PropertyType;

                var dbType = DbType.AnsiString;
                if (valueType == typeof(long))
                    dbType = DbType.Int64;
                if (valueType == typeof(int))
                    dbType = DbType.Int32;
                if (valueType == typeof(short))
                    dbType = DbType.Int16;
                if (valueType == typeof(string))
                    dbType = DbType.AnsiString;
                if (valueType == typeof(Guid))
                    dbType = DbType.Guid;

                return new[] { new SqlType(dbType) };
            }
        }

        PropertyInfo _valueProperty;
        PropertyInfo ValueProperty
        {
            get
            {
                if (_valueProperty == null)
                    _valueProperty = typeof(T).GetProperty("Value");

                return _valueProperty;
            }
        }

        NullableType GetNullableType(Type valueType)
        {
            NullableType nullableType = null;

            if (valueType == typeof(long))
                nullableType = NHibernateUtil.Int64;
            if (valueType == typeof(int))
                nullableType = NHibernateUtil.Int32;
            if (valueType == typeof(short))
                nullableType = NHibernateUtil.Int16;
            if (valueType == typeof(string))
                nullableType = NHibernateUtil.String;
            if (valueType == typeof(Guid))
                nullableType = NHibernateUtil.Guid;
            return nullableType;
        }
    }
}
