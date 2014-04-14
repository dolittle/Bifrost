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
#if(NETFX_CORE)
            var allMethods = handlerType.GetRuntimeMethods().Where(m => m.IsPublic || !m.IsStatic);
#else
            var allMethods = handlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
#endif

            var query = from m in allMethods
                        where m.Name.Equals(HandleMethodName) &&
                              m.GetParameters().Length == 1 &&
                              typeof(ICommand)
#if(NETFX_CORE)
                                .GetTypeInfo().IsAssignableFrom(m.GetParameters()[0].ParameterType.GetTypeInfo())
#else
                                .IsAssignableFrom(m.GetParameters()[0].ParameterType)
#endif
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