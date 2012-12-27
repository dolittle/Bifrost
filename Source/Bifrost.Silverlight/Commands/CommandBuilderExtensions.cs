
using System;
using System.Dynamic;
namespace Bifrost.Commands
{
    /// <summary>
    /// Provides methods for building commands, extensions not available already on <see cref="ICommandBuilder"/>
    /// </summary>
    public static class CommandBuilderExtensions
    {
        /// <summary>
        /// Gives a command being built a name
        /// </summary>
        /// <param name="commandBuilder"><see cref="ICommandBuilder"/> to build on</param>
        /// <param name="name">Name of command</param>
        /// <returns>Chainable <see cref="ICommandBuilder"/></returns>
        public static ICommandBuilder WithName(this ICommandBuilder commandBuilder, string name)
        {
            commandBuilder.Name = name;
            return commandBuilder;
        }

        /// <summary>
        /// Indicates that the command builder should build with a specific type in mind
        /// </summary>
        /// <typeparam name="TC">Type of <see cref="ICommand"/> to build</typeparam>
        /// <param name="commandBuilder"><see cref="ICommandBuilder"/> to build on</param>
        /// <returns>Chainable <see cref="ICommandBuilder"/></returns>
        public static ICommandBuilder WithType<TC>(this ICommandBuilder commandBuilder) where TC : ICommand, new()
        {
            return commandBuilder;
        }

        /// <summary>
        /// Gives default parameters to a command being built
        /// </summary>
        /// <param name="commandBuilder"><see cref="ICommandBuilder"/> to build on</param>
        /// <param name="parameters">Default parameters to use</param>
        /// <returns>Chainable <see cref="ICommandBuilder"/></returns>
        public static ICommandBuilder WithParameters(this ICommandBuilder commandBuilder, dynamic parameters)
        {
            commandBuilder.Parameters = parameters;
            return commandBuilder;
        }


        /// <summary>
        /// Populate the default parameters to a command being build
        /// </summary>
        /// <param name="commandBuilder"><see cref="ICommandBuilder"/> to build on</param>
        /// <param name="populateParameters"><see cref="Action"/> that gets called for populating the parameters</param>
        /// <returns>Chainable <see cref="ICommandBuilder"/></returns>
        public static ICommandBuilder WithParameters(this ICommandBuilder commandBuilder, Action<dynamic> populateParameters)
        {
            commandBuilder.Parameters = new ExpandoObject();
            populateParameters(commandBuilder.Parameters);
            return commandBuilder;
        }

    }
}
