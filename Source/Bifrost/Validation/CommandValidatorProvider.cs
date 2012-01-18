#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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
using System.Linq;
using Bifrost.Commands;
using Bifrost.Execution;
using Microsoft.Practices.ServiceLocation;
using Bifrost.Configuration;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents an instance of an <see cref="ICommandValidatorProvider">ICommandValidatorProvider.</see>
    /// </summary>
    [Singleton]
    public class CommandValidatorProvider : ICommandValidatorProvider
    {
        static ICommandInputValidator NullInputValidator = new NullCommandInputValidator();
        static ICommandBusinessValidator NullBusinessValidator = new NullCommandBusinessValidator();
        static Type _inputValidatorType = typeof (ICommandInputValidator);
        static Type _businessValidatorType = typeof (ICommandBusinessValidator);
        static Type _validatesType = typeof (ICanValidate<>);

        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        IConfigure _configuration;

        Dictionary<Type, Type> _inputValidators;
        Dictionary<Type, Type> _businessValidators;

        /// <summary>
        /// Initializes an instance of <see cref="CommandValidatorProvider"/> CommandValidatorProvider
        /// </summary>
        /// <param name="typeDiscoverer">
        /// An instance of ITypeDiscoverer to help identify and register <see cref="ICommandInputValidator"/> implementations
        /// and  <see cref="ICommandBusinessValidator"/> implementations
        /// </param>
        /// <param name="container">An instance of <see cref="IContainer"/> to manage instances of any <see cref="ICommandInputValidator"/></param>
        /// <param name="configuration">An instance of <see cref="IConfigure"/> that holds the current configuration</param>
        public CommandValidatorProvider(ITypeDiscoverer typeDiscoverer, IContainer container, IConfigure configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _configuration = configuration;

            Initialize();
        }

#pragma warning disable 1591 // Xml Comments
        public ICommandInputValidator GetInputValidatorFor(ICommand command)
        {
            return GetInputValidatorFor(command.GetType());
        }

        public ICommandBusinessValidator GetBusinessValidatorFor(ICommand command)
        {
            return GetBusinessValidatorFor(command.GetType());
        }

        public ICommandInputValidator GetInputValidatorFor(Type type)
        {
            Type registeredType;
            _inputValidators.TryGetValue(type, out registeredType);

            var inputValidator = registeredType != null ? _container.Get(registeredType) as ICommandInputValidator : NullInputValidator;
            return inputValidator;
        }

        public ICommandBusinessValidator GetBusinessValidatorFor(Type type)
        {
            Type registeredType;
            _businessValidators.TryGetValue(type, out registeredType);

            var businessValidator = registeredType != null ? _container.Get(registeredType) as ICommandBusinessValidator : NullBusinessValidator;
            return businessValidator;
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
            get { return _businessValidators.Values; }
        }


        void Initialize()
        {
            _inputValidators = new Dictionary<Type, Type>();
            _businessValidators = new Dictionary<Type, Type>();

            var inputValidators = _typeDiscoverer.FindMultiple(_inputValidatorType);
            var businessValidators = _typeDiscoverer.FindMultiple(_businessValidatorType);

            Array.ForEach(inputValidators, type => Register(type, _inputValidatorType));
            Array.ForEach(businessValidators, type => Register(type, _businessValidatorType));
        }

        void Register(Type typeToRegister, Type registerFor)
        {
            var validatorRegistry = registerFor == _inputValidatorType
                                        ? _inputValidators
                                        : _businessValidators;

            var commandType = GetCommandType(typeToRegister);

            if (commandType == null || 
                commandType.IsInterface ||
                validatorRegistry.ContainsKey(commandType))
                return;

            validatorRegistry.Add(commandType, typeToRegister);
            _container.Bind(typeToRegister, typeToRegister, _configuration.DefaultObjectLifecycle);
        }

        Type GetCommandType(Type typeToRegister)
        {
            var types = from interfaceType in typeToRegister.GetInterfaces()
                        where interfaceType.IsGenericType
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == _validatesType
                        select interfaceType.GetGenericArguments().FirstOrDefault();

            return types.FirstOrDefault();
        }
    }
}