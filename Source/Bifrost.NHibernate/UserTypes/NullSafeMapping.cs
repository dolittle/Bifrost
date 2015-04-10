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

using System.Data;
using System.Reflection;
using NHibernate.Engine;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// Defines a strategy for mapping properties to commands and from data readers with NHibernate
    /// </summary>
    public abstract class NullSafeMapping
    {
        /// <summary>
        /// Retrieves the value of a custom type from a data reader
        /// </summary>
        /// <param name="property">Property Info representing the mapped property</param>
        /// <param name="dr">Date Reader</param>
        /// <param name="propertyName">Name of the property being mapped</param>
        /// <param name="session">NHibernate Session</param>
        /// <param name="owner">Owner object/param>
        /// <returns></returns>
        public abstract object Get(PropertyInfo property, IDataReader dr, string propertyName, ISessionImplementor session, object owner);
        /// <summary>
        /// Sets the value of a custom type in to an IDbCommand
        /// </summary>
        /// <param name="property">Property Info representing the mapped property</param>
        /// <param name="value">The value to set into the Property</param>
        /// <param name="cmd">Database Command</param>
        /// <param name="index">Index position of the property</param>
        /// <param name="session">NHibernate Session</param>
        public abstract void Set(PropertyInfo property, object value, IDbCommand cmd, int index, ISessionImplementor session);
    }
}