/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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

		readonly Dictionary<Type, Dictionary<Type, MethodInfo>> _typesWithProcessMethods = new Dictionary<Type, Dictionary<Type, MethodInfo>>();

#pragma warning disable 1591 // Xml Comments
        public bool TryProcess(object instance, IEvent @event)
		{
			var instanceType = instance.GetType();
			if (!_typesWithProcessMethods.ContainsKey(instanceType))
				Register(instanceType);

			var handleMethods = _typesWithProcessMethods[instanceType];
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
			var handleMethods = GetProcessMethods(typeWithHandleMethods);
			_typesWithProcessMethods[typeWithHandleMethods] = handleMethods;
		}
#pragma warning restore 1591 // Xml Comments

        static Dictionary<Type, MethodInfo> GetProcessMethods(Type typeWithHandleMethods)
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