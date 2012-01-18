using System;

namespace Bifrost.Commands
{
	/// <summary>
	/// The exception that is thrown when a command is not handled by any <see cref="ICommandHandler"/>
	/// </summary>
    public class UnhandledCommandException : ArgumentException
    {
		/// <summary>
		/// Initializes a new instance <see cref="UnhandledCommandException"/>
		/// </summary>
		/// <param name="command"><see cref="ICommand"/> that wasn't handled</param>
        public UnhandledCommandException(ICommand command)
        {
            Command = command;
        }

		/// <summary>
		/// Gets the <see cref="ICommand"/> that wasn't handled
		/// </summary>
        public ICommand Command { get; private set; }
    }
}
