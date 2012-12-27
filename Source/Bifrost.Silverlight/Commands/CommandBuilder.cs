using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Dynamic;

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
            ThrowIfNonDefaultConstructorAndParametersAreMissing();

            T command;

            var typeToCreate = typeof(T);
            if (typeToCreate == typeof(ICommand))
            {
                typeToCreate = typeof(Command);
            }

            command = (T)Activator.CreateInstance(typeToCreate);
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
        public Type Type { get; set; } 
#pragma warning restore 1591 // Xml Comments

        bool HasDefaultConstructor()
        {
            return typeof(T).GetConstructors().Any(c => c.GetParameters().Length == 0);
        }


        ConstructorInfo GetNonDefaultConstructor()
        {
            return typeof(T).GetConstructors().Where(c => c.GetParameters().Length > 0).Single();
        }


        void PopulateParametersFromDictionary(ICommand command)
        {
            var dictionary = Parameters as IDictionary;
            var expandoObject = command.Parameters as BindableExpandoObject;
            if (expandoObject == null)
                return;

            foreach (string key in dictionary.Keys)
                expandoObject[key] = dictionary[key];
        }


        void PopulateParametersFromGenericDictionary(ICommand command)
        {
            var dictionary = Parameters as IDictionary<string,object>;
            var expandoObject = command.Parameters as BindableExpandoObject;
            if (expandoObject == null)
                return;

            foreach (string key in dictionary.Keys)
                expandoObject[key] = dictionary[key];
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

        void ThrowIfNonDefaultConstructorAndParametersAreMissing()
        {
            if (HasDefaultConstructor())
                return;

            if (Parameters == null)
                throw new CommandConstructorParameterMissing(typeof(T));

            var constructor = GetNonDefaultConstructor();
            var constructorParameters = constructor.GetParameters();
            var queryableParameters = ((IEnumerable)Parameters).OfType<string>();

            foreach( var parameter in constructorParameters )
                if( !queryableParameters.Any(p=>_conventions.CommandConstructorName(p) == parameter.Name) )
                    throw new CommandConstructorParameterMissing(typeof(T));
        }
    }
}
