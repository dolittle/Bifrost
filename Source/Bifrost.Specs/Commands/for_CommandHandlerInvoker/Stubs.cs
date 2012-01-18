using System;
using Bifrost.Commands;

namespace Bifrost.Specs.Commands.for_CommandHandlerInvoker
{
	public class Command : ICommand
	{
		public Guid Id
		{
			get { throw new NotImplementedException(); }
		}
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
