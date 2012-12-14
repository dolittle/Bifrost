using System;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandTypeManager.given
{
	public class a_command_type_manager_with_one_command : a_command_type_manager
	{
		protected static Type expected_type = typeof(SomeCommand);
		
		Establish context = () => {
			type_discoverer_mock.Setup(t=>t.FindMultiple<ICommand>()).Returns(new[] { expected_type });
			command_type_manager = new CommandTypeManager(type_discoverer_mock.Object);
		};
	}
}

