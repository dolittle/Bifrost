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
using FluentValidation;

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
        static Type _commandInputValidatorType = typeof (ICommandInputValidator);
        static Type _commandBusinessValidatorType = typeof (ICommandBusinessValidator);
        static Type _validatesType = typeof (ICanValidate<>);
        static Type _inputValidatorType = typeof (InputValidator<>);
        static Type _businessValidatorType = typeof (BusinessValidator<>);

        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;

        Dictionary<Type, Type> _inputCommandValidators;
        Dictionary<Type, Type> _businessCommandValidators;
        Dictionary<Type, List<Type>> _dynamicallyDiscoveredInputValidators = new Dictionary<Type, List<Type>>();
        Dictionary<Type, List<Type>> _dynamicallyDiscoveredBusinessValidators = new Dictionary<Type, List<Type>>();

        NullCommandBusinessValidator _nullCommandBusinessValidator = new NullCommandBusinessValidator();
        NullCommandInputValidator _nullCommandInputValidator = new NullCommandInputValidator();

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

            InitializeCommandValidators();
            InitializeDynamicValidators();
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

        public ICanValidate GetBusinessValidatorFor(Type commandType)
        {
            if (!typeof (ICommand).IsAssignableFrom(commandType))
                return _nullCommandBusinessValidator;

            Type registeredBusinessValidatorType;
            _businessCommandValidators.TryGetValue(commandType, out registeredBusinessValidatorType);

            if (registeredBusinessValidatorType != null)
                return _container.Get(registeredBusinessValidatorType) as ICanValidate;

            var typesAndDiscoveredValidators = GetValidatorsFor(commandType, _dynamicallyDiscoveredBusinessValidators);

            return BuildDynamicallyDiscoveredBusinessValidator(commandType, typesAndDiscoveredValidators);
        }

        public ICanValidate GetInputValidatorFor(Type commandType)
        {
            if (!typeof(ICommand).IsAssignableFrom(commandType))
                return _nullCommandInputValidator;

            Type registeredInputValidatorType;
            _inputCommandValidators.TryGetValue(commandType, out registeredInputValidatorType);

            if (registeredInputValidatorType != null)
                return _container.Get(registeredInputValidatorType) as ICanValidate;

            var typesAndDiscoveredValidators = GetValidatorsFor(commandType, _dynamicallyDiscoveredInputValidators);

            return BuildDynamicallyDiscoveredInputValidator(commandType, typesAndDiscoveredValidators);
        }

        Dictionary<Type,IEnumerable<Type>> GetValidatorsFor(Type commandType, Dictionary<Type, List<Type>> registeredTypes)
        {
            var typesOnCommand = GetTypesFromCommand(commandType).ToList();
            var validatorTypes = new Dictionary<Type, IEnumerable<Type>>();
            foreach (var typeToBeValidated in typesOnCommand)
            {
                if (registeredTypes.ContainsKey(typeToBeValidated))
                    validatorTypes.Add(typeToBeValidated, registeredTypes[typeToBeValidated]);
            }
            return validatorTypes;
        }

        IEnumerable<Type> GetTypesFromCommand(Type commandType)
        {
            var commandPropertyTypes = commandType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                            .Where(p => !p.PropertyType.IsAPrimitiveType()).Select(p => p.PropertyType);
            return commandPropertyTypes;
        }

        ICanValidate BuildDynamicallyDiscoveredInputValidator(Type commandType, IDictionary<Type,IEnumerable<Type>> typeAndAssociatedValidatorTypes)
        {
            Type[] typeArgs = { commandType };
            var closedValidatorType = typeof(ComposedCommandInputValidator<>).MakeGenericType(typeArgs);

            var propertyTypeAndValidatorInstances = new Dictionary<Type, IEnumerable<IValidator>>();
            foreach (var key in typeAndAssociatedValidatorTypes.Keys)
            {
                var validatorTypes = typeAndAssociatedValidatorTypes[key];
                if (validatorTypes.Any())
                    propertyTypeAndValidatorInstances.Add(key, validatorTypes.Select(v => _container.Get(v) as IValidator).ToArray());
                    
            }
            return Activator.CreateInstance(closedValidatorType, propertyTypeAndValidatorInstances) as ICanValidate;
        }

        ICanValidate BuildDynamicallyDiscoveredBusinessValidator(Type commandType, IDictionary<Type, IEnumerable<Type>> typeAndAssociatedValidatorTypes)
        {
            Type[] typeArgs = { commandType };
            var closedValidatorType = typeof(ComposedCommandBusinessValidator<>).MakeGenericType(typeArgs);

            var propertyTypeAndValidatorInstances = new Dictionary<Type, IEnumerable<IValidator>>();
            foreach (var key in typeAndAssociatedValidatorTypes.Keys)
            {
                var validatorTypes = typeAndAssociatedValidatorTypes[key];
                if (validatorTypes.Any())
                    propertyTypeAndValidatorInstances.Add(key, validatorTypes.Select(v => _container.Get(v) as IValidator).ToArray());

            }
            return Activator.CreateInstance(closedValidatorType, propertyTypeAndValidatorInstances) as ICanValidate;
        }
#pragma warning restore 1591 // Xml Comments

        /// <summary>
        /// Gets a list of registered input validator types
        /// </summary>
        public IEnumerable<Type> RegisteredInputCommandValidators
        {
            get { return _inputCommandValidators.Values; }
        }

        /// <summary>
        ///  Gets a list of registered business validator types
        /// </summary>
        public IEnumerable<Type> RegisteredBusinessCommandValidators
        {
            get { return _businessCommandValidators.Values; }
        }

        void InitializeCommandValidators()
        {
            _inputCommandValidators = new Dictionary<Type, Type>();
            _businessCommandValidators = new Dictionary<Type, Type>();

            var commandInputValidators = _typeDiscoverer.FindMultiple(_commandInputValidatorType);
            var commandBusinessValidators = _typeDiscoverer.FindMultiple(_commandBusinessValidatorType);

            commandInputValidators.ForEach(type => RegisterCommandValidator(type, _commandInputValidatorType));
            commandBusinessValidators.ForEach(type => RegisterCommandValidator(type, _commandBusinessValidatorType));
        }

        void InitializeDynamicValidators()
        {
            _dynamicallyDiscoveredBusinessValidators = new Dictionary<Type, List<Type>>();
            _dynamicallyDiscoveredInputValidators = new Dictionary<Type, List<Type>>();

            var inputValidators = _typeDiscoverer.FindMultiple(typeof(IValidateInput<>))
                .Where(t => t != typeof(InputValidator<>) && t != typeof(ComposedCommandInputValidator<>));
            var businessValidators = _typeDiscoverer.FindMultiple(typeof(IValidateBusinessRules<>))
                .Where(t => t != typeof(BusinessValidator<>) && t != typeof(ComposedCommandInputValidator<>));

            inputValidators.ForEach(type => RegisterValidator(type, _inputValidatorType));
            businessValidators.ForEach(type => RegisterValidator(type, _businessValidatorType));
        }

        void RegisterCommandValidator(Type typeToRegister, Type registerFor)
        {
            var validatorRegistry = registerFor == _commandInputValidatorType
                                        ? _inputCommandValidators
                                        : _businessCommandValidators;

            var commandType = GetCommandType(typeToRegister);

            if (commandType == null || 
#if(NETFX_CORE)
                commandType.GetTypeInfo().IsInterface ||
#else
                commandType.IsInterface ||
#endif
                validatorRegistry.ContainsKey(commandType))
                return;

            validatorRegistry.Add(commandType, typeToRegister);
        }

        void RegisterValidator(Type typeToRegister, Type registerFor)
        {
            var validatorRegistry = registerFor == _inputValidatorType
                                        ? _dynamicallyDiscoveredInputValidators
                                        : _dynamicallyDiscoveredBusinessValidators;

            var validatedType = GetValidatedType(typeToRegister);

            if (validatedType == null || validatedType.IsInterface || validatedType.IsAPrimitiveType())
                return;

            if (validatorRegistry.ContainsKey(validatedType))
            {
               validatorRegistry[validatedType].Add(typeToRegister);
            }
            else
            {
                validatorRegistry.Add(validatedType, new List<Type>() { typeToRegister });
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

        Type GetValidatedType(Type typeToRegister)
        {
            Type validatedType = null;
            validatedType = GetGenericParameterType(typeToRegister, typeof (IValidateInput<>));
            return validatedType ?? GetGenericParameterType(typeToRegister, typeof (IValidateBusinessRules<>));
        }

        Type GetGenericParameterType(Type typeToQuery, Type genericInterfaceType)
        {
            return (from @interface in typeToQuery.GetInterfaces() 
                    where @interface.IsGenericType && @interface.GetGenericTypeDefinition() == genericInterfaceType 
                    select @interface.GetGenericArguments()[0]).FirstOrDefault();
        }
    }
}