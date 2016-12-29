using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeFinder
{
    [Subject(typeof(TypeFinder))]
    public class when_finding_types_with_only_one_implementation : given.a_type_finder
    {
        static Type type_found;

        Establish context = () => GetMock<IContractToImplementorsMap>().Setup(c => c.GetImplementorsFor(typeof(ISingle))).Returns(new[] { typeof(Single) });

        Because of = () => type_found = type_finder.FindSingle<ISingle>(Get<IContractToImplementorsMap>());

        It should_not_return_null = () => type_found.ShouldNotBeNull();
        It should_return_correct_implementation_when = () => type_found.ShouldEqual(typeof(Single));
    }
}