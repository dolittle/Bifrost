using System;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandTypeManager
{
	public class when_creating_an_instance_of_command_type_manager_and_multiple_commands_with_same_name_exists : given.a_command_type_manager
	{
		static Exception exception;
		
		Establish context = () => type_discoverer_mock.Setup(t=>t.FindMultiple<ICommand>()).Returns(new[] {typeof(SomeCommand), typeof(SomeCommand)});
		
		Because of = () => exception = Catch.Exception(()=>command_type_manager = new CommandTypeManager(type_discoverer_mock.Object));

        // Todo : Look @ issue #141
        //It should_throw_ambiguous_command_exception = () => exception.ShouldBeOfType<AmbiguousCommandException>();
	}
}

