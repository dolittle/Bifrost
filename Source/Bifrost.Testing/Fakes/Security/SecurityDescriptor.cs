using Bifrost.Commands;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Commands;

namespace Bifrost.Testing.Fakes.Security
{
    public class SecurityDescriptor : Bifrost.Security.BaseSecurityDescriptor
    {
        public const string SECURED_NAMESPACE = "Bifrost.Testing.Fakes.Commands";
        public const string NAMESPACE_ROLE = "CanExecuteCommandsInNamespace";
        public const string SIMPLE_COMMAND_ROLE = "CanExecuteSimpleCommands";

        public SecurityDescriptor()
        {
            When.Handling().Commands()
                .InNamespace(SECURED_NAMESPACE, n => n.User().MustBeInRole(NAMESPACE_ROLE));
            When.Handling().Commands()
                .InstanceOf<SimpleCommand>(c => c.User().MustBeInRole(SIMPLE_COMMAND_ROLE));
        }
    }
}