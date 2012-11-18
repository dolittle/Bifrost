using System;
using System.Linq.Expressions;

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines a factory for creating <see cref="ICommand">commands</see>
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Build a command from a given property
        /// </summary>
        /// <param name="property"><see cref="Expression"/> representing the property that will represent the command</param>
        /// <returns><see cref="ICommandBuilder"/> to use for building the command</returns>
        ICommandBuilder BuildFrom(Expression<Func<object>> property);
    }
}
