#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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