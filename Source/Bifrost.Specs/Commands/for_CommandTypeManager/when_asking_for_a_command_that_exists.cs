using System;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandTypeManager
{
	public class when_asking_for_a_command_that_exists : given.a_command_type_manager_with_one_command
	{
		static Type command_type;
		
		Because of = () => command_type = command_type_manager.GetFromName(expected_type.Name);
		
		It should_return_the_type = () => command_type.ShouldEqual(expected_type);
	}
}

