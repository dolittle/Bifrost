using Bifrost.Commands;
using Machine.Specifications;
using It = Moq.It;

namespace Bifrost.Specs.Commands.for_CommandHandlerInvoker.given
{
	public class a_command_handler_invoker_with_one_command_handler : a_command_handler_invoker_with_no_command_handlers
	{
	    protected static CommandHandler handler;

		Establish context = () =>
		                    	{
		                    		handler = new CommandHandler();
		                    		type_discoverer_mock.Setup(t => t.FindMultiple<ICommandHandler>()).Returns(new[]
 		                    		                                                                     	{typeof(CommandHandler)});

		                    	    service_locator_mock.Setup(s => s.GetInstance(typeof (CommandHandler))).Returns(handler);
		                    	};
	}
}