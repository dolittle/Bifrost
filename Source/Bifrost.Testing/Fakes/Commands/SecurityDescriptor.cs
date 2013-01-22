using Bifrost.Commands;
using Bifrost.Security;

namespace Bifrost.Testing.Fakes.Commands
{
    public class SecurityDescriptor : Security.SecurityDescriptor
    {
        public const string NAMESPACE_ROLE = "CanExecuteCommandsInNamespace";
        public const string SIMPLE_COMMAND_ROLE = "CanExecuteSimpleCommands";

        public SecurityDescriptor()
        {
            When.Handling().Commands()
                .InNamespace("Bifrost.Testing.Fakes.Commands", n => n.User().MustBeInRole(NAMESPACE_ROLE));
            When.Handling().Commands()
                .InstanceOf<SimpleCommand>(c => c.User().MustBeInRole(SIMPLE_COMMAND_ROLE));
        }
    }
}