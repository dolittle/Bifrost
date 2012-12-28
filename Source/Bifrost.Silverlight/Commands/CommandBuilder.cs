using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Dynamic;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandBuilder"/> for building commands with
    /// </summary>
    public class CommandBuilder<T> : ICommandBuilder<T> 
        where T:ICommand
    {
        ICommandCoordinator _commandCoordinator;
        ICommandBuildingConventions _conventions;
        Type _type;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandBuilder"/>
        /// </summary>
        /// <param name="commandCoordinator"><see cref="ICommandCoordinator"/> to use</param>
        public CommandBuilder(ICommandCoordinator commandCoordinator, ICommandBuildingConventions conventions)
        {
            _commandCoordinator = commandCoordinator;
            _conventions = conventions;

            if (typeof(T) != typeof(Command) && typeof(T) != typeof(ICommand))
                Name = conventions.CommandName(typeof(T).Name);
        }

#pragma warning disable 1591 // Xml Comments
        public T GetInstance()
        {
            ThrowIfNameIsMissing();

            if (Parameters != null)
                ThrowIfParametersAreAnonymousType();

            ThrowIfAmbiguousConstructors();
            ThrowIfNonDefaultConstructorAndConstructorParametersAreMissing();

            T command = CreateInstanceOfCommand();
            command.CommandCoordinator = _commandCoordinator;
            command.Name = Name;

            if (Parameters is IDictionary)
                PopulateParametersFromDictionary(command);

            if (Parameters is IDictionary<string, object>)
                PopulateParametersFromGenericDictionary(command);

            return command;
        }

        public string Name { get; set; }
        public dynamic Parameters { get; set; }
        public object[] ConstructorParameters { get; set; }
        public Type Type 
        { 
            get { return _type; }
            set
            {
                _type = value;
                if( string.IsNullOrEmpty(Name) )
                    Name = _conventions.CommandName(value.Name);
            }
        } 
#pragma warning restore 1591 // Xml Comments


        T CreateInstanceOfCommand()
        {
            T command;
            Type typeToCreate = null;
            if (Type != null)
                typeToCreate = Type;
            else
            {
                typeToCreate = typeof(T);
                if (typeToCreate == typeof(ICommand))
                    typeToCreate = typeof(Command);
            }

            ConstructorInfo constructor;
            if (typeToCreate.HasNonDefaultConstructor() &&
                !typeToCreate.HasDefaultConstructor())
                constructor = typeToCreate.GetNonDefaultConstructor();
            else
                constructor = typeToCreate.GetDefaultConstructor();

            command = (T)constructor.Invoke(ConstructorParameters);
            return command;
        }

        void PopulateParametersFromDictionary(ICommand command)
        {
            var dictionary = Parameters as IDictionary;
            var expandoObject = command.Parameters as BindableExpandoObject;
            if (expandoObject == null)
                return;

            foreach (string key in dictionary.Keys)
            {
                var value = dictionary[key];
                expandoObject[key] = value;
                SetPropertyValueFromParameterIfExist(command, key, value);
            }
        }

        void PopulateParametersFromGenericDictionary(ICommand command)
        {
            var dictionary = Parameters as IDictionary<string,object>;
            var expandoObject = command.Parameters as BindableExpandoObject;
            if (expandoObject == null)
                return;

            foreach (string key in dictionary.Keys)
            {
                var value = dictionary[key];
                expandoObject[key] = value;
                SetPropertyValueFromParameterIfExist(command, key, value);
            }
        }

        void SetPropertyValueFromParameterIfExist(ICommand command, string parameter, object value)
        {
            var property = command.GetType().GetProperty(parameter);
            if (property != null)
                property.SetValue(command, value, null);
        }


        void ThrowIfNameIsMissing()
        {
            if (string.IsNullOrEmpty(Name))
                throw new CommandNameMissingException();
        }

        void ThrowIfParametersAreAnonymousType()
        {
            var type = Parameters.GetType();
            if( type.Name.Contains("_Anonymous") )
                throw new UnsupportedParametersConstruct();
        }

        void ThrowIfAmbiguousConstructors()
        {
            if (typeof(T).GetConstructors().Where(c => c.GetParameters().Length > 0).Count() > 1)
                throw new AmbiguousConstructorsException(typeof(T));
        }

        void ThrowIfNonDefaultConstructorAndConstructorParametersAreMissing()
        {
            var type = typeof(T);
            if (type.HasDefaultConstructor() || type.IsInterface)
                return;

            var constructor = type.GetNonDefaultConstructor();
            if (ConstructorParameters == null || 
                constructor.GetParameters().Length != ConstructorParameters.Length)
                throw new CommandConstructorParameterMissing(typeof(T));
        }
    }
}
