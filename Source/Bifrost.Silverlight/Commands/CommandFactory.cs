using System;
using System.Linq.Expressions;
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
        public ICommandBuilder BuildFrom(Expression<Func<object>> property)
        {
            var builder = new CommandBuilder(_commandCoordinator);
            var propertyName = property.GetPropertyInfo().Name;
            builder.WithName(_conventions.CommandName(propertyName));
            return builder;
        }
#pragma warning restore 1591 // Xml Comments

    }
}
