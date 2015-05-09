using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeFinder
{
    [Subject(typeof(TypeFinder))]
    public class when_finding_type_by_name_that_does_not_exist : given.a_type_finder
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() =>  type_finder.FindTypeByFullName(types, typeof(Single).FullName + "Blah"));

        It should_be_throw_unable_to_resolve_type_by_name = () => exception.ShouldBeOfExactType<UnableToResolveTypeByName>();
    }
}