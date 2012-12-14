using System;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandTypeManager
{
	public class when_asking_for_a_command_that_does_not_exist : given.a_command_type_manager
	{
		static Exception thrown_exception;
		
		Because of = () => thrown_exception = Catch.Exception (() => command_type_manager.GetFromName("BlabbediBlahSomethingThatDoesntExist"));
		
		It should_throw_unknown_command_exception = () => thrown_exception.ShouldBeOfType<UnknownCommandException>();
	}
}

