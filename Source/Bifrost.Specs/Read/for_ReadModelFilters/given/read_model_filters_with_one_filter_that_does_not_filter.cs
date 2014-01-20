using System;
using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_ReadModelFilters.given
{
    public class read_model_filters_with_one_filter_that_does_not_filter : all_dependencies
    {
        protected static ReadModelFilters   filters;
        protected static FilterThatDoesNotFilter filter;

        Establish context = () =>
        {
            type_discoverer_mock.Setup(t => t.FindMultiple<ICanFilterReadModels>()).Returns(new Type[] {
                typeof(FilterThatDoesNotFilter)
            });

            filter = new FilterThatDoesNotFilter();
            container_mock.Setup(c => c.Get(typeof(FilterThatDoesNotFilter))).Returns(filter);

            filters = new ReadModelFilters(type_discoverer_mock.Object, container_mock.Object);
        };
    }
}
