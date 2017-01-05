/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Data;
using Bifrost.Events;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Bifrost.NHibernate.Events
{
    public class EventSourceVersionCustomType : IUserType
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
            if (value == null) return null;
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
            var versionAsDouble = (double)NHibernateUtil.Double.NullSafeGet(rs, names[0]);
            var version = EventSourceVersion.FromCombined(versionAsDouble);
            return version;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            var version = (EventSourceVersion)value;
            var versionAsDouble = version.Combine();
            NHibernateUtil.Double.NullSafeSet(cmd, versionAsDouble, index);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType { get { return typeof(EventSourceVersion); } }
        public global::NHibernate.SqlTypes.SqlType[] SqlTypes { get { return new[] { new SqlType(DbType.Double) }; } }
        
    }
}
