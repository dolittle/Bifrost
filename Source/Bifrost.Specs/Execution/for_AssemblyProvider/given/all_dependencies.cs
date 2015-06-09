using System;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_AssemblyProvider.given
{
    public class all_dependencies
    {
        protected static Mock<_AppDomain> app_domain_mock;
        protected static Mock<IAssemblyFilters> assembly_filters_mock;
        protected static Mock<IExecutionEnvironment> execution_environment_mock;
        protected static Mock<IAssemblyUtility> assembly_utility_mock;
        protected static Mock<IAssemblySpecifiers> assembly_specifiers_mock;

        Establish context = () =>
        {
            app_domain_mock = new Mock<_AppDomain>();
            assembly_filters_mock = new Mock<IAssemblyFilters>();
            execution_environment_mock = new Mock<IExecutionEnvironment>();
            assembly_utility_mock = new Mock<IAssemblyUtility>();
            assembly_specifiers_mock = new Mock<IAssemblySpecifiers>();
        };
    }
}
