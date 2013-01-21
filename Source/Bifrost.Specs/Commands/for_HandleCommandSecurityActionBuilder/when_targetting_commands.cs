using System.Linq;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_HandleCommandSecurityActionBuilder
{
    public class when_targetting_commands
    {
        static HandleCommand action;
        static HandleCommandSecurityActionBuilder action_builder;
        static CommandSecurityTargetBuilder target_builder;

        Establish context = () => 
        {
            action = new HandleCommand();
            action_builder = new HandleCommandSecurityActionBuilder(action);
        };

        Because of = () => target_builder = action_builder.Commands();

        It should_return_a_command_security_target_builder = () => target_builder.ShouldNotBeNull();
        It should_add_a_command_security_target = () => action.Targets.First().ShouldBeOfType<CommandSecurityTarget>();
    }
}
