using System.Collections;
using System.Collections.Generic;
using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_ReadModelFilters
{
    public class when_filtering_with_one_filter_that_does_not_filter : given.read_model_filters_with_one_filter_that_does_not_filter
    {
        static ReadModelWithString[] items = new[] {
            new ReadModelWithString { Content = "Hello" },
            new ReadModelWithString { Content = "World" },
        };
        static IEnumerable<IReadModel> result = null;

        Because of = () => result = filters.Filter(items);

        It should_return_the_same_as_input = () => result.ShouldEqual(items);
        It should_call_the_filter = () => filter.FilterCalled.ShouldBeTrue();
    }
}
