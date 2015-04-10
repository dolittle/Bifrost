using System.Linq;
using Bifrost.Mapping;
using Machine.Specifications;

namespace Bifrost.Specs.Mapping.for_Map
{
    public class when_map_has_one_property_mapped_and_one_property_unmapped
    {
        static MapWithOneOfTwoPropertiesMapped map;

        Because of = () => map = new MapWithOneOfTwoPropertiesMapped();

        It should_hold_one_mapped_property = () => map.Properties.Count().ShouldEqual(2);
        It should_hold_the_first_mapped_property = () => map.Properties.First().From.ShouldEqual(typeof(Source).GetProperty("FirstProperty"));
        It should_hold_the_second_mapped_property = () => map.Properties.ToArray()[1].From.ShouldEqual(typeof(Source).GetProperty("SecondProperty"));
        It should_map_with_the_source_property_strategy = () => map.Properties.ToArray()[1].Strategy.ShouldBeOfExactType<SourcePropertyMappingStrategy>();
    }
}
