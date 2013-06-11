using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_Clauses
{
    public class when_page_size_is_more_than_zero
    {
        static Clauses clauses = new Clauses();

        Because of = () => clauses.PageSize = 42;

        It should_not_have_paging_enabled = () => clauses.Paging.ShouldBeTrue();
    }
}
