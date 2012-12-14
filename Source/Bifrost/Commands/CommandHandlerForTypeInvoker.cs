#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
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
using Bifrost.Execution;
using Bifrost.Extensions;
#if(NETFX_CORE)
using System.Linq;
using System.Reflection;
#endif

namespace Bifrost.Commands
{
	/// <summary>
	/// Represents a <see cref="ICommandHandlerInvoker">ICommandHandlerInvoker</see>
	/// </summary>
	[Singleton]
	public class CommandHandlerForTypeInvoker : ICommandHandlerInvoker
	{
	    readonly IContainer _container;
	    readonly ITypeDiscoverer _typeDiscoverer;
		readonly Dictionary<Type, dynamic[]> _handlersMap = new Dictionary<Type, dynamic[]>();
		Type[] _handlerTypes;

	    /// <summary>
	    /// Initializes a new instance of <see cref="CommandHandlerForTypeInvoker">CommandHandlerForTypeInvoker</see>
	    /// </summary>
	    /// <param name="container"><see cref="IContainer"/> to use for creating instances of CommandHandlers discovered</param>
	    /// <param name="typeDiscoverer">A <see cref="ITypeDiscoverer">ITypeDiscoverer</see> used for discovering <see cref="ICommandHandlerForType{T}">ICommandHandlerForType</see> implementations</param>
	    public CommandHandlerForTypeInvoker(IContainer container, ITypeDiscoverer typeDiscoverer)
		{
	        _container = container;
	        _typeDiscoverer = typeDiscoverer;
			InitializeHandlers();
		}

		void InitializeHandlers()
		{
			_handlerTypes = _typeDiscoverer.FindMultiple(typeof (ICommandHandlerForType<>));
		}

#pragma warning disable 1591 // Xml Comments
		public bool TryHandle(ICommand command)
		{
			if (IsCommandForType(command))
			{
				var targetType = GetTargetTypeFromCommand(command);
				var handlers = GetHandlers(targetType);
                handlers.ForEach(h => h.Handle((dynamic)command));
			}

			return false;
		}
#pragma warning restore 1591 // Xml Comments

		private dynamic[] GetHandlers(Type targetType)
		{
			if( _handlersMap.ContainsKey(targetType))
			{
				return _handlersMap[targetType];
			} else
			{
				var	handlers = new List<dynamic>();
				foreach( var type in _handlerTypes )
				{
					var genericType = type.MakeGenericType(targetType);
					var instance = _container.Get(genericType);
					handlers.Add(instance);
				}
				var handlersAsArray = handlers.ToArray();
				_handlersMap[targetType] = handlersAsArray;
				return handlersAsArray;
			}
		}


		private static Type GetTargetTypeFromCommand(ICommand command)
		{
			var baseType = command.GetType()
#if(NETFX_CORE)
                .GetTypeInfo().ImplementedInterfaces.Where(t=>t.Name == typeof(ICommandForType<>).Name).Single();
            var arguments = baseType.GetTypeInfo().GenericTypeArguments;
#else
                .GetInterface(typeof(ICommandForType<>).Name, false);
            var arguments = baseType.GetGenericArguments();
#endif

            if ( null != arguments && arguments.Length == 1 )
				return arguments[0];

			return null;
		}

		private static bool IsCommandForType(ICommand command)
		{
		    var isCommandForType = command.GetType().HasInterface(typeof(ICommandForType<>));
			return isCommandForType;
		}
	}
}