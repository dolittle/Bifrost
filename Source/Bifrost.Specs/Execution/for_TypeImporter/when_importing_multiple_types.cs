using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeImporter
{
    public class when_importing_multiple_types : given.a_type_importer
    {
        static IEnumerable<IMultipleInterface> instances;

        Establish context = () =>
        {
            type_discoverer_mock.Setup(t => t.FindMultiple<IMultipleInterface>()).Returns(new[]
                                                                                            {
                                                                                                typeof(FirstMultipleClass),
                                                                                                typeof(SecondMultipleClass)
                                                                                            });
            container_mock.Setup(c => c.Get(typeof(FirstMultipleClass))).Returns(new FirstMultipleClass());
            container_mock.Setup(c => c.Get(typeof(SecondMultipleClass))).Returns(new SecondMultipleClass());
        };

        Because of = () => instances = type_importer.ImportMany<IMultipleInterface>();

        It should_return_two_instances = () => instances.Count().ShouldEqual(2);
        It should_contain_first_instance = () => instances.First().ShouldBeOfExactType<FirstMultipleClass>();
        It should_contain_second_instance = () => instances.ToArray()[1].ShouldBeOfExactType<SecondMultipleClass>();
    }
}
