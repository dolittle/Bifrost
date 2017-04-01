/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.FluentValidation.Commands
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

        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;

        Dictionary<Type, Type> _inputCommandValidators;
        Dictionary<Type, Type> _businessCommandValidators;
        Dictionary<Type, List<Type>> _dynamicallyDiscoveredInputValidators = new Dictionary<Type, List<Type>>();
        Dictionary<Type, List<Type>> _dynamicallyDiscoveredBusinessValidators = new Dictionary<Type, List<Type>>();

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
        public ICommandInputValidator GetInputValidatorFor(ICommand command)
        {
            return GetInputValidatorFor(command.GetType());
        }

        public ICommandBusinessValidator GetBusinessValidatorFor(ICommand command)
        {
            return GetBusinessValidatorFor(command.GetType());
        }

        public ICommandBusinessValidator GetBusinessValidatorFor(Type commandType)
        {
            if (!typeof (ICommand).GetTypeInfo().IsAssignableFrom(commandType))
                return null;

            Type registeredBusinessValidatorType;
            _businessCommandValidators.TryGetValue(commandType, out registeredBusinessValidatorType);

            if (registeredBusinessValidatorType != null)
                return _container.Get(registeredBusinessValidatorType) as ICommandBusinessValidator;

            var typesAndDiscoveredValidators = GetValidatorsFor(commandType, _dynamicallyDiscoveredBusinessValidators);

            return BuildDynamicallyDiscoveredBusinessValidator(commandType, typesAndDiscoveredValidators);
        }

        public ICommandInputValidator GetInputValidatorFor(Type commandType)
        {
            if (!typeof(ICommand).GetTypeInfo().IsAssignableFrom(commandType))
                return null;

            Type registeredInputValidatorType;
            _inputCommandValidators.TryGetValue(commandType, out registeredInputValidatorType);

            if (registeredInputValidatorType != null)
                return _container.Get(registeredInputValidatorType) as ICommandInputValidator;

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
            var commandPropertyTypes = commandType.GetTypeInfo().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                            .Where(p => !p.PropertyType.IsAPrimitiveType()).Select(p => p.PropertyType).Distinct();
            return commandPropertyTypes;
        }

        ICommandInputValidator BuildDynamicallyDiscoveredInputValidator(Type commandType, IDictionary<Type,IEnumerable<Type>> typeAndAssociatedValidatorTypes)
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
            return Activator.CreateInstance(closedValidatorType, propertyTypeAndValidatorInstances) as ICommandInputValidator;
        }

        ICommandBusinessValidator BuildDynamicallyDiscoveredBusinessValidator(Type commandType, IDictionary<Type, IEnumerable<Type>> typeAndAssociatedValidatorTypes)
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
            return Activator.CreateInstance(closedValidatorType, propertyTypeAndValidatorInstances) as ICommandBusinessValidator;
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

            commandInputValidators.ForEach(type => RegisterCommandValidator(type, _inputCommandValidators));
            commandBusinessValidators.ForEach(type => RegisterCommandValidator(type, _businessCommandValidators));
        }

        void InitializeDynamicValidators()
        {
            _dynamicallyDiscoveredBusinessValidators = new Dictionary<Type, List<Type>>();
            _dynamicallyDiscoveredInputValidators = new Dictionary<Type, List<Type>>();

            var inputValidators = _typeDiscoverer.FindMultiple(typeof(IValidateInput<>))
                .Where(t => t != typeof(InputValidator<>) && t != typeof(ComposedCommandInputValidator<>));
            var businessValidators = _typeDiscoverer.FindMultiple(typeof(IValidateBusinessRules<>))
                .Where(t => t != typeof(BusinessValidator<>) && t != typeof(ComposedCommandBusinessValidator<>));

            inputValidators.ForEach(type => RegisterValidator(type, _dynamicallyDiscoveredInputValidators));
            businessValidators.ForEach(type => RegisterValidator(type, _dynamicallyDiscoveredBusinessValidators));
        }

        void RegisterCommandValidator(Type typeToRegister, IDictionary<Type, Type> validatorRegistry)
        {
            var commandType = GetCommandType(typeToRegister);

            if (commandType == null || 
                commandType.GetTypeInfo().IsInterface ||
                validatorRegistry.ContainsKey(commandType))
                return;

            validatorRegistry.Add(commandType, typeToRegister);
        }

        void RegisterValidator(Type typeToRegister, IDictionary<Type, List<Type>> validatorRegistry)
        {
            var validatedType = GetValidatedType(typeToRegister);

            if (validatedType == null || validatedType.GetTypeInfo().IsInterface || validatedType.IsAPrimitiveType())
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
            var types = from interfaceType in typeToRegister.GetTypeInfo()
                                    .GetInterfaces()
                        where interfaceType.GetTypeInfo()
                                    .IsGenericType
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == _validatesType
                        select interfaceType.GetTypeInfo()
                                    .GetGenericArguments()
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
            return (from @interface in typeToQuery.GetTypeInfo().GetInterfaces() 
                    where @interface.GetTypeInfo().IsGenericType && @interface.GetTypeInfo().GetGenericTypeDefinition() == genericInterfaceType 
                    select @interface.GetTypeInfo().GetGenericArguments()[0]).FirstOrDefault();
        }
    }
}