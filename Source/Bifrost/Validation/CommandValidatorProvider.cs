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
using System.Linq;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;
#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents an instance of an <see cref="ICommandValidatorProvider">ICommandValidatorProvider.</see>
    /// </summary>
    [Singleton]
    public class CommandValidatorProvider : ICommandValidatorProvider
    {
        static readonly ICommandInputValidator NullInputValidator = new NullCommandInputValidator();
        static readonly ICommandBusinessValidator NullBusinessValidator = new NullCommandBusinessValidator();
        static readonly Type _inputValidatorType = typeof (ICommandInputValidator);
        static readonly Type _businessValidatorType = typeof (ICommandBusinessValidator);
        static readonly Type _validatesType = typeof (ICanValidate<>);

        readonly ITypeDiscoverer _typeDiscoverer;
        readonly IContainer _container;

        Dictionary<Type, Type> _inputValidators;
        Dictionary<Type, Type[]> _businessValidators;

        /// <summary>
        /// Initializes an instance of <see cref="CommandValidatorProvider"/> CommandValidatorProvider
        /// </summary>
        /// <param name="typeDiscoverer">
        /// An instance of ITypeDiscoverer to help identify and register <see cref="ICommandInputValidator"/> implementations
        /// and  <see cref="ICommandBusinessValidator"/> implementations
        /// </param>
        /// <param name="container">An instance of <see cref="IContainer"/> to manage instances of any <see cref="ICommandInputValidator"/></param>
        public CommandValidatorProvider(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;

            Initialize();
        }

#pragma warning disable 1591 // Xml Comments
        public ICanValidate GetInputValidatorFor(ICommand command)
        {
            return GetInputValidatorFor(command.GetType());
        }

        public ICanValidate GetBusinessValidatorFor(ICommand command)
        {
            return GetBusinessValidatorFor(command.GetType());
        }

        public ICanValidate GetInputValidatorFor(Type type)
        {
            Type registeredType;
            _inputValidators.TryGetValue(type, out registeredType);
            
            return registeredType != null ? _container.Get(registeredType) as ICanValidate : NullInputValidator as ICanValidate;
        }

        public ICanValidate GetBusinessValidatorFor(Type type)
        {
            Type[] registeredTypes;
            _businessValidators.TryGetValue(type, out registeredTypes);

            if (registeredTypes != null && registeredTypes.Length > 0)
            {
                var validators = registeredTypes.Select(t => _container.Get(t) as ICanValidate);
                return new AggregatedValidator(validators);
            }

            return NullBusinessValidator as ICanValidate;
        }
#pragma warning restore 1591 // Xml Comments

        /// <summary>
        /// Gets a list of registered input validator types
        /// </summary>
        public IEnumerable<Type> RegisteredInputValidators
        {
            get { return _inputValidators.Values; }
        }

        /// <summary>
        ///  Gets a list of registered business validator types
        /// </summary>
        public IEnumerable<Type> RegisteredBusinessValidators
        {
            get { return _businessValidators.Values.SelectMany(r => r.Select(t => t)); }
        }


        void Initialize()
        {
            _inputValidators = new Dictionary<Type, Type>();
            _businessValidators = new Dictionary<Type, Type[]>();

            var commandContractType = typeof (ICommand);
            var inputValidators = _typeDiscoverer.FindMultiple(_inputValidatorType);
            var businessValidators = _typeDiscoverer.FindMultiple(_businessValidatorType);
            var commandTypes = _typeDiscoverer.FindMultiple(commandContractType).Where(t => t.IsInterface == false && t.IsAbstract == false);

            foreach (var commandType in commandTypes)
            {
                var commandInterfaces = commandType.GetInterfaces().Where(i => commandContractType.IsAssignableFrom(i) && i != typeof(ICommand));
                
                var commandInputValidator = inputValidators.SingleOrDefault(t => GetCommandType(t) == commandType);
                
                if (commandInputValidator != null)
                {
                    _inputValidators.Add(commandType, commandInputValidator);
                }
                
                var commandBusinessValidators = businessValidators.Where(t => GetCommandType(t) == commandType || commandInterfaces.Contains(GetCommandType(t)));
                _businessValidators.Add(commandType, commandBusinessValidators.ToArray());
            }
        }

        Type GetCommandType(Type typeToRegister)
        {
            var types = from interfaceType in typeToRegister
#if(NETFX_CORE)
                                    .GetTypeInfo().ImplementedInterfaces
#else
                                    .GetInterfaces()
#endif
                        where interfaceType
#if(NETFX_CORE)
                                    .GetTypeInfo().IsGenericType
#else
                                    .IsGenericType
#endif
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == _validatesType
                        select interfaceType
#if(NETFX_CORE)
                                    .GetTypeInfo().GenericTypeParameters
#else
                                    .GetGenericArguments()
#endif
                            .FirstOrDefault();

            return types.FirstOrDefault();
        }
    }
}