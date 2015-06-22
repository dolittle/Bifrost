using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ContractToImplementorsMap
{
    public class when_getting_implementors_of_abstract_class_that_has_one_implementation : given.an_empty_map
    {
        static IEnumerable<Type> result;

        Establish context = () => map.Feed(new[] { typeof(ImplementationOfAbstractClass) });

        Because of = () => result = map.GetImplementorsFor(typeof(AbstractClass));

        It should_have_the_implementation_only = () => result.ShouldContainOnly(typeof(ImplementationOfAbstractClass));
    }
}
