using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer
{
    [Subject(typeof(TypeDiscoverer))]
    public class when_finding_types_with_multiple_implementations : given.a_type_discoverer
    {
        static IEnumerable<Type> types_found;

        Establish context = () => type_finder_mock.Setup(t => t.FindMultiple<IMultiple>(contract_to_implementors_map_mock.Object)).Returns(new[] { typeof(FirstMultiple), typeof(SecondMultiple) });

        Because of = () => types_found = type_discoverer.FindMultiple<IMultiple>();

        It should_not_return_null = () => types_found.ShouldNotBeNull();
        It should_contain_the_types_found_by_the_finder = () => types_found.ShouldContain(typeof(FirstMultiple), typeof(SecondMultiple));
    }
}