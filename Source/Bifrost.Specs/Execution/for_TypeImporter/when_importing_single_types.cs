using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeImporter
{
    [Subject(typeof(TypeImporter))]
    public class when_importing_single_types : given.a_type_importer
    {
        static object instance;

        Because of = () =>
                         {
                             type_discoverer_mock.Setup(t => t.FindSingle<ISingleInterface>()).Returns(typeof (SingleClass));
                             container_mock.Setup(c => c.Get(typeof (SingleClass))).Returns(new SingleClass());
                             instance = type_importer.Import<ISingleInterface>();
                         };

        It should_not_return_null = () => instance.ShouldNotBeNull();
        It should_return_expected_type = () => instance.ShouldBeOfType<SingleClass>();
    }
}