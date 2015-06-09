using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_AssemblyProvider.given
{
    public class an_unitialized_assembly_provider : all_dependencies
    {
        protected static AssemblyProvider provider;

        //Establish context = () => execution_environment_mock.SetupGet(e => e.CodeBase).Returns(code_base);

        protected static void initialize()
        {
            provider = new AssemblyProvider(
                app_domain_mock.Object,
                assembly_filters_mock.Object,
                execution_environment_mock.Object,
                assembly_utility_mock.Object,
                assembly_specifiers_mock.Object
            );
        }

    }
}
