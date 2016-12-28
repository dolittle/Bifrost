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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IProcessMethodInvoker"/>
    /// </summary>
	public class ProcessMethodInvoker : IProcessMethodInvoker
	{
        /// <summary>
        /// Name of method that any event subscriber needs to be called in order to be recognized by the convention
        /// </summary>
        public const string ProcessMethodName = "Process";

		readonly Dictionary<Type, Dictionary<Type, MethodInfo>> _typesCommandHandleMethods = new Dictionary<Type, Dictionary<Type, MethodInfo>>();

#pragma warning disable 1591 // Xml Comments
        public bool TryProcess(object instance, IEvent @event)
		{
			var instanceType = instance.GetType();
			if (!_typesCommandHandleMethods.ContainsKey(instanceType))
				Register(instanceType);

			var handleMethods = _typesCommandHandleMethods[instanceType];
			Register(instanceType);
			var commandType = @event.GetType();
			if (handleMethods.ContainsKey(commandType))
			{
				handleMethods[commandType].Invoke(instance, new[] { @event });
				return true;
			}

			return false;
		}

		public void Register(Type typeWithHandleMethods)
		{
			var handleMethods = GetHandleMethods(typeWithHandleMethods);
			_typesCommandHandleMethods[typeWithHandleMethods] = handleMethods;
		}
#pragma warning restore 1591 // Xml Comments

        static Dictionary<Type, MethodInfo> GetHandleMethods(Type typeWithHandleMethods)
		{
            var allMethods = typeWithHandleMethods.GetRuntimeMethods().Where(m => !m.IsStatic || m.IsPublic);
            var query = from m in allMethods
			            where m.Name.Equals(ProcessMethodName) &&
			                  m.GetParameters().Length == 1 &&
			                  typeof(IEvent)
                                .GetTypeInfo().IsAssignableFrom(m.GetParameters()[0].ParameterType.GetTypeInfo())
			            select m;

			return query.ToDictionary(m => m.GetParameters()[0].ParameterType);
		}
	}
}