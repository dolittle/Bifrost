using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeImporter
{
    [Subject(typeof(TypeImporter))]
    public class when_importing_single_types_and_multiple_are_available : given.a_type_importer
    {
        static object Instance;
        static Exception Exception;

        Because of = () => Exception = Catch.Exception(() =>
                                                            {
                                                                type_discoverer_mock.Setup(
                                                                    t => t.FindSingle<IMultipleInterface>()).Throws(new ArgumentException());
                                                                Instance = type_importer.Import<IMultipleInterface>();
                                                            });

        It should_throw_an_ArgumentException = () => Exception.ShouldBeOfExactType<ArgumentException>();
    }
}