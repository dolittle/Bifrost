using System.Collections.Generic;
using System.Linq;
using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryableProvider
{
    public class when_paging_is_set_two_show_second_page
    {
        static QueryableProvider provider;
        static PagingInfo clauses;
        static IQueryable source;
        static QueryProviderResult result;
        static int expected_total_items;

        static string[] first_page;
        static string[] second_page;
        static string[] third_page;

        Establish context = () => 
        {
            provider = new QueryableProvider();
            clauses = new PagingInfo
            {
                Size = 2,
                Number = 1
            };

            first_page = new [] {
                "first_page_first_item",
                "first_page_second_item"
            };
            second_page = new [] {
                "second_page_first_item",
                "second_page_second_item"
            };
            third_page = new [] {
                "third_page_first_item",
                "third_page_second_item"
            };

            var allPages = new List<string>();
            allPages.AddRange(first_page);
            allPages.AddRange(second_page);
            allPages.AddRange(third_page);

            expected_total_items = allPages.Count;

            source = allPages.AsQueryable();
        };

        Because of = () => result = provider.Execute(source, clauses);

        It should_return_only_second_page_elements = () => result.Items.ShouldContain(second_page);
        It should_set_the_total_items_to_the_total_number_of_items_in_the_non_paged_source = () => result.TotalItems.ShouldEqual(expected_total_items);
    }
}
