using System;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeImporter
{
    public class when_importing_multiple_types_and_type_discoverer_returns_null : given.a_type_importer
    {
        static Exception exception;

        Establish context = () => type_discoverer_mock.Setup(t => t.FindMultiple<IMultipleInterface>()).Returns((Type[])null);

        Because of = () => exception = Catch.Exception(() => type_importer.ImportMany<IMultipleInterface>());

        It should_throw_argument_exception = () => exception.ShouldBeOfExactType<ArgumentException>();
    }
}
