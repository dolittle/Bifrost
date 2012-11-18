
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
    }
}
