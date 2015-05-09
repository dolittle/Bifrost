using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer
{
    [Subject(typeof(TypeDiscoverer))]
    public class when_finding_type_by_name_that_exists : given.a_type_discoverer
    {
        static Type typeFound;
        Because of = () => typeFound = type_discoverer.FindTypeByFullName(typeof(Single).FullName);

        It should_not_return_null = () => typeFound.ShouldNotBeNull();
        It should_return_the_correct_type = () => typeFound.ShouldEqual(typeof(Single));
    }
}