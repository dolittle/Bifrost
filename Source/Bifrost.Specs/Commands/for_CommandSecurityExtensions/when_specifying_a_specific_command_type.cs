using System.Linq;
using Bifrost.Commands;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Commands;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandSecurityExtensions
{
    public class when_specifying_a_specific_command_type
    {
        static CommandSecurityTarget target;
        static CommandSecurityTargetBuilder builder;
        static TypeSecurableBuilder type_builder;

        Establish context = () =>
        {
            target = new CommandSecurityTarget();
            builder = new CommandSecurityTargetBuilder(target);
        };

        Because of = () => builder.InstanceOf<SimpleCommand>(t => type_builder = t);

        It should_add_a_type_securable = () => builder.Target.Securables.First().ShouldBeOfType<TypeSecurable>();
        It should_set_the_type_on_the_securable = () => ((TypeSecurable)builder.Target.Securables.First()).Type.ShouldEqual(typeof(SimpleCommand));
        It should_continue_the_fluent_interface_with_type_securable_builder = () => type_builder.ShouldNotBeNull();
    }
}
