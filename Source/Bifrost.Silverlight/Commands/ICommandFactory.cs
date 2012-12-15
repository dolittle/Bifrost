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
        /// <param name="conventions">Optional conventions for how it will build <see cref="ICommand">commands</see>, if not specified, it will use the default settings</param>
        /// <returns><see cref="ICommandBuilder"/> to use for building the command</returns>
        ICommandBuilder BuildFrom<TC>(Expression<Func<TC>> property, ICommandBuildingConventions conventions = null) where TC : ICommand;


        /// <summary>
        /// Build a command for a known command
        /// </summary>
        /// <typeparam name="TC"><see cref="ICommand"/> to build for</typeparam>
        /// <returns><see cref="ICommandBuilder"/> to use for building the command</returns>
        ICommandBuilder BuildFor<TC>() where TC : ICommand, new();

        /// <summary>
        /// Build all <see cref="ICommand">commands</see> based on conventions and populate the properties with the instance
        /// of the command
        /// </summary>
        /// <typeparam name="T">Type of the target</typeparam>
        /// <param name="target">Target to build from and populate to</param>
        /// <param name="conventions">Optional conventions for how it will build <see cref="ICommand">commands</see>, if not specified, it will use the default settings</param>
        void BuildAndPopulateAll<T>(T target, ICommandBuildingConventions conventions = null);
    }
}
