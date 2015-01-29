#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
            var buffer = (byte[])NHibernateUtil.Binary.NullSafeGet(dr, propertyName, session, owner);
            if (null != buffer)
            {
                var result = new Guid(buffer);
                return result;
            }
            return Guid.Empty;
        }

        public override void Set(PropertyInfo property, object value, IDbCommand cmd, int index, ISessionImplementor session)
        {
            if (value == null)
                return;

            var guidValue = Guid.Empty;

            if (value is Guid)
                guidValue = (Guid) value;

            var guidAsConcept = value as ConceptAs<Guid>;
            if (guidAsConcept != null)
                guidValue = guidAsConcept.Value;

            if(guidValue == Guid.Empty)
                throw new InvalidOperationException("Invalid type: " + value.GetType());

            NHibernateUtil.Binary.NullSafeSet(cmd, guidValue.ToByteArray(), index);
        }
#pragma warning restore 1591
    }
}