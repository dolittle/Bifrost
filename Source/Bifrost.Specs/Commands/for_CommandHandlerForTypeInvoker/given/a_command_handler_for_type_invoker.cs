using Bifrost.Commands;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandHandlerForTypeInvoker.given
{
	public class a_command_handler_for_type_invoker
	{
		protected static Mock<IContainer> container_mock;
		protected static Mock<ITypeDiscoverer> type_discoverer_mock;
		protected static CommandHandlerForTypeInvoker invoker;

		Establish context = () =>
		                    	{
									container_mock = new Mock<IContainer>();
									type_discoverer_mock = new Mock<ITypeDiscoverer>();
									invoker = new CommandHandlerForTypeInvoker(container_mock.Object, type_discoverer_mock.Object);
		                    	};
	}
}
