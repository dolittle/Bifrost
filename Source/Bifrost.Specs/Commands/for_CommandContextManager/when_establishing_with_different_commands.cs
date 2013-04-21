using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandContextManager
{
	[Subject(Subjects.establishing_context)]
	public class when_establishing_with_different_commands : given.a_command_context_manager
	{
		static ICommandContext firstCommandContext;
		static ICommandContext secondCommandContext;

		Because of = () =>
		             	{
		             		firstCommandContext = Manager.EstablishForCommand(new SimpleCommand());
		             		secondCommandContext = Manager.EstablishForCommand(new SimpleCommand());
		             	};
 

		It should_return_different_contexts = () => firstCommandContext.ShouldNotEqual(secondCommandContext);
	}
}