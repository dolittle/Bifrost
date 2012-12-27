using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandFactory"/> for creating commands with
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        ICommandCoordinator _commandCoordinator;
        ICommandBuildingConventions _conventions;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandFactory"/>
        /// </summary>
        /// <param name="commandCoordinator"><see cref="ICommandCoordinator"/> to use when handling commands</param>
        public CommandFactory(ICommandCoordinator commandCoordinator, ICommandBuildingConventions conventions)
        {
            _commandCoordinator = commandCoordinator;
            _conventions = conventions;
        }

#pragma warning disable 1591 // Xml Comments
        public ICommandBuilder<TC> BuildFrom<TC>(Expression<Func<TC>> property, ICommandBuildingConventions conventions = null) where TC:ICommand
        {
            if (conventions == null) conventions = _conventions;

            var propertyName = property.GetPropertyInfo().Name;
            var builder = BuildFromName<TC>(propertyName, conventions);
            return builder;
        }

        public ICommandBuilder<TC> BuildFor<TC>() where TC : ICommand, new()
        {
            var builder = new CommandBuilder<TC>(_commandCoordinator, _conventions);
            builder.WithName(typeof(TC).Name);
            return builder;
        }

        public void BuildAndPopulateAll<T>(T target, ICommandBuildingConventions conventions = null)
        {
            if (conventions == null) conventions = _conventions;

            foreach (var property in GetCommandProperties(typeof(T)))
            {
                if (property.GetValue(target, null) == null)
                {
                    var instance = BuildFromName<Command>(property.Name, conventions).GetInstance();
                    property.SetValue(target, instance, null);
                }
            }
            
        }
#pragma warning restore 1591 // Xml Comments

        CommandBuilder<TC> BuildFromName<TC>(string name, ICommandBuildingConventions conventions) where TC:ICommand
        {
            var builder = new CommandBuilder<TC>(_commandCoordinator, _conventions);
            builder.WithName(conventions.CommandName(name));
            return builder;
        }

        IEnumerable<PropertyInfo> GetCommandProperties(Type type)
        {
            return type.GetProperties().Where(p => p.PropertyType.HasInterface<ICommand>() || p.PropertyType == typeof(ICommand));
        }


    }
}
