using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryResult
{
    public class when_exception_is_null_and_it_items_is_null
    {
        static QueryResult result;

        Establish context = () => result = new QueryResult();

        Because of = () =>
        {
            result.Exception = null;
            result.Items = null;
        };

        It should_not_be_successful = () => result.Success.ShouldBeFalse();
    }
}
