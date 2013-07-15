using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryResult
{
    public class when_exception_is_null_and_it_has_items
    {
        static QueryResult result;

        Establish context = () => result = new QueryResult();

        Because of = () =>
        {
            result.Exception = null;
            result.Items = new object[1];
        };

        It should_be_successful = () => result.Success.ShouldBeTrue();
    }
}
