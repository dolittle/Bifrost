using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer
{
    [Subject(typeof(TypeDiscoverer))]
    public class when_finding_types_with_only_one_implementation : given.a_type_discoverer
    {
        static Type typeFound;

        Establish context = () => type_finder_mock.Setup(t => t.FindSingle<ISingle>(contract_to_implementors_map_mock.Object)).Returns(typeof(Single));

        Because we_find_single = () => typeFound = type_discoverer.FindSingle<ISingle>();

        It should_not_return_null = () => typeFound.ShouldNotBeNull();
        It should_return_correct_implementation_when = () => typeFound.ShouldEqual(typeof(Single));
    }
}