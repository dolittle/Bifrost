using System;
using System.Linq.Expressions;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
    public static class CommandCoordinatorExtensions
    {
        public static Command Create(this ICommandCoordinator commandCoordinator, string name, object initialParameterValues = null)
        {
            return Command.Create(commandCoordinator, name, initialParameterValues);
        }

        public static Command CreateFrom(this ICommandCoordinator commandCoordinator, Expression<Func<object>> expression, dynamic initialParameterValues = null)
        {
            var name = expression.GetPropertyInfo().Name;
            var command = Command.Create(commandCoordinator, name, initialParameterValues);
            return command;
        }
    }
}
