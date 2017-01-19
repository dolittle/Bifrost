using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeImporter
{
    public class when_importing_multiple_types_with_only_one_implementation : given.a_type_importer
    {
        static IEnumerable<ISingleInterface> instances;

        Establish context = () =>
        {
            type_discoverer_mock.Setup(t => t.FindMultiple<ISingleInterface>()).Returns(new[]
                                                                                            {
                                                                                                typeof(SingleClass)
                                                                                            });
            container_mock.Setup(c => c.Get(typeof(SingleClass))).Returns(new SingleClass());
        };

        Because of = () => instances = type_importer.ImportMany<ISingleInterface>();

        It should_return_one_instances = () => instances.Count().ShouldEqual(1);
        It should_contain_the_single_implementation = () => instances.First().ShouldBeOfExactType<SingleClass>();
    }
}
