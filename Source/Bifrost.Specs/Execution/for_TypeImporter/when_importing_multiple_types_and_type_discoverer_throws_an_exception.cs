using System;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeImporter
{
    public class when_importing_multiple_types_and_type_discoverer_throws_an_exception : given.a_type_importer
    {
        static Exception exception;

        Establish context = () => type_discoverer_mock.Setup(t => t.FindMultiple<IMultipleInterface>()).Throws<ArgumentException>();

        Because of = () => exception = Catch.Exception(() => type_importer.ImportMany<IMultipleInterface>());

        It should_throw_argument_exception = () => exception.ShouldBeOfExactType<ArgumentException>();
    }
}
