using Bifrost.Commands;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using System.Linq;

namespace Bifrost.Specs.Commands.for_CommandTypeManager.given
{
	public class a_command_type_manager
	{
		protected static Mock<ITypeDiscoverer> type_discoverer_mock;
		protected static CommandTypeManager command_type_manager;
		
		Establish context = () => {
			type_discoverer_mock = new Mock<ITypeDiscoverer>();
			command_type_manager = new CommandTypeManager(type_discoverer_mock.Object);
		};
	}
}

