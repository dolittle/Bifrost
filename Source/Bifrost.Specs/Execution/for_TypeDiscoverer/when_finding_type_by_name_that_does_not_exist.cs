using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer
{
    [Subject(typeof(TypeDiscoverer))]
    public class when_finding_type_by_name_that_does_not_exist : given.a_type_discoverer
    {
        static Type typeFound;
        Because of = () => typeFound = TypeDiscoverer.FindTypeByFullName(typeof(Single).FullName + "Blah");

        It should_be_null = () => typeFound.ShouldBeNull();
    }
}