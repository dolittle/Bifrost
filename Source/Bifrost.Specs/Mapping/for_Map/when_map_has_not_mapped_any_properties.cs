using System.Linq;
using Bifrost.Mapping;
using Machine.Specifications;

namespace Bifrost.Specs.Mapping.for_Map
{
    public class when_map_has_not_mapped_any_properties
    {
        static MapWithoutMappings map;

        Because of = () => map = new MapWithoutMappings();

        It should_hold_one_mapped_property = () => map.Properties.Count().ShouldEqual(1);
        It should_hold_the_mapped_property = () => map.Properties.First().From.ShouldEqual(typeof(Source).GetProperty("SomeProperty"));
        It should_map_with_the_source_property_strategy = () => map.Properties.First().Strategy.ShouldBeOfExactType<SourcePropertyMappingStrategy>();
    }
}
