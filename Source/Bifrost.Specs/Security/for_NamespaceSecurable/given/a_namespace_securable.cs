using Bifrost.Security;
using Bifrost.SomeRandomNamespace;
using Bifrost.Testing;
using Bifrost.Testing.Fakes.Commands;

namespace Bifrost.Specs.Security.for_NamespaceSecurable.given
{
    public class a_namespace_securable
    {
        protected static NamespaceSecurable namespace_securable;
        protected static object action_with_exact_namespace_match;
        protected static object action_with_partial_namespace_match;
        protected static object action_within_another_namespace;

        public a_namespace_securable()
        {
            action_with_exact_namespace_match = new CommandScenario<SimpleCommand>();
            action_with_partial_namespace_match = new SimpleCommand();
            action_within_another_namespace = new CommandInADifferentNamespace();

            namespace_securable = new NamespaceSecurable(typeof(CommandScenario<>).Namespace);
        }
    }
}