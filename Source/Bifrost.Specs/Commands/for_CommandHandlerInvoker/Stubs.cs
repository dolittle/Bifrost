using System;
using Bifrost.Commands;

namespace Bifrost.Specs.Commands.for_CommandHandlerInvoker
{
	public class Command : ICommand
	{
        public Guid Id { get; set; }
	}

	public class CommandHandler : ICommandHandler
	{
		public bool HandleCalled = false;


		public void Handle(Command command)
		{
			HandleCalled = true;
		}
	}
}
