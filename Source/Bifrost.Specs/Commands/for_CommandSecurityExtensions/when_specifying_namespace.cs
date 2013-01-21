using System.Linq;
using Machine.Specifications;
using Bifrost.Commands;
using Bifrost.Security;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandSecurityExtensions
{
    public class when_specifying_namespace
    {
        static CommandSecurityTarget target;
        static CommandSecurityTargetBuilder builder;
        static NamespaceSecurableBuilder namespace_builder;

        Establish context = () =>
        {
            target = new CommandSecurityTarget();
            builder = new CommandSecurityTargetBuilder(target);
        };

        Because of = () => builder.InNamespace("MyNamespace", n => namespace_builder = n);

        It should_add_a_namespace_securable = () => builder.Target.Securables.First().ShouldBeOfType<NamespaceSecurable>();
        It should_set_the_name_of_the_namespace_on_the_securable = () => ((NamespaceSecurable)builder.Target.Securables.First()).Namespace.ShouldEqual("MyNamespace");
        It should_continue_the_fluent_interface_with_namespace_securable_builder = () => namespace_builder.ShouldNotBeNull();
    }
}
