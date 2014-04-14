#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// A User type that wraps up transforming between a .NET type of Guid and the RAW(16) data type in Oracle
    /// </summary>
    public class OracleGuidUserType : IUserType
    {
        static SqlType[] _types = new [] { new SqlType(DbType.Binary) };
        static Type _type = typeof (Guid);
#pragma warning disable 1591
        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public new bool Equals(object x, object y)
        {
            return (x != null && x.Equals(y));
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public bool IsMutable
        {
            get { return true; }
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var result = Guid.Empty;
            var buffer = (byte[])NHibernateUtil.Binary.NullSafeGet(rs, names[0]);
            if (buffer == null)
                return result;

            result = new Guid(buffer);
            return result;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (null == value) 
                return;

            var guidValue = (Guid)value;
            var buffer = guidValue.ToByteArray();
            NHibernateUtil.Binary.NullSafeSet(cmd, buffer, index);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType
        {
            get { return _type; }
        }

        public SqlType[] SqlTypes
        {
            get { return _types; }
        }
#pragma warning restore 1591
    }
}