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
            GetMock<ITypeDiscoverer>().Setup(t => t.FindSingle<ISingleInterface>()).Returns(typeof (SingleClass));
            GetMock<IContainer>().Setup(c => c.Get(typeof (SingleClass))).Returns(new SingleClass());
            instance = type_importer.Import<ISingleInterface>();
        };

        It should_not_return_null = () => instance.ShouldNotBeNull();
        It should_return_expected_type = () => instance.ShouldBeOfExactType<SingleClass>();
    }
}