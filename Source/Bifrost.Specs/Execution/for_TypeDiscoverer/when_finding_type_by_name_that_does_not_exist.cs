using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer
{
    [Subject(typeof(TypeDiscoverer))]
    public class when_finding_type_by_name_that_does_not_exist : given.a_type_discoverer
    {
        static Type type_found;

        Establish context = () => type_finder_mock.Setup(t => t.FindTypeByFullName(contract_to_implementors_map_mock.Object, Moq.It.IsAny<string>())).Returns((Type)null);

        Because of = () => type_found = type_discoverer.FindTypeByFullName(typeof(Single).FullName + "Blah");

        It should_be_null = () => type_found.ShouldBeNull();
    }
}