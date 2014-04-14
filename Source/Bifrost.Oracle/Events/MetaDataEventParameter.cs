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
using System.Reflection;
using Bifrost.Events;
using Oracle.DataAccess.Client;

namespace Bifrost.Oracle.Events
{
    public class MetaDataEventParameter : EventParameter
    {
        readonly Func<EventMetaData, object> _valueFromMetaData;

        public MetaDataEventParameter(string name, OracleDbType dbType, int size, Func<EventMetaData, object> valueFromMetaData)
            : base(name, dbType, size)
        {
            _valueFromMetaData = valueFromMetaData;
        }

        public MetaDataEventParameter(string name, OracleDbType dbType, Func<EventMetaData, object> valueFromMetaData)
            : this(name, dbType, 0, valueFromMetaData)
        {
        }

        protected override object GetValue(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData)
        {
            return _valueFromMetaData.Invoke(metaData);
        }
    }
}