namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandBuilder"/> for building commands with
    /// </summary>
    public class CommandBuilder : ICommandBuilder
    {
        ICommandCoordinator _commandCoordinator;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandBuilder"/>
        /// </summary>
        /// <param name="commandCoordinator"><see cref="ICommandCoordinator"/> to use</param>
        public CommandBuilder(ICommandCoordinator commandCoordinator)
        {
            _commandCoordinator = commandCoordinator;
        }

#pragma warning disable 1591 // Xml Comments
        public ICommand GetInstance()
        {
            ThrowIfNameIsMissing();
            var command = new Command(_commandCoordinator);
            command.Name = Name;
            return command;
        }

        public string Name { get; set; }
        public dynamic Parameters { get; set; }
#pragma warning restore 1591 // Xml Comments

        void ThrowIfNameIsMissing()
        {
            if (string.IsNullOrEmpty(Name))
                throw new CommandNameMissingException();
        }
    }
}
