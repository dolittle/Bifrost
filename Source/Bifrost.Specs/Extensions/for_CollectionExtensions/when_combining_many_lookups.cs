using System.Collections.Generic;
using System.Linq;
using Bifrost.Extensions;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_CollectionExtensions
{
    [Subject(typeof(CollectionsExtensions))]
    public class when_combining_many_lookups
    {
        static IEnumerable<ILookup<string, int>> lookups;
        static ILookup<string, int> result;

        Establish context = () =>
        {
            lookups = new[]
            {
                new[]
                {
                    new {K = "1", V = 1},
                    new {K = "2", V = 2},
                    new {K = "2", V = 22},
                    new {K = "3", V = 3},
                }.ToLookup(a => a.K, a => a.V),
                new[]
                {
                    new {K = "1", V = 1},
                    new {K = "2", V = 2},
                }.ToLookup(a => a.K, a => a.V),
                new[]
                {
                    new {K = "2", V = 2},
                    new {K = "3", V = 333},
                }.ToLookup(a => a.K, a => a.V),
                new[]
                {
                    new {K = "", V = 88},
                    new {K = "4", V = 444},
                }.ToLookup(a => a.K, a => a.V),
            };
        };

        Because of = () => result = lookups.Combine();

        It should_have_all_values = () => result.Count().ShouldEqual(5);

        It should_map_first_value = () => result["1"].ShouldContainOnly(1, 1);

        It should_map_second_value = () => result["2"].ShouldContainOnly(2, 22, 2, 2);

        It should_map_third_value = () => result["3"].ShouldContainOnly(3, 333);

        It should_map_fourth_value = () => result["4"].ShouldContainOnly(444);

        It should_map_empty_value = () => result[""].ShouldContainOnly(88);
    }
}