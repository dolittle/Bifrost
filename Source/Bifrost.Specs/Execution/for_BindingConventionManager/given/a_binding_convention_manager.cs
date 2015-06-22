using Bifrost.Execution;
using Bifrost.Testing;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_BindingConventionManager.given
{
    public class a_binding_convention_manager : dependency_injection
    {
        protected static BindingConventionManager manager;

        Establish context = () => manager = Get<BindingConventionManager>();
    }
}