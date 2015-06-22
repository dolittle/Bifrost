using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Bifrost.Testing;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeFinder.given
{
    public class a_type_finder : dependency_injection
    {
        protected static TypeFinder type_finder;
        protected static IEnumerable<Type> types;

        Establish context = () =>
        {
            types = new[] {
                typeof(ISingle),
                typeof(Single),
                typeof(IMultiple),
                typeof(FirstMultiple),
                typeof(SecondMultiple)
            };

            GetMock<IContractToImplementorsMap>().SetupGet(c => c.All).Returns(types);

            type_finder = new TypeFinder();
        };
    }
}
