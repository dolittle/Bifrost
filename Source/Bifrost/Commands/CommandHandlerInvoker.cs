/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
	/// <summary>
	/// Represents a <see cref="ICommandHandlerInvoker">ICommandHandlerInvoker</see> for handling
	/// command handlers that have methods called Handle() and takes specific <see cref="ICommand">commands</see>
	/// in as parameters
	/// </summary>
	[Singleton]
	public class CommandHandlerInvoker : ICommandHandlerInvoker
	{
        const string HandleMethodName = "Handle";

		readonly ITypeDiscoverer _discoverer;
	    readonly IContainer _container;
        readonly Dictionary<Type, MethodInfo> _commandHandlers = new Dictionary<Type, MethodInfo>();
	    bool _initialized;

	    /// <summary>
	    /// Initializes a new instance of <see cref="CommandHandlerInvoker">CommandHandlerInvoker</see>
	    /// </summary>
	    /// <param name="discoverer">A <see cref="ITypeDiscoverer"/> to use for discovering <see cref="IHandleCommands">command handlers</see></param>
	    /// <param name="container">A <see cref="IContainer"/> to use for getting instances of objects</param>
	    public CommandHandlerInvoker(ITypeDiscoverer discoverer, IContainer container)
		{
			_discoverer = discoverer;
		    _container = container;
	        _initialized = false;
		}

		private void Initialize()
		{
		    var handlers = _discoverer.FindMultiple<IHandleCommands>();
            handlers.ForEach(Register);
		    _initialized = true;
		}

		/// <summary>
		/// Register a command handler explicitly 
		/// </summary>
		/// <param name="handlerType"></param>
		/// <remarks>
		/// The registration process will look into the handler and find methods that 
		/// are called Handle() and takes a command as parameter
		/// </remarks>
		public void Register(Type handlerType)
		{
            var allMethods = handlerType.GetRuntimeMethods().Where(m => m.IsPublic || !m.IsStatic);
            var query = from m in allMethods
                        where m.Name.Equals(HandleMethodName) &&
                              m.GetParameters().Length == 1 &&
                              typeof(ICommand)
                                .GetTypeInfo().IsAssignableFrom(m.GetParameters()[0].ParameterType.GetTypeInfo())
                        select m;

            foreach (var method in query)
                _commandHandlers[method.GetParameters()[0].ParameterType] = method;
		}

#pragma warning disable 1591 // Xml Comments
		public bool TryHandle(ICommand command)
		{
            if( !_initialized)
                Initialize();

            var commandType = command.GetType();
            if (_commandHandlers.ContainsKey(commandType))
            {
                var commandHandlerType = _commandHandlers[commandType].DeclaringType;
                var commandHandler = _container.Get(commandHandlerType);
                var method = _commandHandlers[commandType];
                method.Invoke(commandHandler, new[] { command });
                return true;
            }

		    return false;
		}
#pragma warning restore 1591 // Xml Comments
	}
}