using System.Collections.Generic;
using System.Linq;
using Bifrost.Extensions;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_CollectionExtensions
{
    [Subject(typeof(CollectionsExtensions))]
    public class when_combining_no_lookups
    {
        static IEnumerable<ILookup<string, int>> lookups; 
        static ILookup<string, int> result;

        Establish context = () =>
        {
            lookups = Enumerable.Empty<ILookup<string, int>>();
        };

        Because of = () => result = lookups.Combine();

        It should_be_empty = () => result.Count().ShouldEqual(0);
    }
}