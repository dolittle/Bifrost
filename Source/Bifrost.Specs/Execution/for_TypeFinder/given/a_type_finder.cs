using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_TypeFinder.given
{
    public class a_type_finder
    {
        protected static TypeFinder type_finder;
        protected static IEnumerable<Type> types;
        protected static Mock<IContractToImplementorsMap> contract_to_implementors_map_mock;

        Establish context = () =>
        {
            types = new[] {
                typeof(ISingle),
                typeof(Single),
                typeof(IMultiple),
                typeof(FirstMultiple),
                typeof(SecondMultiple)
            };

            contract_to_implementors_map_mock = new Mock<IContractToImplementorsMap>();
            contract_to_implementors_map_mock.SetupGet(c => c.All).Returns(types);

            type_finder = new TypeFinder();
        };
    }
}
