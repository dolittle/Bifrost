using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_PagingInfo
{
    public class when_page_size_is_zero
    {
        static PagingInfo paging = new PagingInfo();

        Because of = () => paging.Size = 0;

        It should_not_have_paging_enabled = () => paging.Enabled.ShouldBeFalse();
    }
}
