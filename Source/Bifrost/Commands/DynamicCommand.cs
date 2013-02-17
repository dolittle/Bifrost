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
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Bifrost.Domain;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="DynamicCommand"/>
    /// </summary>
    /// <remarks>
    /// The purpose of this representation of a <see cref="ICommand"/> is to provide 
    /// a dynamic and generic command type that represents a method on an <see cref="AggregatedRoot"/>
    /// </remarks>
    public partial class DynamicCommand : DynamicObject, ICommand
    {
        readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the type of <see cref="AggregatedRoot"/> the command is targetting
        /// </summary>
        public Type AggregatedRootType { get; set; }

        /// <summary>
        /// Gets or sets the Id of the command - which is also the Id of the <see cref="AggregatedRoot"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the method that will be called
        /// </summary>
        public MethodInfo Method { get; set; }


        /// <summary>
        /// Set a value in the command based upon the property name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyName, object value)
        {
            _values[propertyName] = value;
        }

        /// <summary>
        /// Invoke the method represented by the command on a given <see cref="AggregatedRoot"/>
        /// </summary>
        /// <param name="aggregatedRoot"><see cref="AggregatedRoot"/> to invoke the method on</param>
        public void InvokeMethod(AggregatedRoot aggregatedRoot)
        {
            var parameters = Method.GetParameters();
            var values = parameters.Select(parameter => _values[parameter.Name]);
            Method.Invoke(aggregatedRoot, values.ToArray());
        }

#pragma warning disable 1591 // Xml Comments
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _values[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            var actualName = new string(binder.Name[0],1).ToLowerInvariant() + binder.Name.Substring(1);
            if (_values.ContainsKey(actualName))
                result = _values[actualName];

            return true;
        }
#pragma warning restore 1591 // Xml Comments


    }
}