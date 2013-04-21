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
using System;
using System.Linq;
using System.Reflection;
using Bifrost.Events;
using Oracle.DataAccess.Client;

namespace Bifrost.Oracle.Events
{
    public class EventParameter
    {
        readonly string _name;
        readonly OracleDbType _dbType;
        readonly int _size;

        protected string Name { get { return _name; } }
        protected OracleDbType DbType { get { return _dbType; } }
        protected int Size { get { return _size; } }

        public EventParameter(string name, OracleDbType dbType, int size)
        {
            _name = name;
            _dbType = dbType;
            _size = size;
        }

        public EventParameter(string name, OracleDbType dbType)
            : this(name, dbType, 0)
        {
        }

        public virtual OracleParameter BuildParameter(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData, int position)
        {
            var param = _size > 0 ? new OracleParameter(GetFormattedParameterName(position), _dbType, _size) : new OracleParameter(GetFormattedParameterName(position), _dbType);
            param.Value = GetValue(propertiesOnEvent, @event, metaData);
            return param;
        }

        protected virtual object GetValue(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData)
        {
            var property = propertiesOnEvent.Single(pi => pi.Name.Equals(_name, StringComparison.InvariantCultureIgnoreCase));
            var value = property.GetValue(@event, null);

            if (value is Guid)
                return ((Guid)value).ToByteArray();

            if (value is EventSourceVersion)
                return ((EventSourceVersion)value).Combine();

            return value;
        }

        protected string GetFormattedParameterName(int position)
        {
            return string.Concat(_name, "_", position);
        }
    }
}