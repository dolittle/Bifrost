using System;
using Bifrost.Commands;
using Bifrost.Execution;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandHandlerInvoker.given
{
	public class a_command_handler_invoker_with_no_command_handlers
	{
		protected static CommandHandlerInvoker invoker;
		protected static Mock<ITypeDiscoverer> type_discoverer_mock;
	    protected static Mock<IServiceLocator> service_locator_mock;

		Establish context = () =>
		                    	{
									type_discoverer_mock = new Mock<ITypeDiscoverer>();
									type_discoverer_mock.Setup(t => t.FindMultiple<ICommandHandler>()).Returns(new Type[0]);
		                    	    service_locator_mock = new Mock<IServiceLocator>();
									invoker = new CommandHandlerInvoker(type_discoverer_mock.Object, service_locator_mock.Object); 
		                    	};
	}
}
