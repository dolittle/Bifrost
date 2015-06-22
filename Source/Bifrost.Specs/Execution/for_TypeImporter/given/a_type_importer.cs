using Bifrost.Execution;
using Bifrost.Testing;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeImporter.given
{
    public class a_type_importer : dependency_injection
    {
        protected static TypeImporter type_importer;

        Establish context = () =>
        {
            type_importer = Get<TypeImporter>();
        };
    }
}